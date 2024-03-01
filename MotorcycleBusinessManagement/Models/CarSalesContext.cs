using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleBusinessManagement.Models;

public partial class CarSalesContext : DbContext
{
    public CarSalesContext()
    {
    }

    public CarSalesContext(DbContextOptions<CarSalesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Chitietxe> Chitietxes { get; set; }

    public virtual DbSet<Httt> Httts { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaixe> Loaixes { get; set; }

    public virtual DbSet<Mauxe> Mauxes { get; set; }

    public virtual DbSet<Ndxemauxe> Ndxemauxes { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Noidungchi> Noidungchis { get; set; }

    public virtual DbSet<Noidungthu> Noidungthus { get; set; }

    public virtual DbSet<Phieuchitiennhapxe> Phieuchitiennhapxes { get; set; }

    public virtual DbSet<Phieunhapkho> Phieunhapkhos { get; set; }

    public virtual DbSet<Phieuthutienbanxe> Phieuthutienbanxes { get; set; }

    public virtual DbSet<Phieuxuat> Phieuxuats { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Idacc);

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Idacc).HasColumnName("IDACC");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Email).HasColumnName("EMAIL");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("PHONE");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("ROLE");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<Chitietxe>(entity =>
        {
            entity.HasKey(e => e.Idct);

            entity.ToTable("CHITIETXE");

            entity.Property(e => e.Idct).HasColumnName("IDCT");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Biensolucban)
                .HasMaxLength(50)
                .HasColumnName("BIENSOLUCBAN");
            entity.Property(e => e.Biensolucmua)
                .HasMaxLength(50)
                .HasColumnName("BIENSOLUCMUA");
            entity.Property(e => e.Doixe).HasColumnName("DOIXE");
            entity.Property(e => e.Giaban).HasColumnName("GIABAN");
            entity.Property(e => e.Gianhap).HasColumnName("GIANHAP");
            entity.Property(e => e.Idmx).HasColumnName("IDMX");
            entity.Property(e => e.Idpnk).HasColumnName("IDPNK");
            entity.Property(e => e.Idpxk).HasColumnName("IDPXK");
            entity.Property(e => e.Idxe).HasColumnName("IDXE");
            entity.Property(e => e.Sokhung)
                .HasMaxLength(50)
                .HasColumnName("SOKHUNG");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Somay)
                .HasMaxLength(50)
                .HasColumnName("SOMAY");
            entity.Property(e => e.Thoigianbaohanh).HasColumnName("THOIGIANBAOHANH");
            entity.Property(e => e.Trangthai).HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.IdmxNavigation).WithMany(p => p.Chitietxes)
                .HasForeignKey(d => d.Idmx)
                .HasConstraintName("FK_CT_MX");

            entity.HasOne(d => d.IdpnkNavigation).WithMany(p => p.Chitietxes)
                .HasForeignKey(d => d.Idpnk)
                .HasConstraintName("FK_CT_PNK");

            entity.HasOne(d => d.IdpxkNavigation).WithMany(p => p.Chitietxes)
                .HasForeignKey(d => d.Idpxk)
                .HasConstraintName("FK_CT_PXK");

