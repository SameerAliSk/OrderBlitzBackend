using System;
using System.Collections.Generic;

namespace OrderManagement.Models.Domain;

public partial class OrderedItem
{
    public Guid OrderedItemId { get; set; }

    public Guid ProductItemId { get; set; }

    public Guid OrderId { get; set; }

    public long Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual ShippingOrder Order { get; set; } = null!;

    public virtual ProductItemDetail ProductItem { get; set; } = null!;
}
