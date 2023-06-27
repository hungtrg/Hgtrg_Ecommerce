using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string PromotionType { get; set; } = null!;

    public string PromotionName { get; set; } = null!;

    public string PromotionDescription { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? DiscountPercent { get; set; }

    public int? ProductId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Product? Product { get; set; }
}
