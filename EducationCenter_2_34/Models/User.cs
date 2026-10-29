using System;
using System.Collections.Generic;

namespace EducationCenter_2_34.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int RoleId { get; set; }

    public int EmplyeeId { get; set; }

    public virtual Employee Emplyee { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
