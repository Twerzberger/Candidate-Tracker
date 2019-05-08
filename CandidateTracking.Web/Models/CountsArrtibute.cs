using CandidateTracking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateTracking.Web.Models
{
    public class CountsArrtibute : ActionFilterAttribute
    {
        private string _connectionString;

        public CountsArrtibute(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var rep = new CTReposetory(_connectionString);

            var controller = (Controller)context.Controller;
            controller.ViewBag.Pending = rep.GetPendingCount();
            controller.ViewBag.Confirmed = rep.GetConfirmedCount();
            controller.ViewBag.Declined = rep.GetDeclinedCount();

            base.OnActionExecuted(context);
        }
    }   
    
}
