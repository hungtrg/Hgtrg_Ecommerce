using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class SellerProductCategory
{
    public int SellerId { get; set; }

    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
