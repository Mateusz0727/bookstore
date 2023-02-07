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
}
