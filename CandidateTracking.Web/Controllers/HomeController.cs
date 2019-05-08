using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CandidateTracking.Web.Models;
using CandidateTracking.Data;
using Microsoft.Extensions.Configuration;

namespace CandidateTracking.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult AddCandidate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCandidate(Candidate candidate)
        {
            candidate = new Candidate()
            {
                Name = candidate.Name,
                Phone = candidate.Phone,
                Email = candidate.Email,
                Notes = candidate.Notes,
                Pending = true
            };
            var rep = new CTReposetory(_connectionString);
            rep.AddCandidate(candidate);   

            return Redirect("/");
        }

        public IActionResult ViewPending()
        {
            var rep = new CTReposetory(_connectionString);
            var pen = rep.GetPending();

            return View(pen);
        }

        public IActionResult ViewConfirmed()
        {
            var rep = new CTReposetory(_connectionString);
            var pen = rep.GetConfirmed();

            return View(pen);
        }

        public IActionResult ViewDeclined()
        {
            var rep = new CTReposetory(_connectionString);
            var pen = rep.GetDeclined();

            return View(pen);
        }

        public IActionResult ViewCandidate(int id)
        {
            var rep = new CTReposetory(_connectionString);
            var vm = new CandidateTrackingViewModel();
            vm.Candidate = rep.GetCandidate(id);

            return View(vm);
        }

        public IActionResult Confirmed(int id)
        {
            var rep = new CTReposetory(_connectionString);
            rep.UpdateToConfirmed(id);

            var ids = new
            {
                Pending = rep.GetPendingCount(),
                Confirmed = rep.GetConfirmedCount(),
                Declined = rep.GetDeclinedCount()
            };
            return Json(ids);
        }

        public IActionResult Declined(int id)
        {
            var rep = new CTReposetory(_connectionString);
            rep.UpdateToDeclined(id);

            var ids = new
            {
                Pending = rep.GetPendingCount(),
                Confirmed = rep.GetConfirmedCount(),
                Declined = rep.GetDeclinedCount()
            };
            return Json(ids);
        }

    }
}
