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
        private ITransaction transaction;
        //private static HibernateProvider hibernateProvider;
        //private static readonly object Instancelock = new object();

        /*public static HibernateProvider GetInstance
        {
            get
            {
                lock(Instancelock)
                {
                    if (hibernateProvider == null)
                        hibernateProvider = new HibernateProvider();
                    return hibernateProvider;
                }
            }
        }*/

        public HibernateProvider()
        {
            session = NHibernateHelper.OpenSession();
            transaction = session.BeginTransaction();
        }

        public void Add<T>(T element)
        {
            session.Save(element);
            transaction.Commit();
        }

        public T GetElement<T>(int id)
        {
            return session.Get<T>(id);
        }

        public IEnumerable<T> GetAllElements<T>() where T : HibernateEntity
        {
            return session.CreateCriteria<T>().List<T>();
        }

        public void Update<T>(T elementToUpdate)
        {
            session.Update(elementToUpdate);
            transaction.Commit();
        }

        public void Delete<T>(T elementToDelete)
        {
            session.Delete(elementToDelete);
            transaction.Commit();
        }
    }
}