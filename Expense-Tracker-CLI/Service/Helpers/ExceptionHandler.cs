namespace Expense_Tracker_CLI.Service.Helpers;

public static class ExceptionHandler
{
    public static void Handle(Action action, string? customErrorMessage = null)
    {
        try
        {
            action.Invoke();
        }
        catch (Exception ex)
        {
            LogAndDisplayError(ex, customErrorMessage);
            throw; 
        }
    }
    
    public static T Handle<T>(Func<T> function, string? customErrorMessage = null, T? fallbackValue = default)
    {
        try
        {
            return function.Invoke();
        }
        catch (Exception ex)
        {
            LogAndDisplayError(ex, customErrorMessage);
            return fallbackValue; 
        }
    }

    private static void LogAndDisplayError(Exception ex, string? customMessage)
    {
        string errorMessage = customMessage ?? GetDefaultErrorMessage(ex);
        ConsoleColor.Red.WriteConsole($"{errorMessage}\nDetails: {ex.Message}");
        
    }

    private static string GetDefaultErrorMessage(Exception ex)
    {
        return ex switch
        {
            ArgumentNullException => "Required data is missing.",
            ArgumentException => "Invalid input provided.",
            InvalidOperationException => "Operation cannot be performed.",
            _ => "An unexpected error occurred."
        };
    }
}