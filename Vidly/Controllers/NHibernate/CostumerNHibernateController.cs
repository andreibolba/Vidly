using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DataAccess;

namespace Vidly.Controllers.NHibernate
{
    public class CostumerNHibernateController : Controller
    {
        private readonly ISession session;
        private readonly ITransaction trasaction;

        public CostumerNHibernateController()
        {
            session = NHibernateHelper.OpenSession();
            trasaction = session.BeginTransaction();
        }

        protected override void Dispose(bool disposing)
        {
            NHibernateHelper.CloseSession();
        }

        public ActionResult AllCostumers()
        {
            return View();
        }
    }
}