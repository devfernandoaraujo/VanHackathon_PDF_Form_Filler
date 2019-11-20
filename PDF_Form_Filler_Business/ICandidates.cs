using PDF_Form_Filter_BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Form_Filler_Business
{
    public interface ICandidates
    {
        List<Candidate_BO> getAllCandidates();
        Candidate_BO getCandidateById(int Id);
    }
}
