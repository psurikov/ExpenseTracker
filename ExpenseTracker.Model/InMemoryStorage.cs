using ExpenseTracker.Model;

namespace ExpenseTracker.Server
{
    /// <summary>
    /// Provides an in memory storage.
    /// </summary>
    public class InMemoryStorage : IStorage
    {
        private List<Expense> expenses =
        [
            new Expense()
            {
                Date = DateTime.Now.Date,
                Amount = 50,
                Recipient = "Cafe Tarquin",
                Currency = "USD",
                Category = "Drinks",
            },
            new Expense()
            {
                Date = DateTime.Now.Date,
                Amount = 25,
                Recipient = "Store",
                Currency = "USD",
                Category = "Food",
            }
        ];

        private List<Currency> currencies =
        [
            new Currency() { Name = "USD" },
            new Currency() { Name = "EUR" },
            new Currency() { Name = "UAH" },
            new Currency() { Name = "AUS" }
        ];

        private List<Category> categories =
        [
            new Category() { Name = "Food" },
            new Category() { Name = "Drinks" },
            new Category() { Name = "Movies" },
            new Category() { Name = "Books" },
            new Category() { Name = "Store" }
        ];

        /// <inheritdoc />
        public Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return Task.FromResult(expenses as IEnumerable<Expense>);
        }

        /// <inheritdoc />
        public Task<Expense> AddExpenseAsync(Expense expense)
        {
            expenses.Add(expense);
            return Task.FromResult(expense);
        }

        /// <inheritdoc />
        public Task<bool> RemoveExpenseAsync(int id)
        {
            var count = expenses.RemoveAll(e => e.Id == id);
            var removed = count > 0;
            return Task.FromResult(removed);
        }

        /// <inheritdoc />
        public Task<bool> ClearExpensesAsync()
        {
            expenses.Clear();
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            return Task.FromResult(currencies as IEnumerable<Currency>);
        }

        /// <inheritdoc />
        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return Task.FromResult(categories as IEnumerable<Category>);
        }
    }

}