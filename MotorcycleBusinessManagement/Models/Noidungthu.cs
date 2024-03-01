using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Noidungthu
{
    public int Idndt { get; set; }

    public int? Idpt { get; set; }

    public int? Idct { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
    public double? Sotienthu { get; set; }

    public string? Biensoxe { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual Chitietxe? IdctNavigation { get; set; }

    public virtual Phieuthutienbanxe? IdptNavigation { get; set; }
}
