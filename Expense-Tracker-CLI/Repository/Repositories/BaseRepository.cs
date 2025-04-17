using System.Linq.Expressions;
using Expense_Tracker_CLI.Domain.Common;
using Expense_Tracker_CLI.Repository.Data;
using Expense_Tracker_CLI.Repository.Repositories.Interfaces;
using Expense_Tracker_CLI.Service.Helpers;

namespace Expense_Tracker_CLI.Repository.Repositories;

public class BaseRepository <T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DataContext<T> _context;
    private static int _nextId = 1;

    public BaseRepository()
    {
        _context = new DataContext<T>();
    }

    public void Create(T model)
    {
        ExceptionHandler.Handle(() =>
        {
            model.Id = _nextId++;
            _context.Data.Add(model);
        },"Failed to create record. Check input or storage.");

    }

    public void Update(T model)
    {
        ExceptionHandler.Handle(() =>
        {
            var existing = _context.Data.Find(m=>m.Id==model.Id);
            if (existing == null)
                throw new InvalidOperationException("Record does not exist.");
            _context.Data.Remove(existing);
            model.UpdatedAt = DateTime.UtcNow;
            _context.Data.Add(model);
            
        },"Failed to update record. Check input or storage.");
       
    }

    public void Delete(T model)
    {
        ExceptionHandler.Handle(() =>
        {
            bool isRemoved = _context.Data.Remove(model);
            if(!isRemoved)
                throw new InvalidOperationException("Record does not exist.");
        },"Failed to delete record. Check input or storage.");
    }
    public List<T> GetAll() => 
        ExceptionHandler.Handle(
            () => _context.Data.ToList(),
            fallbackValue: new List<T>() 
        );

    public T Find(Expression<Func<T, bool>> predicate) => 
        ExceptionHandler.Handle(
            () => _context.Data.AsQueryable().FirstOrDefault(predicate),
            fallbackValue: default
        );

    public List<T> GetAllWithExpression(Expression<Func<T, bool>> predicate) => 
        ExceptionHandler.Handle(
            () => _context.Data.AsQueryable().Where(predicate).ToList(),
            fallbackValue: new List<T>()
        );
    
}