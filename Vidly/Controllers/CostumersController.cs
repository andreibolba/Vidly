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
using Autofac;
using Vidly.DataAccessLayer;

namespace Vidly.Controllers
{
    public class CostumersController : Controller
    {
        private ConfigAutofac builder;

        public CostumersController()
        {
            builder = new ConfigAutofac();

        }

        public ActionResult AllCostumers()
        {
            return View();
        }

        [Route("Costumers/GetCostumer/{id}")]
        public ActionResult GetCostumer(int id)
        {
            using (var c = builder.builder.Build())
            {
                var costumer = c.Resolve<EntityFrameworkCostumerProvider>().GetCostumer(id);

                if (costumer == null)
                    return HttpNotFound();

                return View(costumer);
            }
        }

        // GET: Costumers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            using (var c = builder.builder.Build())
            {
                var viewModel = new CostumerFormViewModel()
                {
                    MembershipTypes = c.Resolve<EntityFrameworkMembershipTypeProvider>().GetMembershipTypes().ToList(),
                    Costumer = new Models.Costumer()
                };
                return View("CostumerForm", viewModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Models.Costumer costumer)
        {
            using (var c = builder.builder.Build())
            {
                if (!ModelState.IsValid)
                {
                    var viewModel = new CostumerFormViewModel()
                    {
                        MembershipTypes = c.Resolve<EntityFrameworkMembershipTypeProvider>().GetMembershipTypes().ToList(),
                        Costumer = costumer
                    };
                    return View("CostumerForm", viewModel);
                }
                if (costumer.Id == 0)
                    c.Resolve<EntityFrameworkCostumerProvider>().AddCostumer(costumer);
                else
                    c.Resolve<EntityFrameworkCostumerProvider>().UpdateCostumer(costumer);
                return RedirectToAction("AllCostumers", "Costumers");
            }
        }

        public ActionResult Edit(int id)
        {
            using (var c = builder.builder.Build())
            {
                var costumer = c.Resolve<EntityFrameworkCostumerProvider>().GetCostumer(id);
                if (costumer == null)
                    costumer = new Models.Costumer();
                var viewModel = new CostumerFormViewModel()
                {
                    MembershipTypes = c.Resolve<EntityFrameworkMembershipTypeProvider>().GetMembershipTypes().ToList(),
                    Costumer = costumer
                };
                return View("CostumerForm", viewModel);
            }
        }
    }
}