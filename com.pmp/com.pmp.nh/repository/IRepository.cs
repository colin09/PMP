using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.pmp.nh.domain;

namespace com.pmp.nh.repository
{
    public interface IRepository<T> where T : BaseModel, new()
    {
        void Add(T product);
        void Update(T product);
        void Remove(T product);
        T GetById(Guid productId);
        ICollection<T> GetList(string category);
    }
}
