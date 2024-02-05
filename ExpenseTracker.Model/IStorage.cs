using ExpenseTracker.Model;

namespace ExpenseTracker.Server
{
    public interface IStorage
    {
        public Task<IEnumerable<Expense>> GetExpensesAsync();
        public Task<Expense> AddExpenseAsync(Expense expense);
        public Task<bool> RemoveExpenseAsync(int id);
        public Task<bool> ClearExpensesAsync();
        public Task<IEnumerable<Currency>> GetCurrenciesAsync();
        public Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
