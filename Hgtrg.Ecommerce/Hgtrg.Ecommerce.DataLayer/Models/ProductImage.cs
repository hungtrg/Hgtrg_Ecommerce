using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class ProductImage
{
    public int ImageId { get; set; }

    public int ProductId { get; set; }

    public string Url { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
