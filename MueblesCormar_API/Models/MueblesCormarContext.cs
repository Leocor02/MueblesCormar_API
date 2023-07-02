using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MueblesCormar_API.Models
{
    public partial class MueblesCormarContext : DbContext
    {
        public MueblesCormarContext()
        {
        }

        public MueblesCormarContext(DbContextOptions<MueblesCormarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bitacora> Bitacoras { get; set; } = null!;
        public virtual DbSet<DetalleRegistro> DetalleRegistros { get; set; } = null!;
        public virtual DbSet<Inventario> Inventarios { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<ProveedorInventario> ProveedorInventarios { get; set; } = null!;
        public virtual DbSet<Registro> Registros { get; set; } = null!;
        public virtual DbSet<RolUsuario> RolUsuarios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(@"SERVER=.\SQLEXPRESS;DATABASE=MueblesCormar;INTEGRATED SECURITY=TRUE; USER Id=;Password=");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.Idbitacora)
                    .HasName("PK__Bitacora__AE822D9A326491AD");

                entity.ToTable("Bitacora");

                entity.Property(e => e.Idbitacora).HasColumnName("IDBitacora");

                entity.Property(e => e.Accion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Bitacoras)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario");
            });

            modelBuilder.Entity<DetalleRegistro>(entity =>
            {
                entity.HasKey(e => e.IddetalleRegistro)
                    .HasName("PK__DetalleR__A225A9439C9388EA");

                entity.ToTable("DetalleRegistro");

                entity.Property(e => e.IddetalleRegistro).HasColumnName("IDDetalleRegistro");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.Idregistro).HasColumnName("IDRegistro");

                entity.Property(e => e.Impuestos).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PrecioUnidad).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Subtotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.DetalleRegistros)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producto");

                entity.HasOne(d => d.IdregistroNavigation)
                    .WithMany(p => p.DetalleRegistros)
                    .HasForeignKey(d => d.Idregistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registro");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.Idproducto)
                    .HasName("PK__Inventar__ABDAF2B4060F540F");

                entity.ToTable("Inventario");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ImagenProducto)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnidad).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.Idproveedor)
                    .HasName("PK__Proveedo__4CD73240990AC627");

                entity.ToTable("Proveedor");

                entity.Property(e => e.Idproveedor).HasColumnName("IDProveedor");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProveedorInventario>(entity =>
            {
                entity.HasKey(e => e.IdproveedorInventario)
                    .HasName("PK__Proveedo__9B20D788E86A967B");

                entity.ToTable("ProveedorInventario");

                entity.Property(e => e.IdproveedorInventario).HasColumnName("IDProveedorInventario");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.Idproveedor).HasColumnName("IDProveedor");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.ProveedorInventarios)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventario");

                entity.HasOne(d => d.IdproveedorNavigation)
                    .WithMany(p => p.ProveedorInventarios)
                    .HasForeignKey(d => d.Idproveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proveedor");
            });

            modelBuilder.Entity<Registro>(entity =>
            {
                entity.HasKey(e => e.Idregistro)
                    .HasName("PK__Registro__DFE4ED54E3FE5AA9");

                entity.ToTable("Registro");

                entity.Property(e => e.Idregistro).HasColumnName("IDRegistro");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Nota)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Registros)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona");
            });

            modelBuilder.Entity<RolUsuario>(entity =>
            {
                entity.HasKey(e => e.IdrolUsuario)
                    .HasName("PK__RolUsuar__C2954FF18F64153B");

                entity.ToTable("RolUsuario");

                entity.Property(e => e.IdrolUsuario).HasColumnName("IDRolUsuario");

                entity.Property(e => e.Rol)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__Usuario__52311169BB171F86");

                entity.ToTable("Usuario");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdrolUsuario).HasColumnName("IDRolUsuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdrolUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdrolUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolUsuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
