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

        public ActionResult Edit(int id)
        {
            var costumer = hibernateProvider.GetElement<CostumersHibernate>(id);
            if (costumer == null)
                costumer = new CostumersHibernate();
            else
                costumer.MembershipTypeHibernateId = costumer.MembershipTypeHibernate.Id;
            var viewModel = new CostumerHibernateFormViewModel()
            {
                MembershipTypes = hibernateProvider.GetAllElements<MembershipTypesHibernate>().ToList(),
                Costumer = costumer
            };
            if (User.IsInRole(RoleName.CanManagerMovies))
                return View("CostumerHibernateForm", viewModel);
            return View("CostumerHibernateRForm", viewModel.Costumer);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CostumersHibernate costumer)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new CostumerHibernateFormViewModel()
                {
                    MembershipTypes = hibernateProvider.GetAllElements<MembershipTypesHibernate>().ToList(),
                    Costumer = costumer
                };
                return View("CostumerHibernateForm", viewModel);
            }
            costumer.MembershipTypeHibernate = hibernateProvider.GetElement<MembershipTypesHibernate>(costumer.MembershipTypeHibernateId);
            if (costumer.Id == 0)
                hibernateProvider.Add(costumer);
            else
                hibernateProvider.Update(costumer);
            return RedirectToAction("AllCostumersHibernate", "CostumersNHibernate");

        }

        public ActionResult Delete(int id)
        {
            var costumer = c.Resolve<HibernateProvider>().GetElement<CostumersHibernate>(id);
            hibernateProvider.Delete(costumer);
            return RedirectToAction("AllCostumersHibernate", "CostumersNHibernate");

        }
    }
}