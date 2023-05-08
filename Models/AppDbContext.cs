using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace gestionServiciosVirtuales.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<ServicioEstado> ServicioEstados { get; set; } = null!;
        public virtual DbSet<Solicitante> Solicitantes { get; set; } = null!;

        public virtual DbSet<TipoServicio> TipoServicio { get; set; } = null!;

        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("Servicio");

                entity.Property(e => e.ServicioId).HasColumnName("servicioId");

                entity.Property(e => e.ServicioDescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("servicioDescripcion");

                entity.Property(e => e.ServicioEstado).HasColumnName("servicioEstado");

                entity.Property(e => e.ServicioFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("servicioFechaCreacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ServicioTitulo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("servicioTitulo");

                entity.Property(e => e.UsuarioId)
                    .HasMaxLength(450)
                    .HasColumnName("usuarioId");

                entity.HasOne(d => d.ServicioEstadoNavigation)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.ServicioEstado)
                    .HasConstraintName("FK__Servicio__servic__5EBF139D");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Servicio__usuari__60A75C0F");
            });

            modelBuilder.Entity<ServicioEstado>(entity =>
            {
                entity.ToTable("ServicioEstado");

                entity.Property(e => e.ServicioEstadoId).HasColumnName("servicioEstadoId");

                entity.Property(e => e.ServicioEstadoDescripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("servicioEstadoDescripcion");
            });

            modelBuilder.Entity<Solicitante>(entity =>
            {
                

                entity.ToTable("Solicitante");

                entity.Property(e => e.Carrera)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("carrera");

                entity.Property(e => e.Facultad)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("facultad");

                entity.Property(e => e.Matricula)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("matricula");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.SolicitanteId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("solicitanteId");

                entity.Property(e => e.UsuarioId)
                    .HasMaxLength(450)
                    .HasColumnName("usuarioId");

                entity.HasOne(d => d.Usuario)
                    .WithMany()
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Solicitan__usuar__628FA481");
            });

           

            OnModelCreatingPartial(modelBuilder);
           
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
