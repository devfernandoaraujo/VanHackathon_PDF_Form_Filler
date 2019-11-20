using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PDF_Form_Filter_BO;

namespace PDF_Form_Filler_Business
{

    /// <summary>
    /// This class is used as source to read a json file with all information that must be filled out in the pdf
    /// </summary>
    public class Candidates : ICandidates
    {
        /// <summary>
        /// Get the list of candidates 
        /// </summary>
        /// <returns>List of Candidates </returns>
        public List<Candidate_BO> getAllCandidates() {

            List<Candidate_BO> candidates;
            using(StreamReader r = new StreamReader(@"wwwroot\resources\database.json")){
                string json = r.ReadToEnd();

                candidates = JsonConvert.DeserializeObject<List<Candidate_BO>>(json);
            }

            return candidates;
        }
        /// <summary>
        /// Get a candidate
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Candidate</returns>
        public Candidate_BO getCandidateById(int Id)
        {
            Candidate_BO candidate;

            using(StreamReader r = new StreamReader(@"wwwroot\resources\database.json")){
                string json = r.ReadToEnd();

                List< Candidate_BO> candidates = JsonConvert.DeserializeObject<List<Candidate_BO>>(json);

                candidate = candidates.Where(x => x.id == Id).FirstOrDefault();
            }

            return candidate;

        }
    }
}
