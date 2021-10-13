using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CoronaScraper
{
    [JsonObject]
    public class Root : IEnumerable<CountyDemographic>
    {
        [JsonProperty("lastUpdatedDate")]
        public LastUpdatedDate LastUpdatedDate { get; set; }
        [JsonProperty("county_demographics")]
        public List<CountyDemographic> CountyDemographics { get; set; } = new();
        public DateTime UpdatedDate => LastUpdatedDate.DateTime;

        #region Implementation of IEnumerable

        public IEnumerator<CountyDemographic> GetEnumerator()
        {
            foreach (CountyDemographic demographic in CountyDemographics)
            {
                yield return demographic;
            }
            // return CountyDemographics.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public CountyDemographic this[int index] => CountyDemographics[index];
        public CountyDemographic this[string name]
        {
            get
            {
                foreach (CountyDemographic demographic in CountyDemographics)
                {
                    if (demographic.County.ToLower() == name.ToLower())
                    {
                        return demographic;
                    }
                }
                return null;
            }
        }
    }
    
    public class Race
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("count")]
        public int ConfirmedCases { get; set; }

        [JsonProperty("tested")]
        public int Tested { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public class Age
    {
        [JsonProperty("race")]
        public List<Race> Race { get; set; }

        [JsonProperty("age_group")]
        public string AgeGroup { get; set; }

        [JsonProperty("count")]
        public int ConfirmedCases { get; set; }

        [JsonProperty("tested")]
        public int Tested { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }
    }

    public class Gender
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("count")]
        public int ConfirmedCases { get; set; }

        [JsonProperty("tested")]
        public int Tested { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public class Demographics
    {
        [JsonProperty("age")]
        public List<Age> Age { get; set; }

        [JsonProperty("race")]
        public List<Race> Race { get; set; }

        [JsonProperty("gender")]
        public List<Gender> Gender { get; set; }
    }

    public class CountyDemographic
    {
        [JsonProperty("County")]
        public string County { get; set; }

        [JsonProperty("confirmed_cases")]
        public int ConfirmedCases { get; set; }

        [JsonProperty("total_tested")]
        public int Tested { get; set; }

        [JsonProperty("demographics")]
        public Demographics Demographics { get; set; }
    }
}
