using System;
using System.Collections.Generic;

namespace Bookshop.Data.Model;

public partial class BookLanguage
{
    public long Id { get; set; }

    public string PublicId { get; set; } = null!;

    public string LanguageCode { get; set; } = null!;

    public string LanguageName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
