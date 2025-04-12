using Expense_Tracker_CLI.Domain.Entities;
using Expense_Tracker_CLI.Repository.Repositories.Interfaces;

namespace Expense_Tracker_CLI.Repository.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository;