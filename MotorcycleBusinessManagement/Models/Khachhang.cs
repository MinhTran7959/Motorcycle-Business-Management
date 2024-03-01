using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Khachhang
{
    public int Idkh { get; set; }

    public string Makh { get; set; } = null!;

    public string? Tenkh { get; set; }

    public string? Diachikh { get; set; }

    public string? Dienthoaikh { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Ngaysinh { get; set; }

    public string? Cccdkh { get; set; }

    public string? Emailkh { get; set; }

    public string? Quequankh { get; set; }

    public string? Masothuekh { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Phieuxuat> Phieuxuats { get; set; } = new List<Phieuxuat>();
}
