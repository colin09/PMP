using com.pmp.nh.domain;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.pmp.nh.repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel, new()
    {

        public IQueryable<T> Query()
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Query<T>();
        }


        public void Add(T model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(model);
                transaction.Commit();
            }
        }

        public ICollection<T> GetList(string category)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var list = session
                    .CreateCriteria(typeof(T))
                    .Add(Restrictions.Eq("Category", category))
                    .List<T>();
                return list;
            }
        }

        public T GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var model = session
                    .CreateCriteria(typeof(T))
                    .Add(Restrictions.Eq("Id", id))
                    .UniqueResult<T>();
                return model;
            }
        }


        public void Remove(T model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(model);
                transaction.Commit();
            }
        }

        public void Update(T model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(model);
                transaction.Commit();
            }
        }
    }
}
