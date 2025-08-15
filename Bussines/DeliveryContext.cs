using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;



namespace Bussines;

public partial class DeliveryContext : DbContext

{ 
    public DeliveryContext(DbContextOptions<DeliveryContext> contextOptions):base(contextOptions)
    {
    }


    #region "Dbset Tablas"

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Conductor> Conductors { get; set; }

    public virtual DbSet<EstadoParada> EstadoParada { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<EstadoRuta> EstadoRuta { get; set; }

    public virtual DbSet<HistorialEstadoPedido> HistorialEstadoPedidos { get; set; }

    public virtual DbSet<ParadaEntrega> ParadaEntregas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoDetalle> PedidoDetalles { get; set; }

    public virtual DbSet<RutaEntrega> RutaEntregas { get; set; }

    public virtual DbSet<Tienda> Tienda { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    public virtual DbSet<VwResumenEntregasDias> VwResumenEntregasDia { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ClienteMapping();
        modelBuilder.ConductorMapping();
        modelBuilder.EstadoParadaMapping();
        modelBuilder.EstadoParadaMapping();
        modelBuilder.EstadoRutaMapping();
        modelBuilder.HistorialEstadoPedidoMapping();
        modelBuilder.ParadaEntregaMapping();
        modelBuilder.PedidoMapping();
        modelBuilder.PedidoDetalleMapping();
        modelBuilder.RutaEntregaMapping();
        modelBuilder.TiendaMapping();
        modelBuilder.VehiculoMapping();
        modelBuilder.VwResumenEntregasDiasMapping();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
