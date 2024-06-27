using System;
using System.Collections.Generic;

namespace WAGym.Data.Data;

public partial class UserLog
{
    public long? Id { get; set; }

    public long PersonId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int StatusId { get; set; }

    public long? UserUpdateId { get; set; }

    public int Operation { get; set; }

    public string OperationNote { get; set; } = null!;

    public DateTime OperationDate { get; set; }

    public long? CompanyId { get; set; }

    public virtual User? IdNavigation { get; set; }

    public virtual User? UserUpdate { get; set; }
}
