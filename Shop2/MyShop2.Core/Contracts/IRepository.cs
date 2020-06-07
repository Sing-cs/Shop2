using MyShop2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop2.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
        void Delete(string Id);
        void Commit();
    }
}
