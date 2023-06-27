using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int AddressId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal ShippingFee { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;

    public int SellerId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<OrderShipping> OrderShippings { get; set; } = new List<OrderShipping>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Seller Seller { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
