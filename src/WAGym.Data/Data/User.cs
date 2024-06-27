using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class User
{
    public long Id { get; set; }

    public long PersonId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int StatusId { get; set; }

    public long? UserUpdateId { get; set; }

    public long? CompanyId { get; set; }

    public virtual ICollection<FunctionalityProfile> FunctionalityProfiles { get; set; } = new List<FunctionalityProfile>();

    public virtual ICollection<User> InverseUserUpdate { get; set; } = new List<User>();

    public virtual ICollection<ProfileUser> ProfileUsers { get; set; } = new List<ProfileUser>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual User? UserUpdate { get; set; }
}
