using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class Profile
{
    public long Id { get; set; }

    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int StatusId { get; set; }

    public long UserUpdateId { get; set; }

    public virtual ICollection<FunctionalityProfile> FunctionalityProfiles { get; set; } = new List<FunctionalityProfile>();

    public virtual ICollection<ProfileUser> ProfileUsers { get; set; } = new List<ProfileUser>();

    public virtual User UserUpdate { get; set; } = null!;
}
