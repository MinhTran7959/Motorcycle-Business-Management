using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Phieuthutienbanxe
{
    public int Idpt { get; set; }

    public string Sopt { get; set; } = null!;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Ngaythu { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public double? Tongtienthu { get; set; }

    public int? Idhttt { get; set; }

    public int? Idnv { get; set; }

    public bool? Active { get; set; }

    public virtual Httt? IdhtttNavigation { get; set; }

    public virtual Nhanvien? IdnvNavigation { get; set; }

    public virtual List<Noidungthu> Noidungthus { get; set; } = new List<Noidungthu>();

    public virtual List<Phieuxuat> Phieuxuats { get; set; } = new List<Phieuxuat>();
}
