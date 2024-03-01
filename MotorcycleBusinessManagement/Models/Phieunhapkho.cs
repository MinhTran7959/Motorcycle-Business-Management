using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Phieunhapkho
{
    public int Idpnk { get; set; }

    public string Mapnk { get; set; } = null!;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Ngaynhap { get; set; }

    public string? Sohd { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Ngayhd { get; set; }

    public bool? Dachi { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public double? Tongtiennhap { get; set; }

    public int? Idncc { get; set; }

    public int? Idnv { get; set; }

    public int? Idpc { get; set; }

    public bool? Active { get; set; }

    public virtual List<Chitietxe> Chitietxes { get; set; } = new List<Chitietxe>();

    public virtual Nhacungcap? IdnccNavigation { get; set; }

    public virtual Nhanvien? IdnvNavigation { get; set; }

    public virtual Phieuchitiennhapxe? IdpcNavigation { get; set; }
}
