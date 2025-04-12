namespace Expense_Tracker_CLI.Service.Helpers;

public static class ExceptionHandler
{
    public static void Handle(Action action)
    {
        try
        {
            action.Invoke();
        }
        catch (ArgumentNullException ex)
        {
            LogError("Null argument provided", ex);
        }
        catch (ArgumentException ex)
        {
            LogError("Invalid argument provided", ex);
        }
        catch (InvalidOperationException ex)
        {
            LogError("Invalid operation provided", ex);
        }
        catch (Exception ex)
        {
            LogError("An unexpected error occurred", ex);
        }
    }
    
    public static T? Handle<T> (Func<T> function)
    {
        try
        {
            return function();
        }
        catch (ArgumentNullException ex)
        {
            LogError("Null argument provided", ex);
        }
        catch (ArgumentException ex)
        {
            LogError("Invalid argument provided", ex);
        }
        catch (InvalidOperationException ex)
        {
            LogError("Invalid operation", ex);
        }
        catch (Exception ex)
        {
            LogError("An unexpected error occurred", ex);
        }
        return default;
    }

    public static void LogError(string message, Exception ex)
    {
        ConsoleColor.Red.WriteConsole($"{message}\nError Detail: {ex.Message}");
    }
}