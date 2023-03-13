using System;
using System.Collections.Generic;

namespace Bookshop.Data.Model;

public partial class Order
{
    public long Id { get; set; }

    public string PublicId { get; set; } = null!;

    public DateTime DateCreatedUtc { get; set; }

    public long CreatedBy { get; set; }

    public DateTime? DateModifiedUtc { get; set; }

    public long? ModifiedBy { get; set; }

    public long UserId { get; set; }

    public string Status { get; set; } = null!;

    public float Amount { get; set; }

    public string? PayPalId { get; set; }

    public virtual ICollection<OrderPosition> OrderPositions { get; } = new List<OrderPosition>();

    public virtual User User { get; set; } = null!;
}
