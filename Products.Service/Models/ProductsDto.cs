namespace Products.Service.Models
{
    public class ProductsDto
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; } = default!;
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public int? StockAvailable { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
