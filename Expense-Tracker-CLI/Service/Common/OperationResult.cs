namespace Expense_Tracker_CLI.Service.Common;

public record OperationResult<T>(bool IsSuccess,T? Data =  default, string ? Message=null);