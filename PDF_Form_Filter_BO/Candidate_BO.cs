using Newtonsoft.Json;
using PDF_Form_Filter_BO.GlobalTalentStream;
using System;

namespace PDF_Form_Filter_BO
{
    public class Candidate_BO
    {
        public int id;
        public string firstName;
        public string middleName;
        public string lastName;
        public string country;
        [JsonProperty("GlobalTalentStreamElegibility")]
        public GlobalTalentStreamElegibility_BO globalTalentStreamElegibility;
    }
}
