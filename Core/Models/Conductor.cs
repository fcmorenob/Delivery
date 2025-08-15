using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Conductor
{
    public int ConductorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string DocumentoIdentidad { get; set; } = null!;

    public string? Telefono { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<RutaEntrega> RutaEntregas { get; set; } = new List<RutaEntrega>();
}

public static class ConductorExtension

{
    public static ModelBuilder ConductorMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Conductor>();

       
            entity.ToTable("Conductor");

            entity.HasIndex(e => e.DocumentoIdentidad, "UX_Conductor_Documento").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellido)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.DocumentoIdentidad)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
       
        return modelBuilder;
    }
}
