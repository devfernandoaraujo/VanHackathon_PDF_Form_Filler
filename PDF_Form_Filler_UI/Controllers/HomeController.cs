using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PDF_Form_Filler_Business;
using PDF_Form_Filler_Business.Pdf;
using PDF_Form_Filler_UI.Models;
using PDF_Form_Filter_BO;

namespace PDF_Form_Filler_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICandidates _candidates;
        private IPdf _pdf;

        public HomeController(ILogger<HomeController> logger, ICandidates candidates, IPdf pdf)
        {
            _logger = logger;
            _candidates = candidates;
            _pdf = pdf;
        }

        public IActionResult Index()
        {
            var d = _candidates.getAllCandidates();
            return View();
        }

        /// <summary>
        /// Generate a list with candidates for the Global Talent Stream Application
        /// </summary>
        /// <returns>A json with a list of all candidates</returns>
        public async Task<JsonResult> getAllCandidates() 
        {
            List<Candidate_BO> candidates = null;

            var t = Task.Run(() => candidates  = this._candidates.getAllCandidates());
            await Task.WhenAll(t);
            return Json(candidates);
        }

        public async Task<IActionResult> generateFormApplication(int candidateId) {

            Candidate_BO candidate = null;

            Byte[] pdf = null;

            var t = Task.Run(() => candidate = this._candidates.getCandidateById(candidateId));

            var t1 = t.ContinueWith((tAnt) => pdf = _pdf.fill(candidate));

            await Task.WhenAll(t,t1);

            return File(pdf, "application/pdf",$"{candidate.firstName}{candidate.lastName}.pdf");
        }

    }
}
