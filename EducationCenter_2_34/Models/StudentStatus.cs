using System;
using System.Collections.Generic;

namespace EducationCenter_2_34.Models;

public partial class StudentStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
