using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class FunctionalityProfile
{
    public int Id { get; set; }

    public long CompanyId { get; set; }

    public long ProfileId { get; set; }

    public int FunctionalityId { get; set; }

    public long UserUpdateId { get; set; }

    public virtual Functionality Functionality { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;

    public virtual User UserUpdate { get; set; } = null!;
}
