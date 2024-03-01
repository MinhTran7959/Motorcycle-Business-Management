using System;
using System.Collections.Generic;

namespace MotorcycleBusinessManagement.Models;

public partial class Loaixe
{
    public int Idlx { get; set; }

    public string Malx { get; set; } = null!;

    public string? Tenlx { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
