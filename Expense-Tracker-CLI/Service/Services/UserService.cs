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

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public void Craete(User user)
    {
        ValidateUser(user);
        CheckDublicateEmail(user.Email);
        
        _userRepository.Create(user);
    }
    
    public void Update(User user)
    {
       ValidateUser(user);
       CheckDublicateEmail(user.Email,user.Id);
       
       _userRepository.Update(user);
    }

    public void Delete(int? id)
    {
       var existingUser = _userRepository.Find(m=>m.Id == id)
           ?? throw new ArgumentException("User not found");
       
       _userRepository.Delete(existingUser);
    }

    public User Login(string email, string password)
    {
        if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Invalid email or password");
        
        var user =_userRepository.Find(m=>m.Email==email)
            ?? throw new ArgumentException("User not found");
        
        if(!ValidatePassword(password,user.PasswordHash))
            throw new ArgumentException("Invalid password");
        
        return user;
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
