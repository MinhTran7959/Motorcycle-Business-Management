using System;
using System.Collections.Generic;

namespace MotorcycleBusinessManagement.Models;

public partial class Ndxemauxe
{
    public int Idxemau { get; set; }

    public int? Idmx { get; set; }

    public int? Idxe { get; set; }

    public bool? Active { get; set; }

    public virtual Mauxe? IdmxNavigation { get; set; }

    public virtual Xe? IdxeNavigation { get; set; }
}
