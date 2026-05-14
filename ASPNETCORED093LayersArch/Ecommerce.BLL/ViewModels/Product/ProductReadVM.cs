namespace Ecommerce.BLL
{
    public class ProductReadVM
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }//available quantity
        public string? ImageURL { get; set; }

        public string Category { get; set; }

        /*-------------------------------------------------------------------*/
        public int CategoryId { get; set; }    // needed to pre-select dropdown in Edit
        public DateTime? ExpiryDate { get; set; }

    }
}
