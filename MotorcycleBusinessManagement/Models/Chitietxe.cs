using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Chitietxe
{
    public int Idct { get; set; }

    public int? Idpnk { get; set; }

    public int? Idxe { get; set; }

    public int? Idmx { get; set; }

    public int? Soluong { get; set; }

    public string? Doixe { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}")]
    public double? Gianhap { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public double? Giaban { get; set; }

    public string? Sokhung { get; set; }

    public string? Somay { get; set; }

    public string? Trangthai { get; set; }

    public int? Idpxk { get; set; }

    public string? Biensolucmua { get; set; }

    public string? Biensolucban { get; set; }

    public double? Thoigianbaohanh { get; set; }

    public bool? Active { get; set; }

    public virtual Mauxe? IdmxNavigation { get; set; }

    public virtual Phieunhapkho? IdpnkNavigation { get; set; }

    public virtual Phieuxuat? IdpxkNavigation { get; set; }

    public virtual Xe? IdxeNavigation { get; set; }

    public virtual List<Noidungchi> Noidungchis { get; set; } = new List<Noidungchi>();

    public virtual List<Noidungthu> Noidungthus { get; set; } = new List<Noidungthu>();
}
