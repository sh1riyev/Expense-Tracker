using Expense_Tracker_CLI.Domain.Common;

namespace Expense_Tracker_CLI.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
}