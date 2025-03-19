using Expense_Tracker_CLI.Domain.Common;

namespace Expense_Tracker_CLI.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}