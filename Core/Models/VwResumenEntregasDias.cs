using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class VwResumenEntregasDias
{
    public DateOnly Fecha { get; set; }

    public string? Zona { get; set; }

    public byte EstadoRutaId { get; set; }

    public string EstadoPedidoCodigo { get; set; } = null!;

    public int? Paradas { get; set; }

    public int? Entregadas { get; set; }

    public int? Fallidas { get; set; }
}

public static class VwResumenEntregasDiasExtension
{
    public static ModelBuilder VwResumenEntregasDiasMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<VwResumenEntregasDias>();

        entity
            .HasNoKey()
            .ToView("vw_ResumenEntregasDia");

        entity.Property(e => e.EstadoPedidoCodigo)
            .HasMaxLength(30)
            .IsUnicode(false);
        entity.Property(e => e.Zona)
            .HasMaxLength(60);

        return modelBuilder;
    }
}
