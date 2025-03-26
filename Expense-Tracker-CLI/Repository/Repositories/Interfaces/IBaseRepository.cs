using System.Linq.Expressions;
using Expense_Tracker_CLI.Domain.Common;

namespace Expense_Tracker_CLI.Repository.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T model);
    void Update(T model);
    void Delete(T model);
    List<T> GetAll();
    T Find(Expression<Func<T,bool>> predicate);
    List<T> GetAllWithExpression(Expression<Func<T, bool>> predicate);
}