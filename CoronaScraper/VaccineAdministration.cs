using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoronaScraper
{
    [JsonObject]
    public class Administration : IEnumerable<VaccineAdministration>
    {

        [JsonProperty("lastUpdatedDate")]
        public LastUpdatedDate LastUpdatedDate { get; set; }

        [JsonProperty("VaccineAdministration")]
        public IList<VaccineAdministration> VaccineAdministrations { get; set; }

        [JsonProperty("CurrentVaccineAdministration")]
        public VaccineAdministration CurrentVaccineAdministration { get; set; }

        #region Implementation of IEnumerable
        public IEnumerator<VaccineAdministration> GetEnumerator()
        {
            foreach(VaccineAdministration administation in VaccineAdministrations)
            {
                yield return administation;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        public VaccineAdministration this[int index] => VaccineAdministrations[index];
        public VaccineAdministration this[Index index] => VaccineAdministrations[index];
        public VaccineAdministration this[DateTime date]
        {
            get
            {
                foreach(VaccineAdministration administration in VaccineAdministrations)
                {
                    if(date == administration.Report_Date)
                    {
                        return administration;
                    }
                }
                return null;
            }
        }
    }

    public class VaccineAdministration
    {
        [JsonProperty("CountyName")]
        public string CountyName { get; set; }

        [JsonProperty("AdministeredCount")]
        public int AdministeredCount { get; set; }

        [JsonProperty("AdministeredCountChange")]
        public int AdministeredCountChange { get; set; }

        [JsonProperty("AdministeredCountRollAvg")]
        public double AdministeredCountRollAvg { get; set; }

        [JsonProperty("AllocatedDoses")]
        public int AllocatedDoses { get; set; }

        [JsonProperty("PersonsFullyVaccinated")]
        public int PersonsFullyVaccinated { get; set; }

        [JsonProperty("PersonsFullyVaccinatedChange")]
        public int PersonsFullyVaccinatedChange { get; set; }

        [JsonProperty("PersonsVaccinatedOneDose")]
        public int PersonsVaccinatedOneDose { get; set; }

        [JsonProperty("PersonsVaccinatedOneDoseChange")]
        public int PersonsVaccinatedOneDoseChange { get; set; }

        [JsonProperty("BoosterDoseAdministered")]
        public int BoosterDoseAdministered { get; set; }

        [JsonProperty("BoosterDoseAdministeredChange")]
        public int BoosterDoseAdministeredChange { get; set; }

        [JsonProperty("Report_Date")]
        public DateTime Report_Date { get; set; }

        [JsonProperty("Population")]
        public int Population { get; set; }

        [JsonProperty("PctVaccinatedPopulation")]
        public double PctVaccinatedPopulation { get; set; }

        [JsonProperty("PctVaccinatedOneDosePopulation")]
        public double PctVaccinatedOneDosePopulation { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("LHDReportedInventory")]
        public int LHDReportedInventory { get; set; }

        [JsonProperty("CommunityReportedInventory")]
        public int CommunityReportedInventory { get; set; }

        [JsonProperty("TotalReportedInventory")]
        public int TotalReportedInventory { get; set; }

        [JsonProperty("InventoryReportDate")]
        public DateTime InventoryReportDate { get; set; }
    }
}
