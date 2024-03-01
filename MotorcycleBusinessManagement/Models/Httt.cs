using System;
using System.Collections.Generic;

namespace MotorcycleBusinessManagement.Models;

public partial class Httt
{
    public int Idhttt { get; set; }

    public string? Mahttt { get; set; }

    public string? Tenhttt { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Phieuchitiennhapxe> Phieuchitiennhapxes { get; set; } = new List<Phieuchitiennhapxe>();

    public virtual ICollection<Phieuthutienbanxe> Phieuthutienbanxes { get; set; } = new List<Phieuthutienbanxe>();
}
