using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{

    public class AdminController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        public ActionResult Index()
        {
            using (InsuranceEntities db = new InsuranceEntities())
            {
                var insurees = db.Insurees;

                var insureesVM = new List<Insuree>();

                foreach (var insuree in insurees)
                {
                    var insureeVM = new Insuree();
                    insureeVM.FirstName = insuree.FirstName;
                    insureeVM.LastName = insuree.LastName;
                    insureeVM.EmailAddress = insuree.EmailAddress;
                    insureeVM.Quote = insuree.Quote;
                    insureesVM.Add(insuree);
                }

                return View(insureesVM);
            }
        }
    }
}