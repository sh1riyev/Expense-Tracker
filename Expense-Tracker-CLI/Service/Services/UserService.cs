using Expense_Tracker_CLI.Domain.Entities;
using Expense_Tracker_CLI.Repository.Repositories;
using Expense_Tracker_CLI.Repository.Repositories.Interfaces;
using Expense_Tracker_CLI.Service.Common;
using Expense_Tracker_CLI.Service.Helpers;
using Expense_Tracker_CLI.Service.Services.Interfaces;
using Exception = System.Exception;

namespace Expense_Tracker_CLI.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public OperationResult<object> Create(User user)
    {
        try
        {
            ValidateUser(user);
            CheckDublicateEmail(user.Email);
        
            _userRepository.Create(user);
            return new OperationResult<object>(true,null,$"User {user.Email} created");
        }
        catch (ArgumentException ex)
        {
            return new OperationResult<object>(false, null,ex.Message);
        }
        catch (Exception ex)
        {
            return new OperationResult<object>(false, null,"An unexpected error occurred");
        }
       
    }
    
    public OperationResult<object> Update(int ?id,string newPassword)
    {
        try
        {
            var user = _userRepository.Find(u => u.Id == id);
            if (user == null)
                return new OperationResult<object>(false, null,"User not found");
            user.PasswordHash = newPassword;
            _userRepository.Update(user);
            return new OperationResult<object>(true, null,$"User {user.Email} updated");
        }
        catch (ArgumentException ex)
        {
            return new OperationResult<object>(false, null,ex.Message);
        }
        catch (Exception ex)
        {
            return new OperationResult<object>(false, null,"An unexpected error occurred");
        }
        
    }

    public OperationResult<object> Delete(int? id)
    {
        try
        {
            var user = _userRepository.Find(u => u.Id == id);
            if (user == null)
                return new OperationResult<object>(false, null,"User not found");
            
            _userRepository.Delete(user);
            return new OperationResult<object>(true, null,$"User {user.Email} deleted");
        }
        catch (Exception e)
        {
            return new OperationResult<object>(false, null,"Database error during deletion");
        }
    }

    public OperationResult<object> Login(string email, string password)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return new OperationResult<object>(false, "Email or password cannot be empty");

            var user = _userRepository.Find(m => m.Email == email);
            if (user == null) 
                return new OperationResult<object>(false, null,"User not found");
        
            if(!ValidatePassword(password,user.PasswordHash))
                return new OperationResult<object>(false, null,"Invalid password");
        
            return new OperationResult<object>(true, null,$"User {user.Email} logged in");
        }
        catch (Exception e)
        {
            return new OperationResult<object>(false, null,"Database error during deletion");
        }

    }

    public OperationResult<List<User>> GetUsers()
    {
        try
        {
            var users = _userRepository.GetAll().ToList();
            return users == null
                ? (new OperationResult<List<User>>(false, null, "No Users found"))
                : new OperationResult<List<User>>(true, users, "Users found");
        }
        catch (Exception e)
        {
            return new OperationResult<List<User>>(false, null,"Database error during deletion");
        }
     
    }

    public OperationResult<User> GetUser(int? id)
    {
        try
        {
            var user = _userRepository.Find(m => m.Id == id);
            return user == null
                ? (new OperationResult<User>(false, null, "User not found"))
                : new OperationResult<User>(true, user, "User found");
        }
        catch (Exception e)
        {
            return new OperationResult<User>(false, null,"Database error during deletion");
        }
    }

    private void CheckDublicateEmail(string email,int? currentUserId=null)
        {
            var existingUser = _userRepository.Find(m=>m.Email==email);
            if(existingUser!=null && existingUser.Id != currentUserId)
                throw new ArgumentException($"Email {email} already exists");
        }

    private void ValidateUser(User user)
    {
        if(string.IsNullOrWhiteSpace(user.FullName))
            throw new ArgumentException("Full name cannot be empty");
        
        if(string.IsNullOrWhiteSpace(user.Email)||!InputValidation.IsValidEmail(user.Email))
            throw new ArgumentException("Email is not valid");
    }

    private bool ValidatePassword(string password, string passwordHash)
    {
        return password == passwordHash;
    }
}
