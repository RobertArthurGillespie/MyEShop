using System.Linq;
using MyEShop.Core.Models;

namespace MyEShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        void AddItem(T t);
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string id);
        void Update(T t);
    }
}