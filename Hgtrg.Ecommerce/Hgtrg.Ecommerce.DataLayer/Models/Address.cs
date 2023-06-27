using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string AddressDetail { get; set; } = null!;

    public bool? IsDefault { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
