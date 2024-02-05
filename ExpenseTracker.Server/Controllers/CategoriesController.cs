using ExpensesTracker.Server.Controllers;
using ExpenseTracker.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController
    {
        private readonly ILogger<ExpensesController> logger;
        private readonly IStorage storage;

        public CategoriesController(ILogger<ExpensesController> logger, IStorage storage)
        {
            this.logger = logger;
            this.storage = storage;
        }

        [HttpGet("getCategories")]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var result = await storage.GetCategoriesAsync();
            return result;
        }
    }
}
