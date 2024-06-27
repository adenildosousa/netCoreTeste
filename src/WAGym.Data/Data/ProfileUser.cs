using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class ProfileUser
{
    public long Id { get; set; }

    public long PersonId { get; set; }

    public long UserId { get; set; }

    public long ProfileId { get; set; }

    public long CompanyId { get; set; }

    public long UserUpdateId { get; set; }

    public bool Default { get; set; }

    public virtual Profile Profile { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
