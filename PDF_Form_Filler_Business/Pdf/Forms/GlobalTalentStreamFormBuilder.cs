using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Source;
using iText.Kernel.Pdf;
using PDF_Form_Filter_BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PDF_Form_Filler_Business.Pdf.Forms
{

    

    /// <summary>
    /// GLobal Talent Stream form implementation
    /// </summary>
    class GlobalTalentStreamFormBuilder : IGlobalTalentStreamFormBuilder
    {
        
        private PdfDocument _pdfDoc;
        private PdfAcroForm _form;

        //CheckBox Field Values;
        private string _checkedValue = "Yes";
        private string _uncheckedValue = "Off";
        private readonly Candidate_BO _candidate;
        private MemoryStream _pdfFilled;

        private enum GlobalTalentStreamRdGroup
        {
            Value_0 = 0,
            Value_1,
            Value_2,
            Value_3,
            Value_4,
            Value_5,
            Value_6,
            Value_7,
            Value_8,
            Value_9,
            Value_10
        };

        public GlobalTalentStreamFormBuilder(Candidate_BO candidate) {

            _candidate = candidate;

            //Get a instance of the form to work 
            PdfReader _pdfReader = new PdfReader(@"wwwroot\forms\ESDC-EMP5624.pdf");
            _pdfReader.SetUnethicalReading(true);

            _pdfFilled = new MemoryStream();
            _pdfDoc = new PdfDocument(_pdfReader, new PdfWriter(_pdfFilled));
            
            _form = PdfAcroForm.GetAcroForm(_pdfDoc, true);
        }
       
        public byte[] getForm()
        {
            //This line saves the form as non-editable 
            _form.FlattenFields();
            this._pdfDoc.Close();
            Clear();

            return this._pdfFilled.ToArray();
        }

        private void Clear() => _pdfDoc= null;

        //TODO: In the future include a map with all pdf fields' name. It will decrease the possibility of forget to change a field name in case of it changes on the pdf  

        public void BuildGlobalTalentStreamEligibility()
        {

            try
            {
                #region Global Talent Ocupatiion list - TFW Program

                if (this._candidate.globalTalentStreamElegibility.publishedTFWProgram)
                {
                    //checkbox skip to section 2
                    this._form.GetField("EMP5624_E[0].Page1[0].Yes_business[0]").SetValue(_checkedValue);
                    this._form.GetField("EMP5624_E[0].Page1[0].No_business[0]").SetValue(_uncheckedValue);

                    return;
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page1[0].Yes_business[0]").SetValue(_uncheckedValue);
                    this._form.GetField("EMP5624_E[0].Page1[0].No_business[0]").SetValue(_checkedValue);
                }

                #endregion

                #region Innovative Employer Referred  to the Global Talent Stream by an ESDC

                if (this._candidate.globalTalentStreamElegibility.inovativeEmployerESDC)
                {
                    this._form.GetField("EMP5624_E[0].Page1[0].Yes_inn[0]").SetValue(_checkedValue);
                    this._form.GetField("EMP5624_E[0].Page1[0].No_inn[0]").SetValue(_uncheckedValue);
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page1[0].Yes_inn[0]").SetValue(_uncheckedValue);
                    this._form.GetField("EMP5624_E[0].Page1[0].No_inn[0]").SetValue(_checkedValue);
                }
                #endregion

                #region Designed Referral Partner Contact Information
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_des_part[0]").SetValue(this._candidate.globalTalentStreamElegibility.organizationName ?? string.Empty);

                this._form.GetField("EMP5624_E[0].Page1[0].txtF_first_name[0]").SetValue(this._candidate.globalTalentStreamElegibility.partnerContactInformation.firstName ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_mid_name[0]").SetValue(this._candidate.globalTalentStreamElegibility.partnerContactInformation.middleName ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_last_name[0]").SetValue(this._candidate.globalTalentStreamElegibility.partnerContactInformation.lastName ?? string.Empty);

                var phoneFormat = $"{this._candidate.globalTalentStreamElegibility.partnerContactInformation.telephoneNumber}         {this._candidate.globalTalentStreamElegibility.partnerContactInformation.telephoneNumberExt}";

                this._form.GetField("EMP5624_E[0].Page1[0].txtF_phone_number[0]").SetValue(phoneFormat);

                phoneFormat = $"{this._candidate.globalTalentStreamElegibility.partnerContactInformation.alternativeTelephoneNumber}          {this._candidate.globalTalentStreamElegibility.partnerContactInformation.alternativeTelephoneNumberExt}";

                this._form.GetField("EMP5624_E[0].Page1[0].txtF_alternate_phone[0]").SetValue(phoneFormat);
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_fax_number[0]").SetValue(this._candidate.globalTalentStreamElegibility.partnerContactInformation.faxNumber ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_Email[0]").SetValue(this._candidate.globalTalentStreamElegibility.partnerContactInformation.emailAddress ?? string.Empty);

                //TODO: Validate this information
                //Oral communication
                if (this._candidate.globalTalentStreamElegibility.partnerContactInformation.oralCommunicationEnglish)
                {
                    //English Language
                    this._form.GetField("EMP5624_E[0].Page1[0].rb_language_oral[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                else
                {
                    //French language
                    this._form.GetField("EMP5624_E[0].Page1[0].rb_language_oral[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }

                //Written communication
                if (this._candidate.globalTalentStreamElegibility.partnerContactInformation.writtenCommunicationEnglish)
                {
                    this._form.GetField("EMP5624_E[0].Page1[0].rb_language_written[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page1[0].rb_language_written[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }

                #endregion

                #region Alternative Partner Contact Information

                this._form.GetField("EMP5624_E[0].Page1[0].txtF_first_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.firstName ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_mid_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.middleName ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_last_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.lastName ?? string.Empty);

                phoneFormat = $"{this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.telephoneNumber}          {this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.telephoneNumberExt}";

                this._form.GetField("EMP5624_E[0].Page1[0].txtF_phone_number2[0]").SetValue(phoneFormat);

                phoneFormat = $"{this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.alternativeTelephoneNumber}         {this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.alternativeTelephoneNumberExt}";

                this._form.GetField("EMP5624_E[0].Page1[0].txtF_alternate_phone2[0]").SetValue(phoneFormat);
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_fax_number2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.faxNumber ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page1[0].txtF_Email2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.emailAddress ?? string.Empty);


                //Oral communication
                if (this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.oralCommunicationEnglish)
                {
                    //English Language
                    this._form.GetField("EMP5624_E[0].Page1[0].rb_language_oral2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                else
                {
                    //French language
                    this._form.GetField("EMP5624_E[0].Page1[0].rb_language_oral2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }

                //Written communication
                if (this._candidate.globalTalentStreamElegibility.alternativePartnerContactInformation.writtenCommunicationEnglish)
                {
                    this._form.GetField("EMP5624_E[0].Page1[0].rb_language_written2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page1[0].rb_language_written2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }
                #endregion
            }
            finally {}
        }

        public void BuildEmployerBusinessInformation()
        {
            try
            {
                this._form.GetField("EMP5624_E[0].Page2[0].num_Company_Code[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.canadaRevenueAgencyPayrollAccountNumber ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].num_Company_Code[1]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.canadaRevenueAgencyPayrollAccountNumberDigit ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Emp_Legal[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.legalName ?? string.Empty);

                #region Address
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Mail_Adress1[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.addressLineOne??string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Mail_Adress2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.addressLineTwo?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_City[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.city ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Province[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.provinceTerritoryState ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Country[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.country?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Postal_Code[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.zipCode ?? string.Empty);
                #endregion

                #region mailing address 
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Mail_Adress2-1[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.mailingAddressLineOne?? string.Empty); 
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Mail_Adress2-2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.mailingAddressLineTwo?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_City2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.mailingCity ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Province2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.mailingProvinceTerritoryState?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Country2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.mailingCountry ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Postal_Code2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.zipCode ?? string.Empty);
                #endregion

                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Employer_Web_Address[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.website ?? string.Empty);

                string dateBusiness = $"{this._candidate.globalTalentStreamElegibility.employerBusinesInformation.dateBusinessStarted:yyyy-MM-dd}";
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Employer_Date_Business[0]").SetValue(dateBusiness);

                #region Oranization type
                this._form.GetField("EMP5624_E[0].Page2[0].cb_society[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.businessStructureCorporation ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page2[0].cb_partnership[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.businessStructurePartnerShip ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page2[0].cb_sole_propietor[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.businessStructureCoOperation ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page2[0].cb_individual[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.businessStructureSoleProprietorShip ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page2[0].cb_not_profit[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.businessStructureNonProft ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page2[0].cb_registred[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.businessStructureRegisteredClarity ? _checkedValue : _uncheckedValue);
                #endregion

                this._form.GetField("EMP5624_E[0].Page2[0].txtF_amout_employees[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.numberEmployees.ToString());
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_business_revenu[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.annualGross.ToString());

                #region Support through Employment  and Social Development Canada's Work-Sharing Program 

                if (this._candidate.globalTalentStreamElegibility.employerBusinesInformation.supportWorkSharingProgram)
                {
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_receive_prog[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                    this._form.GetField("EMP5624_E[0].Page2[0].txtF_If_Yes2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerBusinesInformation.supportWorkSharingProgramDetails ?? string.Empty);
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_receive_prog[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                #endregion
            }
            finally{}
        }

        public void BuildEmployerContactInformation()
        {
            try
            {
                #region Principal Employer Contact Information 
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_position_title[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerContactInformation.jobTitle ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_first_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerContactInformation.firstName ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_mid_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerContactInformation.middleName ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_last_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerContactInformation.lastName ?? string.Empty);

                var phoneFormat = $"{this._candidate.globalTalentStreamElegibility.employerContactInformation.telephoneNumber}          {this._candidate.globalTalentStreamElegibility.employerContactInformation.telephoneNumberExt}";

                this._form.GetField("EMP5624_E[0].Page2[0].txtF_phone_number2[0]").SetValue(phoneFormat);

                phoneFormat = $"{this._candidate.globalTalentStreamElegibility.employerContactInformation.alternativeTelephoneNumber}          {this._candidate.globalTalentStreamElegibility.employerContactInformation.alternativeTelephoneNumberExt}";

                this._form.GetField("EMP5624_E[0].Page2[0].txtF_alternate_phone2[0]").SetValue(phoneFormat);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_fax_number2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerContactInformation.faxNumber ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_Email2[0]").SetValue(this._candidate.globalTalentStreamElegibility.employerContactInformation.emailAddress ?? string.Empty);

                #region Oral Communication 

                if (this._candidate.globalTalentStreamElegibility.employerContactInformation.oralCommunicationEnglish)
                {
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_language_oral2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                else
                {
                    //French language
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_language_oral2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }

                #endregion

                #region Written communication

                if (this._candidate.globalTalentStreamElegibility.employerContactInformation
                    .writtenCommunicationEnglish)
                {
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_language_written2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_language_written2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }
                #endregion

                #endregion

                #region Alternate Employer Contact Information 
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_other_position_title[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.jobTitle ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_other_first_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.firstName ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_other_mid_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.middleName ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_other_last_name2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.lastName ?? string.Empty);

                phoneFormat = $"{this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.telephoneNumber}          {this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.telephoneNumberExt}";

                this._form.GetField("EMP5624_E[0].Page2[0].txtF_other_phone_number2[0]").SetValue(phoneFormat);

                phoneFormat = $"{this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.alternativeTelephoneNumber}          {this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.alternativeTelephoneNumberExt}";

                this._form.GetField("EMP5624_E[0].Page2[0].txtF_other_alternate_phone2[0]").SetValue(phoneFormat);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_other_fax_number2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.faxNumber ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page2[0].txtF_other_Email2[0]").SetValue(this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.emailAddress ?? string.Empty);

                #region Oral Communication 

                if (this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation.oralCommunicationEnglish)
                {
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_other_language_oral2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                else
                {
                    //French language
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_other_language_oral2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }

                #endregion

                #region Written communication

                if (this._candidate.globalTalentStreamElegibility.alternativeEmployerContactInformation
                    .writtenCommunicationEnglish)
                {
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_other_anguage_written2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page2[0].rb_other_anguage_written2[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }
                #endregion

                #endregion

            }
            finally {}
        }

        public void BuildThirdPartInformation()
        {
            //TODO this information is filled out
            throw new NotImplementedException();
        }

        public void BuildJobOfferDetails()
        {
            try
            {
                #region Job offer Details 
                this._form.GetField("EMP5624_E[0].Page3[0].txtF_position_title[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.jobTitle ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page3[0].txtF_code[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.noc.ToString());
                this._form.GetField("EMP5624_E[0].Page3[0].txtF_position_title[1]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.TFWRequest ?? string.Empty);
                this._form.GetField("EMP5624_E[0].Page4[0].txtF_MainDuties[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.jobDuties ?? string.Empty);

                
                var dateStart = $"{this._candidate.globalTalentStreamElegibility.jobOfferDetails.employmentStartDate:yyyy-MM-dd}";

                this._form.GetField("EMP5624_E[0].Page4[0].txtF_Date_E[0]").SetValue(dateStart);

                this._form.GetField("EMP5624_E[0].Page4[0].txtF_number_days[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.employmentDurationNumberOfDays.ToString());
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_english1[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.employmentDurationDays ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_english1[1]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.employmentDurationWeeks ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_english1[2]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.employmentDurationMonths ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_english1[3]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.employmentDurationYears ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page4[0].txtF_justification[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.employmentDurationRationale ?? string.Empty);

                if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.positionPartUnion)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].rb_job_syndic[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].rb_job_syndic[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }

                #region Language requirements 
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_NoLanguage[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.noAbilityToCommunicate ? _checkedValue : _uncheckedValue);

                //Communicate orally
                if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.orallyCommunicationEnglish ||
                    this._candidate.globalTalentStreamElegibility.jobOfferDetails.orallyCommunicationFrench || 
                    this._candidate.globalTalentStreamElegibility.jobOfferDetails.orallyCommunicationEnglishAndFrench)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].chkB_OrallyIn[0]").SetValue(_checkedValue);
                }

                this._form.GetField("EMP5624_E[0].Page4[0].chkB_english1[4]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.orallyCommunicationEnglish ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_French1[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.orallyCommunicationFrench ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_English_French1[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.orallyCommunicationEnglishAndFrench ? _checkedValue : _uncheckedValue);

                //Communicate writing 
                if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.writingCommunicationEnglish ||
                    this._candidate.globalTalentStreamElegibility.jobOfferDetails.writingCommunicationFrench || 
                    this._candidate.globalTalentStreamElegibility.jobOfferDetails.writingCommunicationEnglishAndFrench)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].chkB_TheOffer[0]").SetValue(_checkedValue);
                }
                
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_english2[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.writingCommunicationEnglish ? _checkedValue:_uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_french2[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.writingCommunicationFrench ? _checkedValue : _uncheckedValue);
                this._form.GetField("EMP5624_E[0].Page4[0].chkB_English_French2[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.writingCommunicationEnglishAndFrench ? _checkedValue : _uncheckedValue);

                //Communicate using other language rather than English or French 
                 if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.abilityToCommunicateOtherLanguage)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].chkB_TheOffer2[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.abilityToCommunicateOtherLanguage ? _checkedValue : _uncheckedValue);

                    this._form.GetField("EMP5624_E[0].Page4[0].txtF_ProvideDetails[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.abilityToCommunicateOtherLanguageDetails);
                }

                #endregion

                #region Minimum education requirements to the job 


                //This is a checkbox group 
                if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationDoctoratePhd)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationBachelor)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_3).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationTradeDiploma)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_6).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.noEducationRequirement)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_9).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationDoctorOfMedicine)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationCollegeDiploma)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_4).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationSecundarySchool)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_7).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationMasterDegree)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_2).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationApprendiceshipDiploma)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_5).ToString());
                }

                else if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationVocationalDiploma)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].RadioButtonList[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_8).ToString());
                }

                this._form.GetField("EMP5624_E[0].Page4[0].txtF_AdditionalInformation[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.educationAdditionalInformation);
                #endregion

                this._form.GetField("EMP5624_E[0].Page4[0].txtF_MainDuties[1]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.experienceSkillsNeeded ?? string.Empty);

                #region Tried to recruit Canadians/Permanent Residents

                //TODO: check if two option can be filled out at the same time 
                if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.triedRecruitCanadians)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].Yes_E[0]").SetValue(_checkedValue);
                    this._form.GetField("EMP5624_E[0].Page4[0].txtF_AdditionalInformation[1]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.triedRecruitCanadiansExplanation?? string.Empty);
                }

                if (!this._candidate.globalTalentStreamElegibility.jobOfferDetails.triedRecruitCanadians)
                {
                    this._form.GetField("EMP5624_E[0].Page4[0].No_E[0]").SetValue(_checkedValue);
                    this._form.GetField("EMP5624_E[0].Page4[0].txtF_No_AdditionalInformation[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.triedRecruitCanadiansExplanation?? string.Empty);
                }

                #endregion


                #region Were any employees working in the postion being requested

                if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.wereWorkersInThePosition)
                {
                    this._form.GetField("EMP5624_E[0].Page5[0].rb_canadian_empl[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                    this._form.GetField("EMP5624_E[0].Page5[0].txtF_canadian_how_many[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.numberOfCanadianWorkers.ToString());
                    this._form.GetField("EMP5624_E[0].Page5[0].txtF_TET_how_many[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.numberOfTFWWorkers.ToString());
                }
                else
                {
                    this._form.GetField("EMP5624_E[0].Page5[0].rb_canadian_empl[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }

                this._form.GetField("EMP5624_E[0].Page5[0].txtF_info[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.reasonLayoffPosition ?? string.Empty);

                #endregion


                #region impacts of contract a TFW

                if (this._candidate.globalTalentStreamElegibility.jobOfferDetails.isTHereImpactOfHiringTFW)
                {
                    this._form.GetField("EMP5624_E[0].Page5[0].rb_TET_empl[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_1).ToString());
                    this._form.GetField("EMP5624_E[0].Page5[0].txtF_No_add_info[0]").SetValue(this._candidate.globalTalentStreamElegibility.jobOfferDetails.impactOfHiringTFW ?? string.Empty);
                }
                
                else
                {
                    this._form.GetField("EMP5624_E[0].Page5[0].rb_TET_empl[0]").SetValue(((int)GlobalTalentStreamRdGroup.Value_0).ToString());
                }

                #endregion

                #endregion

            }
            finally
            {

            }
        }

        public void BuildCompensatonAndBenefits()
        {
            throw new NotImplementedException();
        }

        public void BuildWorkLocation()
        {
            throw new NotImplementedException();
        }

        public void BuildLabourMarketBenefits()
        {
            throw new NotImplementedException();
        }

        public void BuildSMandatoryLabourMarketBenefit()
        {
            throw new NotImplementedException();
        }

        public void BuildComplementaryLabourMarketBenefits()
        {
            throw new NotImplementedException();
        }

        public void BuildSignatureOfEmployer()
        {
            throw new NotImplementedException();
        }

        public void BuildLabourMarketImpact()
        {
            throw new NotImplementedException();
        }

        public void BuildTemporaryForeingWork()
        {
            throw new NotImplementedException();
        }
    }

    
}
