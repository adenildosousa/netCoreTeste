using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class ProfileUserLog
{
    public long Id { get; set; }

    public long PersonId { get; set; }

    public long UserId { get; set; }

    public long ProfileId { get; set; }

    public long CompanyId { get; set; }

    public long UserUpdateId { get; set; }

    public int Operation { get; set; }

    public string OperationNote { get; set; } = null!;

    public DateTime OperationDate { get; set; }

    public virtual Profile Profile { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual User UserUpdate { get; set; } = null!;
}
