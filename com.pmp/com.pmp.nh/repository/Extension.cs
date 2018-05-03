using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace com.pmp.nh.repository
{
   public static class Extension
    {
       public static IQueryable<T> Query<T>(this ISession session)
       {
           return session.Query<T>();
           //return new NhQueryable<T>(session);
       }
    }
}
