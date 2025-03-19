namespace Expense_Tracker_CLI.Repository.Data;

public class DataContext<T>
{
    public List<T> Data { get; set; }

    public DataContext()
    {
        Data = new List<T>();
    }
}