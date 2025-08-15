using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Vehiculo
{
    public int VehiculoId { get; set; }

    public string Placa { get; set; } = null!;

    public int? CapacidadKg { get; set; }

    public string? Tipo { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<RutaEntrega> RutaEntregas { get; set; } = new List<RutaEntrega>();
}

public static class VehiculoExtension
{
    public static ModelBuilder VehiculoMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Vehiculo>();

            entity.ToTable("Vehiculo");

            entity.HasIndex(e => e.Placa, "UX_Vehiculo_Placa").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Placa)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(40)
                .IsUnicode(false);
        return modelBuilder;
    }
}