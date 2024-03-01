using System;
using System.Collections.Generic;

namespace MotorcycleBusinessManagement.Models;

public partial class Nhacungcap
{
    public int Idncc { get; set; }

    public string Mancc { get; set; } = null!;

    public string? Tenncc { get; set; }

    public string? Diachincc { get; set; }

    public string? Dienthoaincc { get; set; }

    public string? Cccdncc { get; set; }

    public string? Email { get; set; }

    public string? Quequan { get; set; }

    public string? Masothuencc { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Phieunhapkho> Phieunhapkhos { get; set; } = new List<Phieunhapkho>();
}
