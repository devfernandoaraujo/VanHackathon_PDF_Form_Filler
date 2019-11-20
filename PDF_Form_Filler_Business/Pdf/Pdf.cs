using PDF_Form_Filler_Business.Pdf.Forms;
using PDF_Form_Filter_BO;
using System;


namespace PDF_Form_Filler_Business.Pdf
{
    public class Pdf : IPdf
    {
        public byte[] fill(object item)
        {
            
            IGlobalTalentStreamFormBuilder formBuilder = new GlobalTalentStreamFormBuilder((Candidate_BO)item);
            GlobalTalentFormDirector formDirector = new GlobalTalentFormDirector(formBuilder);
            formDirector.BuildForm();
            return formBuilder.getForm();


        }
    }
}
