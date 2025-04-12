using Expense_Tracker_CLI.Domain.Entities;
using Expense_Tracker_CLI.Repository.Repositories;
using Expense_Tracker_CLI.Repository.Repositories.Interfaces;
using Expense_Tracker_CLI.Service.Helpers;
using Expense_Tracker_CLI.Service.Services.Interfaces;
using Exception = System.Exception;

namespace Expense_Tracker_CLI.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService()
    {
        _userRepository = new UserRepository();
    }
    public void Craete(User user)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.FullName))
            {
                throw new ArgumentException("Name and Email cannot be empty");
            }

            if (_userRepository.Find(m => m.Email == user.Email) != null)
            {
                throw new ArgumentException("Email already exists");
            }

            _userRepository.Create(user);
            ConsoleColor.Green.WriteConsole($"User: {user.Email} has been created");
        }
        
        catch (ArgumentException ex)
        {
            ConsoleColor.Red.WriteConsole($"Validation error: {ex.Message}");
            throw; 
        }
        catch (Exception ex)
        {
            ConsoleColor.Red.WriteConsole($"An unexpected error occurred: {ex.Message}");
            throw; 
        }
    }
    
    public void Update(User user)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("Email cannot be empty");
            }

            if (_userRepository.Find(m => m.Email == user.Email) != null)
            {
                throw new ArgumentException("Email already exists");
            }
            _userRepository.Update(user);
            ConsoleColor.Green.WriteConsole($"User: {user.Email} has been updated");
        }
        catch (ArgumentException ex)
        {
            ConsoleColor.Red.WriteConsole($"Validation error: {ex.Message}");
            throw; 
        }
        catch (Exception e)
        {
            ConsoleColor.Red.WriteConsole($"An unexpected error occurred: {e.Message}");
            throw; 
        }
    }

    public void Delete(int? id)
    {
        try
        {
            var user = _userRepository.Find(m => m.Id == id);
            _userRepository.Delete(user);
            ConsoleColor.Green.WriteConsole($"User: {user.Email} has been deleted");
        }
        catch (ArgumentNullException ex)
        {
            ConsoleColor.Red.WriteConsole("User does not exist");
            throw;
        }
        catch (Exception e)
        {
            ConsoleColor.Red.WriteConsole($"An unexpected error occurred: {e.Message}");
            throw; 
        }
    }

    public User Login(string email, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Email and Password cannot be empty");
            }
            
            var user = _userRepository.Find(m => m.Email == email && m.PasswordHash==password);

            if (user == null)
            {
                throw new ArgumentException("Invalid credentials");
            }
            
            return user;
        }
        catch (Exception e)
        {
            ConsoleColor.Red.WriteConsole($"An unexpected error occurred: {e.Message}");
            throw;
        }
    }
}