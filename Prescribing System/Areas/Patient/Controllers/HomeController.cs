﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Patient.Models;

namespace Prescribing_System.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {
        //CHECKS IF THE USER'S ROLE AGAINST THE ROLE IN THE CURRENT AREA
        public PatientDbcontext Data = new PatientDbcontext();
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        public IActionResult Index()
        {
            //GETS THE USER THAT'S STORED IN THE STATIC CLASS "UserSingleton"
            IndexViewModel model = new IndexViewModel()
            {
                LoggedUser = UserSingleton.GetLoggedUser(),
                User = Data.GetPatientWithId(UserSingleton.GetLoggedUser().UserId),
                Patient = Data.GetPatientWithId(UserSingleton.GetLoggedUser().UserId),
            };
            if (UserIsVerified("Patient"))
                return View(model);
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
    
    
}
