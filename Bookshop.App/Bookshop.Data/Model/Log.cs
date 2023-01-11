using System;
using System.Collections.Generic;

namespace Bookshop.Data.Model;

public partial class Log
{
    public long Id { get; set; }

    public string Text { get; set; } = null!;
}
