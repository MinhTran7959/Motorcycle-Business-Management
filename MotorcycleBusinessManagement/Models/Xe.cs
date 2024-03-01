using System;
using System.Collections.Generic;

namespace MotorcycleBusinessManagement.Models;

public partial class Xe
{
    public int Idxe { get; set; }

    public string Maxe { get; set; } = null!;

    public string? Tenxe { get; set; }

    public int? Idlx { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Chitietxe> Chitietxes { get; set; } = new List<Chitietxe>();

    public virtual Loaixe? IdlxNavigation { get; set; }

    public virtual ICollection<Ndxemauxe> Ndxemauxes { get; set; } = new List<Ndxemauxe>();
}
