using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorcycleBusinessManagement.Models;

public partial class Nhanvien
{
    public int Idnv { get; set; }

    public string Manv { get; set; } = null!;

    public string? Tennv { get; set; }

    public string? Diachinv { get; set; }

    public string? Dienthoainv { get; set; }

    public string? Emailnv { get; set; }

    public string? Cccdnv { get; set; }

    public string? Hinhanh { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Ngaysinh { get; set; }

    public string? Gioitinh { get; set; }

    public string? Quequannv { get; set; }

    public string? Masothuenv { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }
    //[Required(ErrorMessage = "Please cho Profile Photo")]
    //[Display(Name = "Profile Photo")]
    [NotMapped]
    public IFormFile ProfilePhoto { get; set; }
    public virtual ICollection<Phieuchitiennhapxe> Phieuchitiennhapxes { get; set; } = new List<Phieuchitiennhapxe>();

    public virtual ICollection<Phieunhapkho> Phieunhapkhos { get; set; } = new List<Phieunhapkho>();

    public virtual ICollection<Phieuthutienbanxe> Phieuthutienbanxes { get; set; } = new List<Phieuthutienbanxe>();

    public virtual ICollection<Phieuxuat> Phieuxuats { get; set; } = new List<Phieuxuat>();
}
