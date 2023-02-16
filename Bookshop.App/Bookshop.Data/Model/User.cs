using System;
using System.Collections.Generic;

namespace Bookshop.Data.Model;

public partial class User
{
    public long Id { get; set; }

    public string PublicId { get; set; } = null!;

    public DateTime DateCreatedUtc { get; set; }

    public long CreatedBy { get; set; }

    public DateTime? DateModifiedUtc { get; set; }

    public long? ModifiedBy { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? PasswordChangedDateUtc { get; set; }

    public bool ForcePasswordChange { get; set; }

    public string? GivenName { get; set; }

    public string? Surname { get; set; }
}
