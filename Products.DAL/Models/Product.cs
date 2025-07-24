using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.DAL.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [MaxLength(250)]
        public string ProductDescription { get; set; } = default!;
        public string? Category { get; set; }
        [Range(0.01,double.MaxValue)] 
        public decimal Price { get; set; }

        [Range(0,int.MaxValue)]
        public int StockAvailable { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get;set; }

    }
}