            entity.HasOne(d => d.IdxeNavigation).WithMany(p => p.Chitietxes)
                .HasForeignKey(d => d.Idxe)
                .HasConstraintName("FK_CT_XE");
        });

        modelBuilder.Entity<Httt>(entity =>
        {
            entity.HasKey(e => e.Idhttt);

            entity.ToTable("HTTT");

            entity.Property(e => e.Idhttt).HasColumnName("IDHTTT");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Mahttt)
                .HasMaxLength(50)
                .HasColumnName("MAHTTT");
            entity.Property(e => e.Tenhttt).HasColumnName("TENHTTT");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Idkh);

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.Idkh).HasColumnName("IDKH");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Cccdkh)
                .HasMaxLength(50)
                .HasColumnName("CCCDKH");
            entity.Property(e => e.Diachikh).HasColumnName("DIACHIKH");
            entity.Property(e => e.Dienthoaikh)
                .HasMaxLength(50)
                .HasColumnName("DIENTHOAIKH");
            entity.Property(e => e.Emailkh).HasColumnName("EMAILKH");
            entity.Property(e => e.Ghichu).HasColumnName("GHICHU");
            entity.Property(e => e.Makh)
                .HasMaxLength(50)
                .HasColumnName("MAKH");
            entity.Property(e => e.Masothuekh)
                .HasMaxLength(50)
                .HasColumnName("MASOTHUEKH");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("date")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Quequankh).HasColumnName("QUEQUANKH");
            entity.Property(e => e.Tenkh).HasColumnName("TENKH");
        });

        modelBuilder.Entity<Loaixe>(entity =>
        {
            entity.HasKey(e => e.Idlx);

            entity.ToTable("LOAIXE");

            entity.Property(e => e.Idlx).HasColumnName("IDLX");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Malx)
                .HasMaxLength(50)
                .HasColumnName("MALX");
            entity.Property(e => e.Tenlx).HasColumnName("TENLX");
        });

        modelBuilder.Entity<Mauxe>(entity =>
        {
            entity.HasKey(e => e.Idmx);

            entity.ToTable("MAUXE");

            entity.Property(e => e.Idmx).HasColumnName("IDMX");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Mamx)
                .HasMaxLength(50)
                .HasColumnName("MAMX");
            entity.Property(e => e.Tenmx).HasColumnName("TENMX");
        });

        modelBuilder.Entity<Ndxemauxe>(entity =>
        {
            entity.HasKey(e => e.Idxemau);

            entity.ToTable("NDXEMAUXE");

            entity.Property(e => e.Idxemau).HasColumnName("IDXEMAU");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Idmx).HasColumnName("IDMX");
            entity.Property(e => e.Idxe).HasColumnName("IDXE");

            entity.HasOne(d => d.IdmxNavigation).WithMany(p => p.Ndxemauxes)
                .HasForeignKey(d => d.Idmx)
                .HasConstraintName("FK_MAUXE_XEMX");

            entity.HasOne(d => d.IdxeNavigation).WithMany(p => p.Ndxemauxes)
                .HasForeignKey(d => d.Idxe)
                .HasConstraintName("FK_XE_XEMX");
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.Idncc);

            entity.ToTable("NHACUNGCAP");

            entity.Property(e => e.Idncc).HasColumnName("IDNCC");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Cccdncc)
                .HasMaxLength(50)
                .HasColumnName("CCCDNCC");
            entity.Property(e => e.Diachincc).HasColumnName("DIACHINCC");
            entity.Property(e => e.Dienthoaincc)
                .HasMaxLength(50)
                .HasColumnName("DIENTHOAINCC");
            entity.Property(e => e.Email).HasColumnName("EMAIL");
            entity.Property(e => e.Ghichu).HasColumnName("GHICHU");
            entity.Property(e => e.Mancc)
                .HasMaxLength(50)
                .HasColumnName("MANCC");
            entity.Property(e => e.Masothuencc)
                .HasMaxLength(50)
                .HasColumnName("MASOTHUENCC");
            entity.Property(e => e.Quequan).HasColumnName("QUEQUAN");
            entity.Property(e => e.Tenncc).HasColumnName("TENNCC");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Idnv);

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Cccdnv)
                .HasMaxLength(50)
                .HasColumnName("CCCDNV");
            entity.Property(e => e.Diachinv).HasColumnName("DIACHINV");
            entity.Property(e => e.Dienthoainv)
                .HasMaxLength(50)
                .HasColumnName("DIENTHOAINV");
            entity.Property(e => e.Emailnv).HasColumnName("EMAILNV");
            entity.Property(e => e.Ghichu).HasColumnName("GHICHU");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(50)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Manv)
                .HasMaxLength(50)
                .HasColumnName("MANV");
            entity.Property(e => e.Masothuenv)
                .HasMaxLength(50)
                .HasColumnName("MASOTHUENV");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("date")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Quequannv).HasColumnName("QUEQUANNV");
            entity.Property(e => e.Tennv).HasColumnName("TENNV");
        });

        modelBuilder.Entity<Noidungchi>(entity =>
        {
            entity.HasKey(e => e.Idndc);

            entity.ToTable("NOIDUNGCHI");

            entity.Property(e => e.Idndc).HasColumnName("IDNDC");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Ghichu).HasColumnName("GHICHU");
            entity.Property(e => e.Idct).HasColumnName("IDCT");
            entity.Property(e => e.Idpc).HasColumnName("IDPC");
            entity.Property(e => e.Sotienchi).HasColumnName("SOTIENCHI");

            entity.HasOne(d => d.IdctNavigation).WithMany(p => p.Noidungchis)
                .HasForeignKey(d => d.Idct)
                .HasConstraintName("FK_NDC_CT");

            entity.HasOne(d => d.IdpcNavigation).WithMany(p => p.Noidungchis)
                .HasForeignKey(d => d.Idpc)
                .HasConstraintName("FK_NDC_PC");
        });

        modelBuilder.Entity<Noidungthu>(entity =>
        {
            entity.HasKey(e => e.Idndt);

            entity.ToTable("NOIDUNGTHU");

            entity.Property(e => e.Idndt).HasColumnName("IDNDT");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Biensoxe)
                .HasMaxLength(50)
                .HasColumnName("BIENSOXE");
            entity.Property(e => e.Ghichu).HasColumnName("GHICHU");
            entity.Property(e => e.Idct).HasColumnName("IDCT");
            entity.Property(e => e.Idpt).HasColumnName("IDPT");
            entity.Property(e => e.Sotienthu).HasColumnName("SOTIENTHU");

            entity.HasOne(d => d.IdctNavigation).WithMany(p => p.Noidungthus)
                .HasForeignKey(d => d.Idct)
                .HasConstraintName("FK_NDT_CT");

            entity.HasOne(d => d.IdptNavigation).WithMany(p => p.Noidungthus)
                .HasForeignKey(d => d.Idpt)
                .HasConstraintName("FK_NDT_PT");
        });

        modelBuilder.Entity<Phieuchitiennhapxe>(entity =>
        {
            entity.HasKey(e => e.Idpc);

            entity.ToTable("PHIEUCHITIENNHAPXE");

            entity.Property(e => e.Idpc).HasColumnName("IDPC");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Idhttt).HasColumnName("IDHTTT");
            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.Ngaychi)
                .HasColumnType("date")
                .HasColumnName("NGAYCHI");
            entity.Property(e => e.Sopc)
                .HasMaxLength(50)
                .HasColumnName("SOPC");
            entity.Property(e => e.Tongtienchi).HasColumnName("TONGTIENCHI");

            entity.HasOne(d => d.IdhtttNavigation).WithMany(p => p.Phieuchitiennhapxes)
                .HasForeignKey(d => d.Idhttt)
                .HasConstraintName("FK_PC_HTTT");

            entity.HasOne(d => d.IdnvNavigation).WithMany(p => p.Phieuchitiennhapxes)
                .HasForeignKey(d => d.Idnv)
                .HasConstraintName("FK_PC_NV");
        });

        modelBuilder.Entity<Phieunhapkho>(entity =>
        {
            entity.HasKey(e => e.Idpnk);

            entity.ToTable("PHIEUNHAPKHO");

            entity.Property(e => e.Idpnk).HasColumnName("IDPNK");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Dachi).HasColumnName("DACHI");
            entity.Property(e => e.Idncc).HasColumnName("IDNCC");
            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.Idpc).HasColumnName("IDPC");
            entity.Property(e => e.Mapnk)
                .HasMaxLength(50)
                .HasColumnName("MAPNK");
            entity.Property(e => e.Ngayhd)
                .HasColumnType("date")
                .HasColumnName("NGAYHD");
            entity.Property(e => e.Ngaynhap)
                .HasColumnType("date")
                .HasColumnName("NGAYNHAP");
            entity.Property(e => e.Sohd)
                .HasMaxLength(50)
                .HasColumnName("SOHD");
            entity.Property(e => e.Tongtiennhap).HasColumnName("TONGTIENNHAP");

            entity.HasOne(d => d.IdnccNavigation).WithMany(p => p.Phieunhapkhos)
                .HasForeignKey(d => d.Idncc)
                .HasConstraintName("FK_PNK_NCC");

            entity.HasOne(d => d.IdnvNavigation).WithMany(p => p.Phieunhapkhos)
                .HasForeignKey(d => d.Idnv)
                .HasConstraintName("FK_PNK_NV");

            entity.HasOne(d => d.IdpcNavigation).WithMany(p => p.Phieunhapkhos)
                .HasForeignKey(d => d.Idpc)
                .HasConstraintName("FK_NK_PC");
        });

        modelBuilder.Entity<Phieuthutienbanxe>(entity =>
        {
            entity.HasKey(e => e.Idpt);

            entity.ToTable("PHIEUTHUTIENBANXE");

            entity.Property(e => e.Idpt).HasColumnName("IDPT");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Idhttt).HasColumnName("IDHTTT");
            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.Ngaythu)
                .HasColumnType("date")
                .HasColumnName("NGAYTHU");
            entity.Property(e => e.Sopt)
                .HasMaxLength(50)
                .HasColumnName("SOPT");
            entity.Property(e => e.Tongtienthu).HasColumnName("TONGTIENTHU");

            entity.HasOne(d => d.IdhtttNavigation).WithMany(p => p.Phieuthutienbanxes)
                .HasForeignKey(d => d.Idhttt)
                .HasConstraintName("FK_PT_HTTT");

            entity.HasOne(d => d.IdnvNavigation).WithMany(p => p.Phieuthutienbanxes)
                .HasForeignKey(d => d.Idnv)
                .HasConstraintName("FK_PT_NV");
        });

        modelBuilder.Entity<Phieuxuat>(entity =>
        {
            entity.HasKey(e => e.Idpxk);

            entity.ToTable("PHIEUXUAT");

            entity.Property(e => e.Idpxk).HasColumnName("IDPXK");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Dathu).HasColumnName("DATHU");
            entity.Property(e => e.Idkh).HasColumnName("IDKH");
            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.Idpt).HasColumnName("IDPT");
            entity.Property(e => e.Ngayhd)
                .HasColumnType("date")
                .HasColumnName("NGAYHD");
            entity.Property(e => e.Ngayxuat)
                .HasColumnType("date")
                .HasColumnName("NGAYXUAT");
            entity.Property(e => e.Sohd)
                .HasMaxLength(50)
                .HasColumnName("SOHD");
            entity.Property(e => e.Sopxk)
                .HasMaxLength(50)
                .HasColumnName("SOPXK");
            entity.Property(e => e.Tongtienxuat).HasColumnName("TONGTIENXUAT");

            entity.HasOne(d => d.IdkhNavigation).WithMany(p => p.Phieuxuats)
                .HasForeignKey(d => d.Idkh)
                .HasConstraintName("FK_PX_KH");

            entity.HasOne(d => d.IdnvNavigation).WithMany(p => p.Phieuxuats)
                .HasForeignKey(d => d.Idnv)
                .HasConstraintName("FK_PX_NV");

            entity.HasOne(d => d.IdptNavigation).WithMany(p => p.Phieuxuats)
                .HasForeignKey(d => d.Idpt)
                .HasConstraintName("FK_XK_PT");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.Idxe);

            entity.ToTable("XE");

            entity.Property(e => e.Idxe).HasColumnName("IDXE");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Idlx).HasColumnName("IDLX");
            entity.Property(e => e.Maxe)
                .HasMaxLength(50)
                .HasColumnName("MAXE");
            entity.Property(e => e.Tenxe).HasColumnName("TENXE");

            entity.HasOne(d => d.IdlxNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.Idlx)
                .HasConstraintName("FK_XE_LX");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
