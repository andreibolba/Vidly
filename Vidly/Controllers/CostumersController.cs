using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
using Vidly.Models.IdentityModels;
using System.Runtime.Caching;
using Vidly.DataAccess;

namespace Vidly.Controllers
{
    public class CostumersController : Controller
    {
        private EntityFrameworkCostumerProvider providerCostumers;
        private EntityFrameworkMembershipTypeProvider providerMembership;

        public CostumersController()
        {
            providerCostumers = new EntityFrameworkCostumerProvider();
            providerMembership = new EntityFrameworkMembershipTypeProvider();

        }

        protected override void Dispose(bool disposing)
        {
            providerCostumers.Dispose();
            providerMembership.Dispose();

        }

        public ActionResult AllCostumers()
        {
            return View();
        }

        [Route("Costumers/GetCostumer/{id}")]
        public ActionResult GetCostumer(int id)
        {
            var costumer = providerCostumers.GetCostumer(id);

            if (costumer == null)
                return HttpNotFound();

            return View(costumer);
        }

        // GET: Costumers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new CostumerFormViewModel()
            {
                MembershipTypes = providerMembership.GetMembershipTypes().ToList(),
                Costumer = new Models.Costumer()
            };
            return View("CostumerForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Models.Costumer costumer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CostumerFormViewModel()
                {
                    MembershipTypes = providerMembership.GetMembershipTypes().ToList(),
                    Costumer = costumer
                };
                return View("CostumerForm", viewModel);
            }
            if (costumer.Id == 0)
                providerCostumers.AddCostumer(costumer);
            else
                providerCostumers.UpdateCostumer(costumer);
            return RedirectToAction("AllCostumers", "Costumers");
        }

        public ActionResult Edit(int id)
        {
            var costumer = providerCostumers.GetCostumer(id);
            if (costumer == null)
                costumer = new Models.Costumer();
            var viewModel = new CostumerFormViewModel()
            {
                MembershipTypes = providerMembership.GetMembershipTypes().ToList(),
                Costumer = costumer
            };
            return View("CostumerForm",viewModel);
        }
    }
}