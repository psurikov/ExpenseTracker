using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Database
{
    [Table("Expense")]
    public class ExpenseRecord
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public string? Recipient { get; set; }
        public string? Currency { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
    }
}
