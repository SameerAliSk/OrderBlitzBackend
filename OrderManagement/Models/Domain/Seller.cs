using System;
using System.Collections.Generic;

namespace OrderManagement.Models.Domain;

public partial class Seller
{
    public Guid SellerId { get; set; }

    public string SellerName { get; set; } = null!;

    public virtual ICollection<CartProductItem> CartProductItems { get; set; } = new List<CartProductItem>();

    public virtual ICollection<SellerProductItem> SellerProductItems { get; set; } = new List<SellerProductItem>();
}
