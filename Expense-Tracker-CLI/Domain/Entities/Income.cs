using Expense_Tracker_CLI.Domain.Common;

namespace Expense_Tracker_CLI.Domain.Entities;

public class Income : BaseEntity
{
    public decimal Amount { get; set; }
    public int UserId { get; set; }
    public DateTime IncomeDate { get; set; }
}