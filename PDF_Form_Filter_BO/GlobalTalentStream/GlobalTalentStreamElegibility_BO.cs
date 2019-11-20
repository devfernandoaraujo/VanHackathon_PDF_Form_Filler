using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Form_Filter_BO.GlobalTalentStream
{
    public class GlobalTalentStreamElegibility_BO
    {
        public bool publishedTFWProgram;
        public bool inovativeEmployerESDC;
        public string organizationName;
        [JsonProperty("PartnerContactInformation")]
        public GlobalTalentStreamContactInformation_BO partnerContactInformation;
        [JsonProperty("AlternativePartnerContactInformation")]
        public GlobalTalentStreamContactInformation_BO alternativePartnerContactInformation;
        [JsonProperty("EmployerBusinesInformation")]
        public GlobalTalentStreamEmployerBusinesInformation_BO employerBusinesInformation;
        [JsonProperty("EmployerContactInformation")]
        public GlobalTalentStreamEmployerContactInformation_BO employerContactInformation;
        [JsonProperty("AlternativeEmployerContactInformation")]
        public GlobalTalentStreamEmployerContactInformation_BO alternativeEmployerContactInformation;
        [JsonProperty("JobOfferDetails")]
        public GlobalTalentStreamJobOfferDetails jobOfferDetails;
    }
}
