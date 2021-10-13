namespace CoronaScraper
{
    public class Link
    {
        //Age and Race
        public const  string Age_Race_Confirmed_Cases = "";
        public const string Age_Race_Deaths =           "";
        public const string Age_Race_Tested =           "";

        public const string Gender   = "";
        public const string Original = "";
        public const string Overall  = "";

        public enum Covid_Sites { County_Data, Age_Race, County_Test_Results, Overall_Data, County_Map_Cases }
        public const string County_Data = "http://www.dph.illinois.gov/sitefiles/CountyList.json?nocache=1";
        public const string Age_Race = "https://idph.illinois.gov/DPHPublicInformation/api/COVID/GetCountyDemographics?countyName="; //Requires County Name
        public const string County_Test_Results = "https://idph.illinois.gov/DPHPublicInformation/api/covid/getCountyTestResults";
        public const string Overall_Data = "https://idph.illinois.gov/DPHPublicInformation/api/COVID/GetIllinoisCases"; //view-source:http://www.dph.illinois.gov/covid19/covid19-statistics
        public const string County_Map_Cases = "https://idph.illinois.gov/DPHPublicInformation/api/COVID/GetCountyRates";

        public static string TranslateEnum(Counties county) => county switch
        {
            Counties.Adams => Adams,
            Counties.Alexander => Alexander,
            Counties.Bond => Bond,
            Counties.Boone => Boone,
            Counties.Brown => Brown,
            Counties.Bureau => Bureau,
            Counties.Calhoun => Calhoun,
            Counties.Carroll => Carroll,
            Counties.Cass => Cass,
            Counties.Champaign => Champaign,
            Counties.Chicago => Chicago,
            Counties.Christian => Christian,
            Counties.Clark => Clark,
            Counties.Clay => Clay,
            Counties.Clinton => Clinton,
            Counties.Coles => Coles,
            Counties.Cook => Cook,
            Counties.Crawford => Crawford,
            Counties.Cumberland => Cumberland,
            Counties.De_Witt => De_Witt,
            Counties.DeKalb => DeKalb,
            Counties.Douglas => Douglas,
            Counties.DuPage => DuPage,
            Counties.Edgar => Edgar,
            Counties.Edwards => Edwards,
            Counties.Effingham => Effingham,
            Counties.Fayette => Fayette,
            Counties.Ford => Ford,
            Counties.Franklin => Franklin,
            Counties.Fulton => Fulton,
            Counties.Gallatin => Gallatin,
            Counties.Greene => Greene,
            Counties.Grundy => Grundy,
            Counties.Hamilton => Hamilton,
            Counties.Hancock => Hancock,
            Counties.Hardin => Hardin,
            Counties.Henderson => Henderson,
            Counties.Henry => Henry,
            Counties.Illinois => Illinois,
            Counties.Iroquois => Iroquois,
            Counties.Jackson => Jackson,
            Counties.Jasper => Jasper,
            Counties.Jefferson => Jefferson,
            Counties.Jersey => Jersey,
            Counties.Jo_Daviess => Jo_Daviess,
            Counties.Johnson => Johnson,
            Counties.Kane => Kane,
            Counties.Kankakee => Kankakee,
            Counties.Kendall => Kendall,
            Counties.Knox => Knox,
            Counties.Lake => Lake,
            Counties.LaSalle => LaSalle,
            Counties.Lawrence => Lawrence,
            Counties.Lee => Lee,
            Counties.Livingston => Livingston,
            Counties.Logan => Logan,
            Counties.Macon => Macon,
            Counties.Macoupin => Macoupin,
            Counties.Madison => Madison,
            Counties.Marion => Marion,
            Counties.Marshall => Marshall,
            Counties.Mason => Mason,
            Counties.Massac => Massac,
            Counties.McDonough => McDonough,
            Counties.McHenry => McHenry,
            Counties.McLean => McLean,
            Counties.Menard => Menard,
            Counties.Mercer => Mercer,
            Counties.Monroe => Monroe,
            Counties.Montgomery => Montgomery,
            Counties.Morgan => Morgan,
            Counties.Moultrie => Moultrie,
            Counties.Ogle => Ogle,
            Counties.Out_Of_State => Out_Of_State,
            Counties.Peoria => Peoria,
            Counties.Perry => Perry,
            Counties.Piatt => Piatt,
            Counties.Pike => Pike,
            Counties.Pope => Pope,
            Counties.Pulaski => Pulaski,
            Counties.Putnam => Putnam,
            Counties.Randolph => Randolph,
            Counties.Richland => Richland,
            Counties.Rock_Island => Rock_Island,
            Counties.Saline => Saline,
            Counties.Sangamon => Sangamon,
            Counties.Schuyler => Schuyler,
            Counties.Scott => Scott,
            Counties.Shelby => Shelby,
            Counties.St_Clair => St_Clair,
            Counties.Stark => Stark,
            Counties.Stephenson => Stephenson,
            Counties.Tazewell => Tazewell,
            Counties.Unassigned => Unassigned,
            Counties.Union => Union,
            Counties.Vermilion => Vermilion,
            Counties.Wabash => Wabash,
            Counties.Warren => Warren,
            Counties.Washington => Washington,
            Counties.Wayne => Wayne,
            Counties.White => White,
            Counties.Whiteside => Whiteside,
            Counties.Will => Will,
            Counties.Williamson => Williamson,
            Counties.Winnebago => Winnebago,
            Counties.Woodford => Woodford,
            _ => throw new System.NotImplementedException()
        };

        public enum Counties
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
