using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class Functionality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<FunctionalityProfile> FunctionalityProfiles { get; set; } = new List<FunctionalityProfile>();
}
