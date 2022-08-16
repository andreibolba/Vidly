using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DataAccess;
using Vidly.NHibernateModels;

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
    }
}