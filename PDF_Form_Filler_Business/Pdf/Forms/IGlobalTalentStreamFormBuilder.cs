using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Form_Filler_Business.Pdf.Forms
{
    public interface  IGlobalTalentStreamFormBuilder
    {
        /// <summary>
        /// This refer to the first section of the Global talent Stream Form
        /// </summary>
        void BuildGlobalTalentStreamEligibility();
        /// <summary>
        /// This refer to the second section of the Global talent Stream Form
        /// </summary>
        void BuildEmployerBusinessInformation();
        /// <summary>
        /// This refer to the third section of the Global talent Stream Form
        /// </summary>
        void BuildEmployerContactInformation();
        /// <summary>
        /// This refer to the fourth section of the Global talent Stream Form
        /// </summary>
        void BuildThirdPartInformation();
        /// <summary>
        /// This refer to the fiveth section of the Global talent Stream Form
        /// </summary>
        void BuildJobOfferDetails();
        /// <summary>
        /// This refer to the sixth section of the Global talent Stream Form
        /// </summary>
        void BuildCompensatonAndBenefits();
        /// <summary>
        /// This refer to the seventh section of the Global talent Stream Form
        /// </summary>
        void BuildWorkLocation();
        /// <summary>
        /// This refer to the eighth section of the Global talent Stream Form
        /// </summary>
        void BuildLabourMarketBenefits();
        /// <summary>
        /// This refer to the ninth section of the Global talent Stream Form
        /// </summary>
        void BuildSMandatoryLabourMarketBenefit();
        /// <summary>
        /// This refer to the tenth section of the Global talent Stream Form
        /// </summary>
        void BuildComplementaryLabourMarketBenefits();
        /// <summary>
        /// This refer to the eleventh section of the Global talent Stream Form
        /// </summary>
        void BuildSignatureOfEmployer();
        /// <summary>
        /// This refer to the Labour Market Impact of the Global talent Stream Form
        /// </summary>
        void BuildLabourMarketImpact();
       
        /// <summary>
        /// This refer to the work of Temporary Foreing Work Information Template of the Global talent Stream Form
        /// </summary>
        void BuildTemporaryForeingWork();
        /// <summary>
        /// Return the form filled out 
        /// </summary>
        byte[] getForm();
    }
}
