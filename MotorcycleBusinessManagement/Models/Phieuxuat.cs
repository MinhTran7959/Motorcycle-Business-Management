using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Phieuxuat
{
    public int Idpxk { get; set; }

    public string Sopxk { get; set; } = null!;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Ngayxuat { get; set; }

    public string? Sohd { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Ngayhd { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public double? Tongtienxuat { get; set; }

    public bool? Dathu { get; set; }

    public int? Idnv { get; set; }

    public int? Idpt { get; set; }

    public int? Idkh { get; set; }

    public bool? Active { get; set; }

    public virtual List<Chitietxe> Chitietxes { get; set; } = new List<Chitietxe>();

    public virtual Khachhang? IdkhNavigation { get; set; }

    public virtual Nhanvien? IdnvNavigation { get; set; }

    public virtual Phieuthutienbanxe? IdptNavigation { get; set; }
}
