using ExpensesTracker.Server.Controllers;
using ExpenseTracker.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController
    {
        private readonly ILogger<ExpensesController> logger;
        private readonly IStorage storage;

        public CurrenciesController(ILogger<ExpensesController> logger, IStorage storage)
        {
            this.logger = logger;
            this.storage = storage;
        }

        [HttpGet("getCurrencies")]
        public async Task<IEnumerable<Currency>> GetCurrencies()
        {
            var result = await storage.GetCurrenciesAsync();
            return result;
        }
    }
}
