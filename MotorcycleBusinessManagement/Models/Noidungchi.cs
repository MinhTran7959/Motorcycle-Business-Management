using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Noidungchi
{
    public int Idndc { get; set; }

    public int? Idpc { get; set; }

    public int? Idct { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
    public double? Sotienchi { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual Chitietxe? IdctNavigation { get; set; }

    public virtual Phieuchitiennhapxe? IdpcNavigation { get; set; }
}
