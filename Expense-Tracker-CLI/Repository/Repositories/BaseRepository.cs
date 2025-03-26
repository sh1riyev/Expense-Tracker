using System.Linq.Expressions;
using Expense_Tracker_CLI.Domain.Common;
using Expense_Tracker_CLI.Repository.Data;
using Expense_Tracker_CLI.Repository.Repositories.Interfaces;

namespace Expense_Tracker_CLI.Repository.Repositories;

public class BaseRepository <T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DataContext<T> _context;

    public BaseRepository()
    {
        _context = new DataContext<T>();
    }
    
    public void Create(T model) => _context.Data.Add(model);

    public void Update(T model)
    {
        var existing = _context.Data.Find(m=>m.Id==model.Id);
        if (existing != null)
        {
            _context.Data.Remove(existing);
            _context.Data.Add(model);
        }
    }

    public void Delete(T model) => _context.Data.Remove(model);

    public List<T> GetAll() => _context.Data.ToList();

    public T Find(Expression<Func<T, bool>> predicate) => _context.Data.AsQueryable().FirstOrDefault(predicate);

    public List<T> GetAllWithExpression(Expression<Func<T, bool>> predicate) => _context.Data.AsQueryable().Where(predicate).ToList();
}