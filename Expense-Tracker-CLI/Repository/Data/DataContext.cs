namespace Expense_Tracker_CLI.Repository.Data;

public class DataContext<T>
{
    public List<T> Data { get; }

    public DataContext()
    {
        Data = new List<T>();
    }
}