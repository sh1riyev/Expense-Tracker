using Expense_Tracker_CLI.Domain.Entities;

namespace Expense_Tracker_CLI.Service.Services.Interfaces;

public interface IUserService
{
    void Craete(User user);
    void Update(User user);
    void Delete(int? id);
    User Login(string email, string password);
}