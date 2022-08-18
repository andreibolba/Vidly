using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.NHibernateModels;

namespace Vidly.DataAccess
{
    public class HibernateProvider
    {
        private ISession session;

        public HibernateProvider()
        {
            session = NHibernateHelper.OpenSession();
        }


        public void Add<T>(T element)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(element);
                transaction.Commit();
            }
        }

        public T GetElement<T>(int id)
        {
            return session.Get<T>(id);
        }

        public IEnumerable<T> GetAllElements<T>()
        {
            return session.Query<T>();
        }

        public void Update<T>(T elementToUpdate)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(elementToUpdate);
                transaction.Commit();
            }
        }

        public void Delete<T>(T elementToDelete)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(elementToDelete);
                transaction.Commit();
            }
        }
    }
}