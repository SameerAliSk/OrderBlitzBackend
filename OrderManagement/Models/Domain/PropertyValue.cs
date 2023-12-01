using System;
using System.Collections.Generic;

namespace OrderManagement.Models.Domain;

public partial class PropertyValue
{
    public Guid PropertyValueId { get; set; }

    public Guid PropertyNameId { get; set; }

    public string PropertyValue1 { get; set; } = null!;

    public virtual ICollection<ProductItemConfiguration> ProductItemConfigurations { get; set; } = new List<ProductItemConfiguration>();

    public virtual PropertyName PropertyName { get; set; } = null!;
}
