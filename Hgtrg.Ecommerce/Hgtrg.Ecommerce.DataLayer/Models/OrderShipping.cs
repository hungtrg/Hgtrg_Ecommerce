using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class OrderShipping
{
    public int OrderId { get; set; }

    public int ShippingMethodId { get; set; }

    public string? Trackingnumber { get; set; }

    public DateTime? ShippedDate { get; set; }

    public DateTime? DeliveredDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ShippingMethod ShippingMethod { get; set; } = null!;
}
