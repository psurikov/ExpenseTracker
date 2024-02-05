using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Database
{
    [Table("Currency")]
    public class CurrencyRecord
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
