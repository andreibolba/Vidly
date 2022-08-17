using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DataAccess;
using Vidly.Models;
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
            bool CanManage = false;
            if (User.IsInRole(RoleName.CanManagerMovies))
                CanManage = true;
            var viewModel = new CostumerHibernateFormViewModel()
            {
                Costumers = session.CreateCriteria<CostumersHibernate>().List<CostumersHibernate>().ToList(),
                CanManageCostumers = CanManage
            };
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var costumer = session.Get<CostumersHibernate>(id);
            if (costumer == null)
                costumer = new NHibernateModels.CostumersHibernate();
            else
                costumer.MembershipTypeHibernateId = costumer.MembershipTypeHibernate.Id;
            var viewModel = new CostumerHibernateFormViewModel()
            {
                MembershipTypes = session.CreateCriteria<MembershipTypesHibernate>().List<MembershipTypesHibernate>().ToList(),
                Costumer = costumer
            };
            if (User.IsInRole(RoleName.CanManagerMovies))
                return View("CostumerHibernateForm", viewModel);
            return View("CostumerHibernateRForm", viewModel.Costumer);
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
            costumer.MembershipTypeHibernate = session.Get<MembershipTypesHibernate>(costumer.MembershipTypeHibernateId);
            if (costumer.Id == 0)
            {
                session.Save(costumer);
            }
            else
            {
                session.Update(costumer);
            }
            transaction.Commit();
            return RedirectToAction("AllCostumersHibernate", "CostumersNHibernate");
        }

        public ActionResult Delete(int id)
        {
            var costumer = session.Get<CostumersHibernate>(id);
            session.Delete(costumer);

            transaction.Commit();
            return RedirectToAction("AllCostumersHibernate", "CostumersNHibernate");
        }
    }
}