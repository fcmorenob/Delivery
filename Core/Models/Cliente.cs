using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Cliente
{
    public long ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Departamento { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}

public static class ClienteExtension

{
    public static ModelBuilder ClienteMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Cliente>();
              
        entity.ToTable("Cliente");

        entity.HasIndex(e => e.Email, "UX_Cliente_Email").IsUnique();

        entity.Property(e => e.Apellido)
            .HasMaxLength(80)
            .IsUnicode(false);
        entity.Property(e => e.Ciudad)
            .HasMaxLength(80)
            .IsUnicode(false);
        entity.Property(e => e.Departamento)
            .HasMaxLength(80)
            .IsUnicode(false);
        entity.Property(e => e.Direccion)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.Email)
            .HasMaxLength(256)
            .IsUnicode(false);
        entity.Property(e => e.FechaRegistro)
            .HasPrecision(3)
            .HasDefaultValueSql("(sysutcdatetime())");
        entity.Property(e => e.Nombre)
            .HasMaxLength(80)
            .IsUnicode(false);
        entity.Property(e => e.Telefono)
            .HasMaxLength(30)
            .IsUnicode(false);
        return modelBuilder;
    }
}
