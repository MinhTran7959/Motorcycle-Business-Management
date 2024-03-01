using System;
using System.Collections.Generic;

namespace MotorcycleBusinessManagement.Models;

public partial class Mauxe
{
    public int Idmx { get; set; }

    public string? Mamx { get; set; }

    public string? Tenmx { get; set; }

    public bool? Active { get; set; }

    //public virtual ICollection<Chitietxe> Chitietxes { get; set; } = new List<Chitietxe>();

    //public virtual ICollection<Ndxemauxe> Ndxemauxes { get; set; } = new List<Ndxemauxe>();
    public virtual List<Chitietxe> Chitietxes { get; set; } = new List<Chitietxe>();

    public virtual List<Ndxemauxe> Ndxemauxes { get; set; } = new List<Ndxemauxe>();
}
