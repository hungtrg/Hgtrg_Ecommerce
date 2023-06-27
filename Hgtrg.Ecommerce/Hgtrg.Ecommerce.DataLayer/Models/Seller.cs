using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class Seller
{
    public int SellerId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SellerProductCategory> SellerProductCategories { get; set; } = new List<SellerProductCategory>();

    public virtual User User { get; set; } = null!;
}
