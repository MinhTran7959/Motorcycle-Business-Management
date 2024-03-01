using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Phieuchitiennhapxe
{
    public int Idpc { get; set; }

    public string Sopc { get; set; } = null!;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Ngaychi { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public double? Tongtienchi { get; set; }

    public int? Idnv { get; set; }

    public int? Idhttt { get; set; }

    public bool? Active { get; set; }

    public virtual Httt? IdhtttNavigation { get; set; }

    public virtual Nhanvien? IdnvNavigation { get; set; }

    public virtual List<Noidungchi> Noidungchis { get; set; } = new List<Noidungchi>();

    public virtual List<Phieunhapkho> Phieunhapkhos { get; set; } = new List<Phieunhapkho>();
}
