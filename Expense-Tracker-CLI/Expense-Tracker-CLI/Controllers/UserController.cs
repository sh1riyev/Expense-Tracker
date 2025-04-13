using Expense_Tracker_CLI.Domain.Entities;
using Expense_Tracker_CLI.Repository.Repositories.Interfaces;
using Expense_Tracker_CLI.Service.Helpers;
using Expense_Tracker_CLI.Service.Services;
using Expense_Tracker_CLI.Service.Services.Interfaces;

namespace Expense_Tracker_CLI.Expense_Tracker_CLI.Controllers;

public class UserController(IUserService userService)
{
  private readonly IUserService _userService = userService;

  public void Create(User user)
  {
  }
}