using Expense_Tracker_CLI.Domain.Common;

namespace Expense_Tracker_CLI.Domain.Entities;

public class Expense : BaseEntity
{
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public DateTime ExpenseDate { get; set; }
}