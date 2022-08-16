using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DataAccess;
using Vidly.NHibernateModels;
using Vidly.ViewModelHibernate;

namespace Vidly.Controllers
{
    public class CostumersNHibernateController : Controller
    {
        private ISession session;
        private ITransaction transaction;

        public CostumersNHibernateController()
        {
            session = NHibernateHelper.OpenSession();
            transaction = session.BeginTransaction();
        }

        public ActionResult AllCostumersHibernate()
        {
            var allCostumers = session.CreateCriteria<CostumersHibernate>().List<CostumersHibernate>();
            return View(allCostumers);
        }

        public ActionResult Edit(int id)
        {
            var costumer = session.Get<CostumersHibernate>(id);
            if (costumer == null)
                costumer = new NHibernateModels.CostumersHibernate();
            var viewModel = new CostumerHibernateFormViewModel()
            {
                MembershipTypes = session.CreateCriteria<MembershipTypesHibernate>().List<MembershipTypesHibernate>().ToList(),
                Costumer = costumer
            };
            return View("CostumerHibernateForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NHibernateModels.CostumersHibernate costumer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CostumerHibernateFormViewModel()
                {
                    MembershipTypes = session.CreateCriteria<MembershipTypesHibernate>().List<MembershipTypesHibernate>().ToList(),
                    Costumer = costumer
                };
                return View("CostumerHibernateForm", viewModel);
            }
            if (costumer.Id == 0)
            {
                costumer.MembershipTypeHibernate = session.Get<MembershipTypesHibernate>(costumer.MemberShipId);
                session.Save(costumer);
            }
            else
            {
                session.Update(costumer);
            }
            transaction.Commit();
            return RedirectToAction("AllCostumersHibernate", "CostumersNHibernate");
        }
    }
}