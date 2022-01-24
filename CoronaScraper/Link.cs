namespace CoronaScraper
{
    public class Link
    {
        public static class Age_Race
        {
            //Age and Race
            public const string Confirmed_Cases = "1ofE0QnPLQu9fN387-_hq3RBmOkf3u4sPJVK72YYgJ2c";
            public const string Deaths =          "11HqcIBD2wpZTUaOQQEKIja6rINEIY2EXK19UJHiufuE";
            public const string Tested =          "1_GaWoyq4oJKr08QwPeq8CM0OcpnMLBJHSVQAOoS0M1Q";
        }

        public static class Vaccine
        {
            public const string StateWide =         "10VK3XYYZ9rBcoOAQPO8ZaApr-NOjpdWK7Zt_fPdlGKk";
            public const string Administration =    "16Ab_C-LniH3lVjeDmPCIXKPb2aDLKn50S3ghIxPIlRE";
        }

        public const string Gender   = "1c9N0tnfnrOdho1Wumxj1Jbf4WBQW4n1PjvJoyKZFZrI";
        public const string Original = "15mVzrd6bQeWHoBzRdcwGcZzNtjOdc6oAGW6CVc2pIcE";
        public const string Overall  = "1LvheiEFwA4m46WRGadCwPZ-PLquTvyRCtIkyIsaMPdo";

        public enum Covid_Sites { County_Data, Age_Race, County_Test_Results, Overall_Data, County_Map_Cases }
        public const string County_Data = "http://www.dph.illinois.gov/sitefiles/CountyList.json?nocache=1";
        public const string Age_Race_Site = "https://idph.illinois.gov/DPHPublicInformation/api/COVID/GetCountyDemographics?countyName="; //Requires County Name
        public const string County_Test_Results = "https://idph.illinois.gov/DPHPublicInformation/api/covid/getCountyTestResults";
        public const string Overall_Data = "https://idph.illinois.gov/DPHPublicInformation/api/COVID/GetIllinoisCases"; //view-source:http://www.dph.illinois.gov/covid19/covid19-statistics
        public const string County_Map_Cases = "https://idph.illinois.gov/DPHPublicInformation/api/COVID/GetCountyRates";
        public const string Vaccine_Details = "https://idph.illinois.gov/DPHPublicInformation/api/covidvaccine/getStatewideVaccineDetails";
        public const string Vaccine_Administration = "https://idph.illinois.gov/DPHPublicInformation/api/covidVaccine/getVaccineAdministration?countyName=";
        public static string TranslateEnum(County county) => county switch
        {
            County.Adams => Adams,
            County.Alexander => Alexander,
            County.Bond => Bond,
            County.Boone => Boone,
            County.Brown => Brown,
            County.Bureau => Bureau,
            County.Calhoun => Calhoun,
            County.Carroll => Carroll,
            County.Cass => Cass,
            County.Champaign => Champaign,
            County.Chicago => Chicago,
            County.Christian => Christian,
            County.Clark => Clark,
            County.Clay => Clay,
            County.Clinton => Clinton,
            County.Coles => Coles,
            County.Cook => Cook,
            County.Crawford => Crawford,
            County.Cumberland => Cumberland,
            County.De_Witt => De_Witt,
            County.DeKalb => DeKalb,
            County.Douglas => Douglas,
            County.DuPage => DuPage,
            County.Edgar => Edgar,
            County.Edwards => Edwards,
            County.Effingham => Effingham,
            County.Fayette => Fayette,
            County.Ford => Ford,
            County.Franklin => Franklin,
            County.Fulton => Fulton,
            County.Gallatin => Gallatin,
            County.Greene => Greene,
            County.Grundy => Grundy,
            County.Hamilton => Hamilton,
            County.Hancock => Hancock,
            County.Hardin => Hardin,
            County.Henderson => Henderson,
            County.Henry => Henry,
            County.Illinois => Illinois,
            County.Iroquois => Iroquois,
            County.Jackson => Jackson,
            County.Jasper => Jasper,
            County.Jefferson => Jefferson,
            County.Jersey => Jersey,
            County.Jo_Daviess => Jo_Daviess,
            County.Johnson => Johnson,
            County.Kane => Kane,
            County.Kankakee => Kankakee,
            County.Kendall => Kendall,
            County.Knox => Knox,
            County.Lake => Lake,
            County.LaSalle => LaSalle,
            County.Lawrence => Lawrence,
            County.Lee => Lee,
            County.Livingston => Livingston,
            County.Logan => Logan,
            County.Macon => Macon,
            County.Macoupin => Macoupin,
            County.Madison => Madison,
            County.Marion => Marion,
            County.Marshall => Marshall,
            County.Mason => Mason,
            County.Massac => Massac,
            County.McDonough => McDonough,
            County.McHenry => McHenry,
            County.McLean => McLean,
            County.Menard => Menard,
            County.Mercer => Mercer,
            County.Monroe => Monroe,
            County.Montgomery => Montgomery,
            County.Morgan => Morgan,
            County.Moultrie => Moultrie,
            County.Ogle => Ogle,
            County.Out_Of_State => Out_Of_State,
            County.Peoria => Peoria,
            County.Perry => Perry,
            County.Piatt => Piatt,
            County.Pike => Pike,
            County.Pope => Pope,
            County.Pulaski => Pulaski,
            County.Putnam => Putnam,
            County.Randolph => Randolph,
            County.Richland => Richland,
            County.Rock_Island => Rock_Island,
            County.Saline => Saline,
            County.Sangamon => Sangamon,
            County.Schuyler => Schuyler,
            County.Scott => Scott,
            County.Shelby => Shelby,
            County.St_Clair => St_Clair,
            County.Stark => Stark,
            County.Stephenson => Stephenson,
            County.Tazewell => Tazewell,
            County.Unassigned => Unassigned,
            County.Union => Union,
            County.Vermilion => Vermilion,
            County.Wabash => Wabash,
            County.Warren => Warren,
            County.Washington => Washington,
            County.Wayne => Wayne,
            County.White => White,
            County.Whiteside => Whiteside,
            County.Will => Will,
            County.Williamson => Williamson,
            County.Winnebago => Winnebago,
            County.Woodford => Woodford,
            _ => throw new System.NotImplementedException()
        };

        public enum County
        {
            Adams,
            Alexander,
            Bond,
            Boone,
            Brown,
            Bureau,
            Calhoun,
            Carroll,
            Cass,
            Champaign,
            Chicago,
            Christian,
            Clark,
            Clay,
            Clinton,
            Coles,
            Cook,
            Crawford,
            Cumberland,
            De_Witt,
            DeKalb,
            Douglas,
            DuPage,
            Edgar,
            Edwards,
            Effingham,
            Fayette,
            Ford,
            Franklin,
            Fulton,
            Gallatin,
            Greene,
            Grundy,
            Hamilton,
            Hancock,
            Hardin,
            Henderson,
            Henry,
            Illinois,
            Iroquois,
            Jackson,
            Jasper,
            Jefferson,
            Jersey,
            Jo_Daviess,
            Johnson,
            Kane,
            Kankakee,
            Kendall,
            Knox,
            Lake,
            LaSalle,
            Lawrence,
            Lee,
            Livingston,
            Logan,
            Macon,
            Macoupin,
            Madison,
            Marion,
            Marshall,
            Mason,
            Massac,
            McDonough,
            McHenry,
            McLean,
            Menard,
            Mercer,
            Monroe,
            Montgomery,
            Morgan,
            Moultrie,
            Ogle,
            Out_Of_State,
            Peoria,
            Perry,
            Piatt,
            Pike,
            Pope,
            Pulaski,
            Putnam,
            Randolph,
            Richland,
            Rock_Island,
            Saline,
            Sangamon,
            Schuyler,
            Scott,
            Shelby,
            St_Clair,
            Stark,
            Stephenson,
            Tazewell,
            Unassigned,
            Union,
            Vermilion,
            Wabash,
            Warren,
            Washington,
            Wayne,
            White,
            Whiteside,
            Will,
            Williamson,
            Winnebago,
            Woodford,
        }

        public const string Adams = "Adams";
        public const string Alexander = "Alexander";
        public const string Bond = "Bond";
        public const string Boone = "Boone";
        public const string Brown = "Brown";
        public const string Bureau = "Bureau";
        public const string Calhoun = "Calhoun";
        public const string Carroll = "Carroll";
        public const string Cass = "Cass";
        public const string Champaign = "Champaign";
        public const string Chicago = "Chicago";
        public const string Christian = "Christian";
        public const string Clark = "Clark";
        public const string Clay = "Clay";
        public const string Clinton = "Clinton";
        public const string Coles = "Coles";
        public const string Cook = "Cook";
        public const string Crawford = "Crawford";
        public const string Cumberland = "Cumberland";
        public const string De_Witt = "De Witt";
        public const string DeKalb = "DeKalb";
        public const string Douglas = "Douglas";
        public const string DuPage = "DuPage";
        public const string Edgar = "Edgar";
        public const string Edwards = "Edwards";
        public const string Effingham = "Effingham";
        public const string Fayette = "Fayette";
        public const string Ford = "Ford";
        public const string Franklin = "Franklin";
        public const string Fulton = "Fulton";
        public const string Gallatin = "Gallatin";
        public const string Greene = "Greene";
        public const string Grundy = "Grundy";
        public const string Hamilton = "Hamilton";
        public const string Hancock = "Hancock";
        public const string Hardin = "Hardin";
        public const string Henderson = "Henderson";
        public const string Henry = "Henry";
        public const string Illinois = "Illinois";
        public const string Iroquois = "Iroquois";
        public const string Jackson = "Jackson";
        public const string Jasper = "Jasper";
        public const string Jefferson = "Jefferson";
        public const string Jersey = "Jersey";
        public const string Jo_Daviess = "Jo Daviess";
        public const string Johnson = "Johnson";
        public const string Kane = "Kane";
        public const string Kankakee = "Kankakee";
        public const string Kendall = "Kendall";
        public const string Knox = "Knox";
        public const string Lake = "Lake";
        public const string LaSalle = "LaSalle";
        public const string Lawrence = "Lawrence";
        public const string Lee = "Lee";
        public const string Livingston = "Livingston";
        public const string Logan = "Logan";
        public const string Macon = "Macon";
        public const string Macoupin = "Macoupin";
        public const string Madison = "Madison";
        public const string Marion = "Marion";
        public const string Marshall = "Marshall";
        public const string Mason = "Mason";
        public const string Massac = "Massac";
        public const string McDonough = "McDonough";
        public const string McHenry = "McHenry";
        public const string McLean = "McLean";
        public const string Menard = "Menard";
        public const string Mercer = "Mercer";
        public const string Monroe = "Monroe";
        public const string Montgomery = "Montgomery";
        public const string Morgan = "Morgan";
        public const string Moultrie = "Moultrie";
        public const string Ogle = "Ogle";
        public const string Out_Of_State = "Out Of State";
        public const string Peoria = "Peoria";
        public const string Perry = "Perry";
        public const string Piatt = "Piatt";
        public const string Pike = "Pike";
        public const string Pope = "Pope";
        public const string Pulaski = "Pulaski";
        public const string Putnam = "Putnam";
        public const string Randolph = "Randolph";
        public const string Richland = "Richland";
        public const string Rock_Island = "Rock Island";
        public const string Saline = "Saline";
        public const string Sangamon = "Sangamon";
        public const string Schuyler = "Schuyler";
        public const string Scott = "Scott";
        public const string Shelby = "Shelby";
        public const string St_Clair = "St. Clair";
        public const string Stark = "Stark";
        public const string Stephenson = "Stephenson";
        public const string Tazewell = "Tazewell";
        public const string Unassigned = "Unassigned";
        public const string Union = "Union";
        public const string Vermilion = "Vermilion";
        public const string Wabash = "Wabash";
        public const string Warren = "Warren";
        public const string Washington = "Washington";
        public const string Wayne = "Wayne";
        public const string White = "White";
        public const string Whiteside = "Whiteside";
        public const string Will = "Will";
        public const string Williamson = "Williamson";
        public const string Winnebago = "Winnebago";
        public const string Woodford = "Woodford";
    }
}
