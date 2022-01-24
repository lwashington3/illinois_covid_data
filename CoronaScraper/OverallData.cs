using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CoronaScraper
{
    [JsonObject]
    public class OverallData : IEnumerable<IllinoisTestingResults>
    {
        [JsonProperty("lastUpdatedDate")]
        private LastUpdatedDate LastUpdatedDate { get; set; }
        public DateTime UpdatedDate => LastUpdatedDate.DateTime;
        [JsonProperty("state_testing_results")]
        public List<IllinoisTestingResults> StateTestingResults { get; set; } = new ();
        #region Implementation of IEnumerable

        public IEnumerator<IllinoisTestingResults> GetEnumerator()
        {
            foreach (IllinoisTestingResults result in StateTestingResults)
            {
                yield return result;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public IllinoisTestingResults this[int index] => StateTestingResults[index];
        public IllinoisTestingResults this[Index index] => StateTestingResults[index];
        public IllinoisTestingResults this[DateTime date]
        {
            get
            {
                foreach (IllinoisTestingResults result in StateTestingResults)
                {
                    if (result.TestDate == date)
                    {
                        return result;
                    }
                }
                return null;
            }
        }
    }
    public class LastUpdatedDate
    {
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("month")]
        public int Month { get; set; }
        [JsonProperty("day")]
        public int Day { get; set; }
        public DateTime DateTime => new (Year, Month, Day);
    }

    public class IllinoisTestingResults
    {
        [JsonProperty("testDate")]
        public DateTime TestDate { get; set; }

        [JsonProperty("total_tested")]
        public int TotalTested { get; set; } = 0;

        [JsonProperty("confirmed_cases")]
        public int ConfirmedCases { get; set; } = 0;

        [JsonProperty("deaths")]
        public int Deaths { get; set; } = 0;

        [JsonProperty("tested_change")]
        public int TestedChange { get; set; } = 0;

        [JsonProperty("cases_change")]
        public int CasesChange { get; set; } = 0;

        [JsonProperty("deaths_change")]
        public int DeathsChange { get; set; } = 0;

        [JsonProperty("tested_7_day_rolling_avg")]
        public double Tested7DayRollingAvg { get; set; } = 0;

        [JsonProperty("cases_7_day_rolling_avg")]
        public double Cases7DayRollingAvg { get; set; } = 0;

        [JsonProperty("deaths_7_day_rolling_avg")]
        public double Deaths7DayRollingAvg { get; set; } = 0;
    }
}
