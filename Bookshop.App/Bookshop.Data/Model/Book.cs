using System;
using System.Collections.Generic;

namespace Bookshop.Data.Model;

public partial class Book
{
    public string PublicId { get; set; } = null!;

    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Describe { get; set; } = null!;

    public double Price { get; set; }

    public string PublishingHouse { get; set; } = null!;

    public bool IsDiscount { get; set; }

    public int? Discount { get; set; }

    public DateTime DateCreatedUtc { get; set; }

    public long CreatedBy { get; set; }

    public DateTime? DateModifiedUtc { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual ICollection<OrderPosition> OrderPositions { get; } = new List<OrderPosition>();
}
