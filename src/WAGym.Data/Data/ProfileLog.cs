using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class ProfileLog
{
    public long Id { get; set; }

    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int StatusId { get; set; }

    public long UserUpdateId { get; set; }

    public int Operation { get; set; }

    public string OperationNote { get; set; } = null!;

    public DateTime OperationDate { get; set; }

    public virtual Profile Profile { get; set; } = null!;

    public virtual User UserUpdate { get; set; } = null!;
}
