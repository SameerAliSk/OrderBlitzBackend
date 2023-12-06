namespace OrderManagement.Models.Dto
{
    public class OrderInformationDto
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public List<ProductDto> Products { get; set; }
        public string OrderDate { get; set; }
        public string ExpectedDeliveryDate { get; set; }
        public decimal TotalOrderAmount { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryAddress { get; set; }
    }

    public class ProductDto
    {
        public string Name { get; set; }
        public long Quantity { get; set; }
    }
}
