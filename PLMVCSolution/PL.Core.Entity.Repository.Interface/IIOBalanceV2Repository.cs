using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PL.Core.Entity.Repository.Interface
{
    public interface IIOBalanceV2Repository <T>
    {
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T Insert(T item);
        T Update(T item, Expression<Func<T, bool>> predicate);
        T Update2(T item);
        bool Delete(Expression<Func<T, bool>> predicate);
        bool BulkInsert(IList<T> list);
        IEnumerable<T> ExecuteQuery(string commandString, params object[] param);
        DataTable ExecuteSPReturnTable(string commandString, bool IsStoredProc, params object[] param);
        bool RemoveRange(Expression<Func<T, bool>> predicate);
    }
}
