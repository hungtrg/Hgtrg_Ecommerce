using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class ShippingMethod
{
    public int ShippingMethodId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Fee { get; set; }

    public virtual ICollection<OrderShipping> OrderShippings { get; set; } = new List<OrderShipping>();
}
