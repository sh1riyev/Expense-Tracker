using Expense_Tracker_CLI.Domain.Entities;
using Expense_Tracker_CLI.Service.Common;

namespace Expense_Tracker_CLI.Service.Services.Interfaces;

public interface IUserService
{
    OperationResult<object> Create(User user);
    OperationResult<object> Update(int ? id,string newPassword);
    OperationResult<object> Delete(int? id);
    OperationResult<object> Login(string email, string password);
    OperationResult<List<User>> GetUsers();
    OperationResult<User> GetUser(int? id);
}