using System.Net.Mail;

namespace Expense_Tracker_CLI.Service.Helpers;

public static class InputValidation
{
    public static string GetValidInput(string prompt, Func<string, bool> validation, string errorMsg)
    {
        while (true)
        {
            ConsoleColor.Yellow.WriteConsole(prompt);
            string input = Console.ReadLine();

            if (validation(input))
                return input;

            ConsoleColor.Red.WriteConsole(errorMsg);
        }
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || email.Length < 5)
            return false;

        try
        {
            MailAddress m = new MailAddress(email);
            return m.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static string GetValidPassword()
        {
            while (true)
            {
                string password = GetValidInput(
                    prompt: "Enter Password: ",
                    validation: input => !string.IsNullOrWhiteSpace(input),
                    errorMsg: "Password cannot be empty."
                );

                string confirmPassword = GetValidInput(
                    prompt: "Confirm Password: ",
                    validation: input => !string.IsNullOrWhiteSpace(input),
                    errorMsg: "Confirmation cannot be empty."
                );

                if (password == confirmPassword)
                    return password;

                ConsoleColor.Red.WriteConsole("Passwords do not match.");
            }
        }

    public static bool GetConfirmation(string prompt, bool isValid = false)
    {
        while (true)
        {
            ConsoleColor.Yellow.WriteConsole(prompt);
            string input = Console.ReadLine()?.Trim().ToLower();
            
            if(string.IsNullOrEmpty(input))
                return isValid;
            
            switch (input)
            {
                case "y":
                case "yes":
                    return true;
                case "n":
                case "no":
                    return false;
                default:
                    ConsoleColor.Red.WriteConsole("Invalid input. Please enter 'y' or 'n'.");
                    break;
            }
        }
    }
}
