using ExpenseTracker.Model;
using ExpenseTracker.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Database
{
    /// <summary>
    /// Represents the database storage (SQLite)
    /// </summary>
    public sealed class DatabaseStorage : IStorage, IDisposable
    {
        private readonly DatabaseContext databaseContext;
        
        public DatabaseStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            var expenses = await databaseContext.Expenses.ToListAsync();
            return await Task.FromResult(expenses.Select(e => FromExpenseRecord(e)));
        }

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            var expenseRecord = ToExpenseRecord(expense);
            await databaseContext.Expenses.AddAsync(expenseRecord);
            await databaseContext.SaveChangesAsync();
            return FromExpenseRecord(expenseRecord);
        }

        public async Task<bool> RemoveExpenseAsync(int id)
        {
            var expenses = databaseContext.Expenses.Where(e => e.Id == id);
            if (!expenses.Any())
                return false;
            databaseContext.Expenses.RemoveRange(expenses);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearExpensesAsync()
        {
            if (databaseContext.Expenses.Any() == false)
                return false;
            databaseContext.Expenses.RemoveRange(databaseContext.Expenses);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            var currencies = await databaseContext.Currencies.ToListAsync();
            return await Task.FromResult(currencies.Select(e => FromCurrencyRecord(e)));
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await databaseContext.Categories.ToListAsync();
            return await Task.FromResult(categories.Select(e => FromCategoryRecord(e)));
        }

        private static Category FromCategoryRecord(CategoryRecord categoryRecord)
        {
            var category = new Category();
            category.Id = categoryRecord.Id;
            category.Name = categoryRecord.Name;
            return category;
        }

        private static Currency FromCurrencyRecord(CurrencyRecord currencyRecord)
        {
            var currency = new Currency();
            currency.Id = currencyRecord.Id;
            currency.Name = currencyRecord.Name;
            return currency;
        }

        private static ExpenseRecord ToExpenseRecord(Expense expense)
        {
            var record = new ExpenseRecord();
            record.Currency = expense.Currency;
            record.Recipient = expense.Recipient;
            record.Amount = expense.Amount;
            record.Date = expense.Date;
            record.Category = expense.Category;
            record.Description = expense.Description;
            return record;
        }

        private static Expense FromExpenseRecord(ExpenseRecord expenseRecord)
        {
            var expense = new Expense();
            expense.Id = expenseRecord.Id;
            expense.Currency = expenseRecord.Currency;
            expense.Recipient = expenseRecord.Recipient;
            expense.Amount = expenseRecord.Amount;
            expense.Date = expenseRecord.Date;
            expense.Category = expenseRecord.Category;
            expense.Description = expenseRecord.Description;
            return expense;
        }

        public void Dispose()
        {
            databaseContext.Dispose();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseStorage(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();
            services.AddScoped<IStorage, DatabaseStorage>();
        }
    }
}
