namespace Expense_Tracker_CLI.Service.Helpers;

public static class ConsoleExtension
{
    public static void WriteConsole(this ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}