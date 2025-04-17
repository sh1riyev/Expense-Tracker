using Expense_Tracker_CLI.Domain.Entities;
using Expense_Tracker_CLI.Repository.Repositories.Interfaces;
using Expense_Tracker_CLI.Service.Helpers;
using Expense_Tracker_CLI.Service.Services;
using Expense_Tracker_CLI.Service.Services.Interfaces;

namespace Expense_Tracker_CLI.Expense_Tracker_CLI.Controllers;

public class UserController
{
  private readonly IUserService _userService;
  public UserController(IUserService userService)
  {
    _userService = userService;
  }
  public void Create()
  {
    try
    {
      var user = new User
      {
        FullName = InputValidation.GetValidInput(
          "Full Name",
          s=>!string.IsNullOrWhiteSpace(s),
          "Full Name cannot be empty."
          ),
        Email = InputValidation.GetValidInput(
          "Email",
          InputValidation.IsValidEmail,
          "Invalid email format (e.g., user@example.com)."
          ),
        PasswordHash = InputValidation.GetValidPassword()
      };
      
      var result = _userService.Create(user);
      if (result.IsSuccess)
      {
        ConsoleColor.Green.WriteConsole(result.Message);
      }
      else
      {
        ConsoleColor.Red.WriteConsole($"Creation failed: {result.Message}");
      }
    }
    catch (ArgumentException ex)
    {
      ConsoleColor.Red.WriteConsole($"Error: {ex.Message}");
    }
    catch (Exception ex)
    {
      ConsoleColor.Red.WriteConsole($"Unexpected error: {ex.Message}");
    }
  }

  public void Delete()
  {
    try
    {
      var userId = InputValidation.GetValidInput(
        "Enter User ID to Delete",
        s => int.TryParse(s, out int id) && id > 0,
        "User ID is not valid"
      );

      int id = int.Parse(userId);

      bool confirmDelete = InputValidation.GetConfirmation(
        $"Are you sure you want to delete {id}? (y/n)"
      );
      if (!confirmDelete)
      {
        ConsoleColor.Yellow.WriteConsole("Deletion cancelled.");
        return;
      }
      
      var result = _userService.Delete(id);
      
      if (result.IsSuccess)
      {
        ConsoleColor.Green.WriteConsole(result.Message);
      }
      else
      {
        ConsoleColor.Red.WriteConsole($"Deletion failed: {result.Message}");
      }

    }
    catch (Exception ex)
    {
      ConsoleColor.Red.WriteConsole("An unexpected error occurred.");
    }
  }

  public void GetAll()
  {
    try
    {
      var users = _userService.GetUsers();
      if (!users.IsSuccess)
      {
        ConsoleColor.Red.WriteConsole(users.Message);
      }
      foreach (var user in users.Data)
      {
        Console.WriteLine($"{user.FullName} {user.Email} {user.Id}");
      }
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
   
  }

  public void Update()
  {
    try
    {
      var userId =  InputValidation.GetValidInput(
        "Enter User ID to Update",
        s => int.TryParse(s, out int id) && id > 0,
        "User ID is not valid"
      );

      int id = int.Parse(userId);

      var newPassword = InputValidation.GetValidPassword();

      var result = _userService.Update(id, newPassword);
        
      Console.WriteLine(result.IsSuccess 
        ? "Success!" 
        : $"Error: {result.Message}");

    }
    catch (Exception e)
    {
      ConsoleColor.Red.WriteConsole($"Unexpected error: {e.Message}");
    }
  }

  public void Login()
  {
    try
    {
      ConsoleColor.Yellow.WriteConsole("Enter Email:");
      string email = Console.ReadLine();
    
      ConsoleColor.Yellow.WriteConsole("Enter Password:");
      string password = Console.ReadLine();
    
      var result = _userService.Login(email, password);
      Console.WriteLine(result.IsSuccess 
        ? "Success!" 
        : $"Error: {result.Message}");
      
    }
    catch (Exception e)
    {
      ConsoleColor.Red.WriteConsole($"Unexpected error: {e.Message}");
    }
   
  }
}