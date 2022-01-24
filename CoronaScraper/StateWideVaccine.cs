using System;
using System.Reflection;
using Newtonsoft.Json;

namespace CoronaScraper
{
    [JsonObject]
    public class StateWideVaccine
    {
        public void CorrectPercentages()
        {   //The api sends percentages over as ##.# instead of .### so this makes it easier for Google Sheets Formatting
            string[] percentage_properties = new string[] {"AdministeredToIllinois_Fully_5_Percent", "AdministeredToIllinois_Fully_12_Percent", "AdministeredToIllinois_Fully_18_Percent", "AdministeredToIllinois_Fully_65_Percent",
                "AdministeredToIllinois_One_5_Percent", "AdministeredToIllinois_One_12_Percent", "AdministeredToIllinois_One_18_Percent", "AdministeredToIllinois_One_65_Percent",
                "AdministeredToIllinoisans_Fully_5_Percent", "AdministeredToIllinoisans_Fully_12_Percent", "AdministeredToIllinoisans_Fully_18_Percent", "AdministeredToIllinoisans_Fully_65_Percent",
                "AdministeredToIllinoisans_One_5_Percent", "AdministeredToIllinoisans_One_12_Percent", "AdministeredToIllinoisans_One_18_Percent", "AdministeredToIllinoisans_One_65_Percent"};
            
            foreach(string property in percentage_properties)
            {
                PropertyInfo info = GetType().GetProperty(property);
                info.SetValue(this, (double)info.GetValue(this) / 100);
            }
        }
        [JsonProperty("Total_Delivered")]
        public int Total_Doses { get; set; }

        [JsonProperty("Total_Administered")]
        public int Total_Administered { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated")]
        public int Persons_Fully_Vaccinated { get; set; }

        [JsonProperty("AdministeredRollAvg")]
        public double AdministeredRollingAvg { get; set; }

        [JsonProperty("Report_Date")]
        public DateTime ReportDate { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated5plus")]
        public int AdministeredToIllinois_Fully_5 { get; set; }

        [JsonProperty("Population_Fully_Vaccinated5plus")]
        public double AdministeredToIllinois_Fully_5_Percent { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated12plus")]
        public int AdministeredToIllinois_Fully_12 { get; set; }

        [JsonProperty("Population_Fully_Vaccinated12plus")]
        public double AdministeredToIllinois_Fully_12_Percent { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated18plus")]
        public int AdministeredToIllinois_Fully_18 { get; set; }

        [JsonProperty("Population_Fully_Vaccinated18plus")]
        public double AdministeredToIllinois_Fully_18_Percent { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated65plus")]
        public int AdministeredToIllinois_Fully_65 { get; set; }

        [JsonProperty("Population_Fully_Vaccinated65plus")]
        public double AdministeredToIllinois_Fully_65_Percent { get; set; }

        [JsonProperty("Vaccinated5plusOneDose")]
        public int AdministeredToIllinois_One_5 { get; set; }

        [JsonProperty("Vaccinated5plusPercentOneDose")]
        public double AdministeredToIllinois_One_5_Percent { get; set; }

        [JsonProperty("Vaccinated12plusOneDose")]
        public int AdministeredToIllinois_One_12 { get; set; }

        [JsonProperty("Vaccinated12plusPercentOneDose")]
        public double AdministeredToIllinois_One_12_Percent { get; set; }

        [JsonProperty("Vaccinated18plusOneDose")]
        public int AdministeredToIllinois_One_18 { get; set; }

        [JsonProperty("Vaccinated18plusPercentOneDose")]
        public double AdministeredToIllinois_One_18_Percent { get; set; }

        [JsonProperty("Vaccinated65plusOneDose")]
        public int AdministeredToIllinois_One_65 { get; set; }

        [JsonProperty("Vaccinated65plusPercentOneDose")]
        public double AdministeredToIllinois_One_65_Percent { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated5plusCDC")]
        public int AdministeredToIllinoisans_Fully_5 { get; set; }

        [JsonProperty("Population_Fully_Vaccinated5plusCDC")]
        public double AdministeredToIllinoisans_Fully_5_Percent { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated12plusCDC")]
        public int AdministeredToIllinoisans_Fully_12 { get; set; }

        [JsonProperty("Population_Fully_Vaccinated12plusCDC")]
        public double AdministeredToIllinoisans_Fully_12_Percent { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated18plusCDC")]
        public int AdministeredToIllinoisans_Fully_18 { get; set; }

        [JsonProperty("Population_Fully_Vaccinated18plusCDC")]
        public double AdministeredToIllinoisans_Fully_18_Percent { get; set; }

        [JsonProperty("Persons_Fully_Vaccinated65plusCDC")]
        public int AdministeredToIllinoisans_Fully_65 { get; set; }

        [JsonProperty("Population_Fully_Vaccinated65plusCDC")]
        public double AdministeredToIllinoisans_Fully_65_Percent { get; set; }

        [JsonProperty("Vaccinated5plusOneDoseCDC")]
        public int AdministeredToIllinoisans_One_5 { get; set; }

        [JsonProperty("Vaccinated5plusPercentOneDoseCDC")]
        public double AdministeredToIllinoisans_One_5_Percent { get; set; }

        [JsonProperty("Vaccinated12plusOneDoseCDC")]
        public int AdministeredToIllinoisans_One_12 { get; set; }

        [JsonProperty("Vaccinated12plusPercentOneDoseCDC")]
        public double AdministeredToIllinoisans_One_12_Percent { get; set; }

        [JsonProperty("Vaccinated18plusOneDoseCDC")]
        public int AdministeredToIllinoisans_One_18 { get; set; }

        [JsonProperty("Vaccinated18plusPercentOneDoseCDC")]
        public double AdministeredToIllinoisans_One_18_Percent { get; set; }

        [JsonProperty("Vaccinated65plusOneDoseCDC")]
        public int AdministeredToIllinoisans_One_65 { get; set; }

        [JsonProperty("Vaccinated65plusPercentOneDoseCDC")]
        public double AdministeredToIllinoisans_One_65_Percent { get; set; }
    }
}
