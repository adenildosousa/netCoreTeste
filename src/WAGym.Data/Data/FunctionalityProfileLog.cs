﻿using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class FunctionalityProfileLog
{
    public int Id { get; set; }

    public long CompanyId { get; set; }

    public long ProfileId { get; set; }

    public int FunctionalityId { get; set; }

    public long UserUpdateId { get; set; }

    public int Operation { get; set; }

    public string OperationNote { get; set; } = null!;

    public DateTime OperationDate { get; set; }

    public virtual Profile Profile { get; set; } = null!;

    public virtual User UserUpdate { get; set; } = null!;
}