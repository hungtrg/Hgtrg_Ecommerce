using System;
using System.Collections.Generic;

namespace Hgtrg.Ecommerce.DataLayer.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? GoogleId { get; set; }

    public string Role { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Seller? Seller { get; set; }
}
