namespace OrderManagement.Models.Dto
{
    public class ProductItemDetailDTO
    {
        public Guid ProductItemId { get; set; }
        public long QtyInStock { get; set; }
        public decimal Price { get; set; }
        public string ProductItemName { get; set; }
    }
}
