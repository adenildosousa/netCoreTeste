using System;
using System.Collections.Generic;
using WAGym.Common.Enum;

namespace WAGym.Data.Data;

public partial class FunctionalityLog
{
    public long Id { get; set; }

    public long ProfileId { get; set; }

    public int FunctionalityId { get; set; }

    public int Operation { get; set; }

    public DateTime OperationDate { get; set; }

    public long CompanyId { get; set; }

    public long UserUpdateId { get; set; }

    public long PersonId { get; set; }

    public string? Note { get; set; }

    public virtual Functionality Functionality { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
