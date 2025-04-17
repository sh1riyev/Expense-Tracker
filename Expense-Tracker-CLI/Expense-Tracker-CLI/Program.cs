using Expense_Tracker_CLI.Domain.Entities;
using Expense_Tracker_CLI.Expense_Tracker_CLI.Controllers;
using Expense_Tracker_CLI.Repository.Repositories;
using Expense_Tracker_CLI.Repository.Repositories.Interfaces;
using Expense_Tracker_CLI.Service.Services;
using Expense_Tracker_CLI.Service.Services.Interfaces;

void Main()
{
    // Step 1: Instantiate repositories (concrete classes)
    IUserRepository userRepository = new UserRepository();
        
    // Step 2: Instantiate services (inject interfaces)
    IUserService userService = new UserService(userRepository);
        
    // Step 3: Instantiate controllers (inject services)
    var userController = new UserController(userService);
        
    // Step 4: Run the app
    userController.Create();
    userController.GetAll();
    // userController.Delete();
    // userController.Update();
    // userController.GetAll();
    // userController.Delete();
    userController.Login();
}

Main();