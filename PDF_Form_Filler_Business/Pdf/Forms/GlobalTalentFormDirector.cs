using PDF_Form_Filter_BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Form_Filler_Business.Pdf.Forms
{
    /// <summary>
    /// e Director class to the Global Talent Stream Form
    /// </summary>
    class GlobalTalentFormDirector
    {
        private readonly IGlobalTalentStreamFormBuilder _globalTalentStreamFormBuilder;
        public GlobalTalentFormDirector(IGlobalTalentStreamFormBuilder globalTalentStreamFormBuilder)
        {
            this._globalTalentStreamFormBuilder = globalTalentStreamFormBuilder;
        }

        public void BuildForm() {
            this._globalTalentStreamFormBuilder.BuildGlobalTalentStreamEligibility();
            this._globalTalentStreamFormBuilder.BuildEmployerBusinessInformation();
            this._globalTalentStreamFormBuilder.BuildEmployerContactInformation();
            //this._globalTalentStreamFormBuilder.BuildThirdPartInformation(); //TODO: this part is filled out 
            this._globalTalentStreamFormBuilder.BuildJobOfferDetails();
        }
    }
}
