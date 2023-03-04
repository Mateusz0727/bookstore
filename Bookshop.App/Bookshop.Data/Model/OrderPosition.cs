using System;
using System.Collections.Generic;

namespace Bookshop.Data.Model;

public partial class OrderPosition
{
    public long Id { get; set; }

    public string PublicId { get; set; } = null!;

    public long OrderId { get; set; }

    public long BookId { get; set; }

    public float Price { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
