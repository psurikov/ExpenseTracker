using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Database
{
    [Table("Category1")]
    public class CategoryRecord
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
