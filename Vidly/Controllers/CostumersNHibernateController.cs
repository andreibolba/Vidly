using Autofac;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DataAccess;
using Vidly.DataAccessLayer;
using Vidly.Models;
using Vidly.NHibernateModels;
using Vidly.ViewModelHibernate;

namespace Vidly.Controllers
{
    public class CostumersNHibernateController : Controller
    {
        private ConfigAutofac builder;
        private readonly HibernateProvider hibernateProvider;

        public CostumersNHibernateController(HibernateProvider hibernateProvider)
        {
            this.hibernateProvider = hibernateProvider;
        }

        public ActionResult AllCostumersHibernate()
        {
            bool CanManage = false;
            if (User.IsInRole(RoleName.CanManagerMovies))
                CanManage = true;
            var viewModel = new CostumerHibernateFormViewModel()
            {
                Costumers = hibernateProvider.GetAllElements<CostumersHibernate>().ToList(),
                CanManageCostumers = CanManage
            };
            return View(viewModel);
        }

        /*public ActionResult AllCostumersHibernate()
        {
            using (var c = builder.builder.Build())
            {
                bool CanManage = false;
                if (User.IsInRole(RoleName.CanManagerMovies))
                    CanManage = true;
                var viewModel = new CostumerHibernateFormViewModel()
                {
                    Costumers = c.Resolve<HibernateProvider>().GetAllElements<CostumersHibernate>().ToList(),
                    CanManageCostumers = CanManage
                };
                return View(viewModel);
            }
        }*/
        public ActionResult Edit(int id)
        {
            using (var c = builder.builder.Build())
            {
                var costumer = c.Resolve<HibernateProvider>().GetElement<CostumersHibernate>(id);
                if (costumer == null)
                    costumer = new CostumersHibernate();
                else
                    costumer.MembershipTypeHibernateId = costumer.MembershipTypeHibernate.Id;
                var viewModel = new CostumerHibernateFormViewModel()
                {
                    MembershipTypes = c.Resolve<HibernateProvider>().GetAllElements<MembershipTypesHibernate>().ToList(),
                    Costumer = costumer
                };
                if (User.IsInRole(RoleName.CanManagerMovies))
                    return View("CostumerHibernateForm", viewModel);
                return View("CostumerHibernateRForm", viewModel.Costumer);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CostumersHibernate costumer)
        {
            using (var c = builder.builder.Build())
            {
                if (!ModelState.IsValid)
                {
                    var viewModel = new CostumerHibernateFormViewModel()
                    {
                        MembershipTypes = c.Resolve<HibernateProvider>().GetAllElements<MembershipTypesHibernate>().ToList(),
                        Costumer = costumer
                    };
                    return View("CostumerHibernateForm", viewModel);
                }
                costumer.MembershipTypeHibernate = c.Resolve<HibernateProvider>().GetElement<MembershipTypesHibernate>(costumer.MembershipTypeHibernateId);
                if (costumer.Id == 0)
                    c.Resolve<HibernateProvider>().Add(costumer);
                else
                    c.Resolve<HibernateProvider>().Update(costumer);
                return RedirectToAction("AllCostumersHibernate", "CostumersNHibernate");
            }
        }

        public ActionResult Delete(int id)
        {
            using (var c = builder.builder.Build())
            {
                var costumer = c.Resolve<HibernateProvider>().GetElement<CostumersHibernate>(id);
                c.Resolve<HibernateProvider>().Delete(costumer);
                return RedirectToAction("AllCostumersHibernate", "CostumersNHibernate");
            }
        }
    }
}