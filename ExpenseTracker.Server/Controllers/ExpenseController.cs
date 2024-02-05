using ExpenseTracker.Server;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> logger;
        private readonly IStorage storage;

        public ExpensesController(ILogger<ExpensesController> logger, IStorage storage)
        {
            this.logger = logger;
            this.storage = storage;
        }

        [HttpPost("addExpense")]
        public async Task<IActionResult> AddExpense([FromBody] Expense expense)
        {
            var amountError = ValidateAmount(expense.Amount);
            if (amountError != null)
            {
                logger.LogError(amountError);
                return BadRequest(amountError);
            }

            var categoryError = ValidateCategory(expense.Category);
            if (categoryError != null)
            {
                logger.LogError(categoryError);
                return BadRequest(categoryError);
            }

            var descriptionError = ValidateDescription(expense.Description);
            if (descriptionError != null)
            {
                logger.LogError(descriptionError);
                return BadRequest(descriptionError);
            }

            var recipientError = ValidateRecipient(expense.Recipient);
            if (recipientError != null)
            {
                logger.LogError(recipientError);
                return BadRequest(recipientError);
            }

            var currencyError = ValidateCurrency(expense.Currency);
            if (currencyError != null)
            {
                logger.LogError(currencyError);
                return BadRequest(currencyError);
            }

            var addedExpense = await storage.AddExpenseAsync(expense);
            logger.LogInformation($"Added expense with Id: {expense.Id}");
            return Ok(addedExpense);
        }

        [HttpGet("getExpenses")]
        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            var result = await storage.GetExpensesAsync();
            return result;
        }

        [HttpDelete("clearExpenses")]
        public async Task<IActionResult> ClearExpenses()
        {
            await storage.ClearExpensesAsync();
            logger.LogInformation($"Cleared expenses.");
            return Ok();
        }

        private static string? ValidateAmount(int amount)
        {
            int maximumAmount = 1000000000;
            if (amount < 0)
                return "Amount is less than zero.";
            if (amount > maximumAmount)
                return string.Format("Amount cannot be larger than {0}", maximumAmount);
            return null;
        }

        private static string? ValidateCategory(string? category)
        {
            int maximumCategoryLength = 100;
            if (string.IsNullOrWhiteSpace(category))
                return "Category cannot be empty.";
            if (category.Length > maximumCategoryLength)
                return "Category is too long";
            return null;
        }

        private static string? ValidateCurrency(string? currency)
        {
            var currencyLength = 3;
            if (string.IsNullOrWhiteSpace(currency))
                return "Currency cannot be empty.";
            if (currency.Length != currencyLength)
                return "Currency length should be 3";
            return null;
        }

        private static string? ValidateRecipient(string? recipient)
        {
            int maximumRecipientLength = 100;
            if (string.IsNullOrWhiteSpace(recipient))
                return "Recipient cannot be empty.";
            if (recipient.Length > maximumRecipientLength)
                return "Recipient is too long";
            return null;
        }

        private static string? ValidateDescription(string? description)
        {
            int maximumDescriptionLength = 100;
            if (string.IsNullOrWhiteSpace(description))
                return "Description cannot be empty.";
            if (description.Length > maximumDescriptionLength)
                return "Description is too long";
            return null;
        }
    }
}