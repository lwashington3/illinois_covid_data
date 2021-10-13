using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using Google.Apis.Util.Store;
using System.Text.RegularExpressions;

namespace CoronaScraper
{
    class Driver
    {
        public static readonly HttpClient client = new ();
        public SheetsService Service { get; set; }
        private UserCredential Credential { get; set; }
        public string ApplicationName { get; set; }
        public static string[] DEFAULT_SCOPES = { SheetsService.Scope.Spreadsheets };

        static void Main(string[] args)
        {
            Driver driver = CreateDriver(args);

            Console.WriteLine("Scraping Overall Data");
            driver.ScrapeOverallData(false);
            Console.WriteLine("Scraping Gender Data");
            driver.ScrapeGenderData();
            Console.WriteLine("Scraping Age/Race Data");
            driver.ScrapeAgeRaceData();
        }

        public static Driver CreateDriver(string[] args)
        {
            if (args.Length > 1)
            {
                for (int i = 0; i < args.Length - 1; i++)
                {
                    Regex regex = new (@"(?-)c\b");
                    if (regex.IsMatch(args[i]))
                    {
                        return new(args[i + 1]);
                    }
                }
            }
            else if (args.Length == 1)
            {
                return new(args[0]);
            }
            return new();
        }

        public Driver() : this(Properties.Resources.credentials, DEFAULT_SCOPES) { }
        public Driver(string credentialLocation) : this(credentialLocation, DEFAULT_SCOPES) { }
        public Driver(string credentialLocation, IEnumerable<string> scopes) : this(new FileStream(credentialLocation, FileMode.Open, FileAccess.Read), scopes) { }
        public Driver(byte[] credentialLocation, IEnumerable<string> scopes) : this(new MemoryStream(credentialLocation), scopes) { }
        public Driver(Stream credentialStream, IEnumerable<string> scopes)
        {
            ApplicationName = "Illinois Coronavirus Data Scraper";
            Credential = CreateCredential(credentialStream, scopes);
            Service = CreateSheetsService();
        }
        private static UserCredential CreateCredential(Stream stream, IEnumerable<string> scopes)
        {
            string credPath = "token";
            return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
        }
        private SheetsService CreateSheetsService()
        {
            return new (new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = ApplicationName,
            });
        }
        public static async Task<string> CallUrl(string link)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(link);
            return await response;
        }
        public void ScrapeOverallData()
        {
            ScrapeOverallData(false);
        }
        public void ScrapeOverallData(bool addEachEntry)
        {
            string response = CallUrl(Link.Overall_Data).Result;
            OverallData data = JsonConvert.DeserializeObject<OverallData>(response);

            List<IList<object>> values = new ();

            if (addEachEntry)
            {
                foreach(IllinoisTestingResults results in data)
                {
                    values.Add(new List<object>() { results.TestDate.ToString("MM/dd/yyyy"), results.TotalTested, results.ConfirmedCases, results.Deaths, results.TestedChange, results.CasesChange, results.DeathsChange, results.Tested7DayRollingAvg, results.Cases7DayRollingAvg, results.Deaths7DayRollingAvg});
                }
            }
            else
            {
                ValueRange dataResponse = Service.Spreadsheets.Values.Get(Link.Overall, "Illinois Data!A2:A").Execute();

                DateTime lastUpload = DateTime.Parse(dataResponse.Values[^1][0].ToString());
                if(lastUpload != data.UpdatedDate)
                {
                    int indexes = data.UpdatedDate.DayOfWeek == DayOfWeek.Monday ? 3 : 1;

                    for(int i = 1; i <= indexes; i++)
                    {
                        IllinoisTestingResults results = data[^i];
                        values.Add(new List<object>() { results.TestDate.ToString("MM/dd/yyyy"), results.TotalTested, results.ConfirmedCases, results.Deaths, results.TestedChange, results.CasesChange, results.DeathsChange, results.Tested7DayRollingAvg, results.Cases7DayRollingAvg, results.Deaths7DayRollingAvg });
                    }

                    //Reverses list because it's adds newer first

                    values.Reverse();

                }
                else
                {
                    return; //No need to keep going, the spreadsheet is up to date
                }
            }

            ValueRange range = new()
            {
                Values = values
            };
            SpreadsheetsResource.ValuesResource.AppendRequest request = Service.Spreadsheets.Values.Append(range, Link.Overall, "Illinois Data!A2:J");
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            request.Execute();

            #region Formatting
            BatchUpdateSpreadsheetRequest format = new();
            format.Requests = new List<Request>()
                {
                    new Request { RepeatCell = new()
                    {
                        Fields = "userEnteredFormat.numberFormat",
                        Range = new()
                        {
                            StartColumnIndex = 0,
                            EndColumnIndex = 1,
                            StartRowIndex = 1
                        },
                        Cell = new CellData()
                        {
                            UserEnteredFormat = new CellFormat()
                            {
                                NumberFormat = new NumberFormat()
                                {
                                    Type = "DATE",
                                    Pattern = "MM/dd/YYYY"
                                }
                            }
                        }
                    }
                },
                new Request // Number formatting
                {
                    RepeatCell = new RepeatCellRequest
                    {
                        Fields = "userEnteredFormat.numberFormat",
                        Range = new()
                        {
                            StartColumnIndex = 1,
                            EndColumnIndex = 10,
                            StartRowIndex = 1
                        },
                        Cell = new CellData()
                        {
                            UserEnteredFormat = new CellFormat()
                            {
                                NumberFormat = new NumberFormat()
                                {
                                    Type = "NUMBER",
                                    Pattern = "###,##0"
                                }
                            }
                        }
                    }
                }
             };
            Service.Spreadsheets.BatchUpdate(format, Link.Overall).Execute();
            #endregion
        }
        public void ScrapeGenderData()
        {
            string link = Link.Age_Race;
            string response = CallUrl(link).Result;
            Root data = JsonConvert.DeserializeObject<Root>(response);
            CountyDemographic illinois = data["Illinois"];

            IList<IList<object>> values = new List<IList<object>>();

            List<Tuple<string, string, int, int?>> tuples = new();
            Spreadsheet spreadsheet = Service.Spreadsheets.Get(Link.Gender).Execute();

            List<DateTime> uploadDates = new();

            foreach (string sheetName in new string[] { "Confirmed Cases", "Tested", "Deaths"})
            {
                ValueRange dataResponse = Service.Spreadsheets.Values.Get(Link.Gender, $"{sheetName}!A2:A").Execute();
                int row = dataResponse.Values.Count + 2;
                DateTime lastUpload = DateTime.Parse(dataResponse.Values[^1][0].ToString());

                int? id = null;

                foreach(Sheet sheet in spreadsheet.Sheets)
                {
                    if(sheet.Properties.Title == sheetName)
                    {
                        id = sheet.Properties.SheetId;
                    }
                }

                tuples.Add(new(sheetName.Replace(" ", ""), sheetName, row, id));
                uploadDates.Add(lastUpload);
            }
 
            if (!uploadDates.Contains(data.UpdatedDate))
            {
                List<Gender> genders = illinois.Demographics.Gender;
                foreach (var tuple in tuples) {
                    List<object> dataList = new () {data.UpdatedDate.ToString("MM/dd/yyyy") };
                    genders.ForEach(x => dataList.Add(x.GetType().GetProperty(tuple.Item1).GetValue(x, null)));
                    dataList.Add($"=SUM(B{tuple.Item3}:D{tuple.Item3})");
                    values.Add(dataList);
                    ValueRange range = new()
                    {
                        Values = values
                    };
                    SpreadsheetsResource.ValuesResource.AppendRequest request = Service.Spreadsheets.Values.Append(range, Link.Gender , $"{tuple.Item2}!A:E");
                    request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                    request.Execute();
                    values.Clear();

                    #region Formatting
                    BatchUpdateSpreadsheetRequest format = new();
                    format.Requests = new List<Request>()
                    {
                        new Request { RepeatCell = new()
                        {
                            Fields = "userEnteredFormat.numberFormat",
                            Range = new()
                            {
                                SheetId = tuple.Item4,
                                StartColumnIndex = 0,
                                EndColumnIndex = 1,
                                StartRowIndex = 1
                            },
                            Cell = new CellData()
                            {
                                UserEnteredFormat = new CellFormat()
                                {
                                    NumberFormat = new NumberFormat()
                                    {
                                        Type = "DATE",
                                        Pattern = "MM/dd/YYYY"
                                    }
                                }
                            }
                        }
                    },
                    new Request // Number formatting
                    {
                        RepeatCell = new RepeatCellRequest
                        {
                            Fields = "userEnteredFormat.numberFormat",
                            Range = new()
                            {
                                SheetId = tuple.Item4,
                                StartColumnIndex = 1,
                                EndColumnIndex = 5,
                                StartRowIndex = 1
                            },
                            Cell = new CellData()
                            {
                                UserEnteredFormat = new CellFormat()
                                {
                                    NumberFormat = new NumberFormat()
                                    {
                                        Type = "NUMBER",
                                        Pattern = "###,##0"
                                    }
                                }
                            }
                        }
                    }
                    };
                    Service.Spreadsheets.BatchUpdate(format, Link.Gender).Execute();
                    #endregion
                }
            }
        }
        public void ScrapeAgeRaceData()
        {
            string link = Link.Age_Race;
            string response = CallUrl(link).Result;

            Root data = JsonConvert.DeserializeObject<Root>(response);
            CountyDemographic illinois = data["Illinois"];
            
            string newSheetName = data.UpdatedDate.ToString("MMMM dd, yyyy");

            List<Tuple<string, string>> tuples = new()
            {
                new(Link.Age_Race_Confirmed_Cases, "Confirmed Cases"),
                new(Link.Age_Race_Tested, "Tested"),
                new(Link.Age_Race_Deaths, "Deaths")
            };

            foreach(var tuple in tuples)
            {
                string spreadsheetID = tuple.Item1;
                string selectedProperty = tuple.Item2.Replace(" ", "");
                #region Check if Sheet Exists
                Spreadsheet spreadsheet = Service.Spreadsheets.Get(spreadsheetID).Execute();
                bool alreadyCreated = false;
                foreach(Sheet check in spreadsheet.Sheets)
                {
                    if (check.Properties.Title == newSheetName)
                    {
                        alreadyCreated = true;
                        break;
                    }
                }

                if (alreadyCreated)
                {
                    continue;
                }

                #endregion
                #region Add Sheet

                AddSheetRequest sheetRequest = new()
                { Properties = new SheetProperties() };

                sheetRequest.Properties.Title = newSheetName;

                BatchUpdateSpreadsheetRequest body = new()
                {
                    IncludeSpreadsheetInResponse = true
                };
                body.Requests = new List<Request>
                {
                    new Request { AddSheet = sheetRequest }
                };
                var newSheet = Service.Spreadsheets.BatchUpdate(body, spreadsheetID).Execute();
                int? newSheetId = null;
                Sheet sheet = null;
                foreach (Sheet updatedSheet in newSheet.UpdatedSpreadsheet.Sheets)
                {
                    if (updatedSheet.Properties.Title == newSheetName)
                    {
                        newSheetId = updatedSheet.Properties.SheetId;
                        sheet = updatedSheet;
                    }
                }

                #endregion
                #region Formatting Data For Upload
                List<Tuple<string, string>> abbreviations = new() 
                {
                    new ("AI/AN**", "American Indian or Alaskan Native"),
                    new("NH/PI*", "Native Hawaiian or Other Pacific Islander"),
                    new("Hispanic", "Hispanic"),
                    new("Asian", "Asian"),
                    new("Other", "Other"),
                    new("Left Blank", "Left Blank"),
                    new("Black", "Black"),
                    new("White", "White")
                };
                List<BasicChartSeries> series = new();
                List<GridRange> pieSeries = new();            
                           
                List<object> firstRow = new() {"Age Group"};
                abbreviations.ForEach(x => firstRow.Add(x.Item2));
                firstRow.Add("Total");
                IList<IList<object>> values = new List<IList<object>>() { firstRow };
                
                foreach (Age ageGroup in illinois.Demographics.Age)
                {
                    List<object> row = new ();

                    row.Add(ageGroup.AgeGroup.Trim());
                    foreach(var abbreviation in abbreviations)
                    {
                        foreach (Race race in ageGroup.Race)
                        {
                            if (abbreviation.Item1 == race.Description)
                            {
                                row.Add(race.GetType().GetProperty(selectedProperty).GetValue(race, null));
                            }
                        }
                    }

                    

                    row.Add(ageGroup.GetType().GetProperty(selectedProperty).GetValue(ageGroup, null));

                    values.Add(row);
                }

                List<object> lastRow = new() {"Total"};

                foreach (var abbreviation in abbreviations)
                {
                    foreach (Race race in illinois.Demographics.Race)
                    {
                        if (abbreviation.Item1 == race.Description)
                        {
                            lastRow.Add(race.GetType().GetProperty(selectedProperty).GetValue(race, null));
                        }
                    }
                }

                Type illinoisType = illinois.GetType();
                var propertyInfo = illinoisType.GetProperty(selectedProperty);
                if(propertyInfo == null)
                {
                    lastRow.Add("=SUM(J2:J10)");
                }
                else 
                {
                    lastRow.Add(propertyInfo.GetValue(illinois, null));
                }
                values.Add(lastRow); //Use given values, then create add
                #endregion
                #region Upload
                ValueRange range = new()
                {
                    Values = values
                };
                SpreadsheetsResource.ValuesResource.AppendRequest request = Service.Spreadsheets.Values.Append(range, spreadsheetID, $"{newSheetName}!A1:J11");
                request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                request.Execute();

                #endregion
                #region Formatting
                BatchUpdateSpreadsheetRequest format = new();
                format.Requests = new List<Request>()
                {
                    new Request{ UpdateSheetProperties = new()
                    {
                        Fields = "gridProperties.frozenRowCount",
                        Properties = new()
                        {
                            SheetId = newSheetId,
                            GridProperties = new()
                            {
                                FrozenRowCount = 1,
                                FrozenColumnCount = 1
                            }
                        }
                    } 
                },
                    new Request // Number formatting
                    {
                        RepeatCell = new RepeatCellRequest
                        {
                            Fields = "userEnteredFormat.numberFormat",
                            Range = new()
                            {
                                SheetId = newSheetId,
                                StartColumnIndex = 1,
                                EndColumnIndex = 10,
                                StartRowIndex = 1,
                            },
                            Cell = new CellData()
                            {
                                UserEnteredFormat = new CellFormat()
                                {
                                    NumberFormat = new NumberFormat()
                                    {
                                        Type = "NUMBER",
                                        Pattern = "###,##0"
                                    }
                                }
                            }
                        }
                    }
                    };
                Service.Spreadsheets.BatchUpdate(format, tuple.Item1).Execute();
                #endregion
                #region Bar Chart
                int column = 1;
                foreach (var abbreviation in abbreviations)
                {
                    foreach (Race race in illinois.Demographics.Race)
                    {
                        if (race.Description == abbreviation.Item1)
                        {
                            series.Add(new BasicChartSeries()
                            {
                                Color = ConvertColor(race.Color),// find way to programmatically generate series because colors are in illinois
                                TargetAxis = "LEFT_AXIS",
                                Series = new ChartData()
                                {
                                    SourceRange = new ChartSourceRange()
                                    {
                                        Sources = new List<GridRange>()
                                        {
                                            new GridRange()
                                            {
                                                SheetId = newSheetId,
                                                StartRowIndex = 0,
                                                EndRowIndex = 10,
                                                StartColumnIndex = column,
                                                EndColumnIndex = column + 1
                                            }
                                        }
                                    }
                                }
                            });
                            pieSeries.Add(new GridRange()
                            {
                                SheetId = newSheetId,
                                StartRowIndex = 0,
                                EndRowIndex = 10,
                                StartColumnIndex = column,
                                EndColumnIndex = column + 1
                            });
                            column++;
                            break;
                        }
                    }
                }

                BatchUpdateSpreadsheetRequest barChart = new()
                {
                    Requests = new List<Request>()
                };

                barChart.Requests.Add(new Request
                {
                    AddChart = new AddChartRequest()
                    {
                        Chart = new EmbeddedChart()
                        {
                            Spec = new ChartSpec()
                            {
                                Title = $"{tuple.Item2} per Age Group",
                                TitleTextFormat = new TextFormat()
                                {
                                    ForegroundColor = new Color
                                    {
                                        Red = 0,
                                        Green = 0,
                                        Blue = 0,
                                        Alpha = 1
                                    }
                                },
                                TitleTextPosition = new TextPosition()
                                {
                                    HorizontalAlignment = "CENTER"
                                },
                                BasicChart = new BasicChartSpec()
                                {
                                    TotalDataLabel = new DataLabel() 
                                    {
                                        Type = "DATA",
                                    },
                                    StackedType = "STACKED",
                                    ChartType = "COLUMN",
                                    HeaderCount = 1,
                                    Axis = new List<BasicChartAxis>()
                                    {
                                        new BasicChartAxis()
                                        {
                                            Position = "LEFT_AXIS",
                                            Title = tuple.Item2
                                        },
                                        new BasicChartAxis()
                                        {
                                            Position = "BOTTOM_AXIS",
                                            Title = "Age Group"
                                        }
                                    },
                                    Domains = new List<BasicChartDomain>()
                                    {
                                        new BasicChartDomain()
                                        {
                                            Domain = new ChartData()
                                            {
                                                SourceRange = new ChartSourceRange()
                                                {
                                                    Sources = new List<GridRange>()
                                                    {
                                                        new GridRange()
                                                        {
                                                            SheetId = newSheetId,
                                                            StartRowIndex = 0,
                                                            EndRowIndex = 10,
                                                            StartColumnIndex = 0,
                                                            EndColumnIndex = 1
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    Series = series
                                }
                            },
                            Position = new EmbeddedObjectPosition()
                            {
                                OverlayPosition = new OverlayPosition()
                                {
                                    AnchorCell = new GridCoordinate()
                                    {
                                        SheetId = newSheetId,
                                        ColumnIndex = 0,
                                        RowIndex = 11
                                    },
                                    OffsetXPixels = 0,
                                    OffsetYPixels = 0,
                                    WidthPixels = 800,
                                    HeightPixels = 495
                                }
                            }
                        }
                    }
                });
                Service.Spreadsheets.BatchUpdate(barChart, tuple.Item1).Execute();

                #endregion
                #region Pie Chart
                BatchUpdateSpreadsheetRequest pieChart = new() { Requests = new List<Request>()};

                pieChart.Requests.Add(new Request()
                {
                    AddChart = new AddChartRequest()
                    {
                        Chart = new EmbeddedChart()
                        {
                            Spec = new ChartSpec()
                            {
                                Title = $"{tuple.Item2} per Race",
                                TitleTextFormat = new TextFormat()
                                {
                                    ForegroundColor = new Color
                                    {
                                        Red = 0,
                                        Green = 0,
                                        Blue = 0,
                                        Alpha = 1
                                    }
                                },
                                TitleTextPosition = new TextPosition()
                                {
                                    HorizontalAlignment = "CENTER"
                                },
                                PieChart = new PieChartSpec()
                                {
                                    LegendPosition = "RIGHT_LEGEND",
                                    ThreeDimensional = true,
                                    Domain = new ChartData() 
                                    {
                                        SourceRange = new ChartSourceRange()
                                        {
                                            Sources = new List<GridRange>()
                                            {
                                                new()
                                                {
                                                    SheetId = newSheetId,
                                                    StartRowIndex = 0,
                                                    EndRowIndex = 1,
                                                    StartColumnIndex = 0,
                                                    EndColumnIndex = 9

                                                }
                                            }
                                        }
                                    },
                                    Series = new ChartData()
                                    {
                                        SourceRange = new ChartSourceRange()
                                        {
                                            Sources = new List<GridRange>() 
                                            {
                                                new GridRange()
                                                {
                                                    SheetId = newSheetId,
                                                    StartRowIndex = 10,
                                                    EndRowIndex = 11,
                                                    StartColumnIndex = 0,
                                                    EndColumnIndex = 9
                                                }
                                            } 
                                        }
                                    }
                                }
                            },
                            Position = new EmbeddedObjectPosition()
                            {
                                OverlayPosition = new OverlayPosition()
                                {
                                    AnchorCell = new GridCoordinate()
                                    {
                                        SheetId = newSheetId,
                                        RowIndex = 11,
                                        ColumnIndex = 8
                                    },
                                    OffsetXPixels = 0,
                                    OffsetYPixels = 0,
                                }
                            }
                        }
                    }
                });

                Service.Spreadsheets.BatchUpdate(pieChart, tuple.Item1).Execute();
                #endregion
            }
        }

        public static Color ConvertColor(string color)
        {
            Regex regex = new (@"rgb\((\d+),(\d+),(\d+)\)");

            MatchCollection matches = regex.Matches(color);
            Match match = matches[0];
            if(match.Groups.Count == 4) {
                return new()
                {
                    Red = Int16.Parse(match.Groups[1].Value) / ((float)255.0),
                    Green = Int16.Parse(match.Groups[2].Value) / ((float)255.0),
                    Blue = Int16.Parse(match.Groups[3].Value) / ((float)255.0),
                    Alpha = (float)1.0,
                };
            }
            throw new Exception($"Could not find RGB value in `{color}`");
        }
    }
}
