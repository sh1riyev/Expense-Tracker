using Expense_Tracker_CLI.Domain.Common;

namespace Expense_Tracker_CLI.Domain.Entities;

public class Budget : BaseEntity
{
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal MonthlyLimit { get; set; }
}