using Bussines.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly DeliveryContext context;
        public UnitWork(DeliveryContext _context)
        {
            context = _context;
            ClienteRepository = new ClienteRepository(context);
            ConductorRepository = new ConductorRepository(context);
            EstadoParadaRepository = new EstadoParadaRepository(context);
            EstadoPedidoRepository = new EstadoPedidoRepository(context);
            EstadoRutaRepository = new EstadoRutaRepository(context);
            HistorialEstadoPedidoRepository = new HistorialEstadoPedidoRepository(context);
            ParadaEntregaRepository = new ParadaEntregaRepository(context);
            PedidoDetalleRepository = new PedidoDetalleRepository(context);
            PedidoRepository = new PedidoRepository(context);
            RutaEntregaRepository = new RutaEntregaRepository(context);
            TiendaRepository = new TiendaRepository(context);
            VehiculoRepository = new VehiculoRepository(context);
            VwResumenEntregaDiasRepository = new VwResumenEntregaDiasRepository(context);
        }

        public IClienteRepository ClienteRepository { get; set; }

        public IConductorRepository ConductorRepository { get; set; }

        public IEstadoParadaRepository EstadoParadaRepository { get; set; }

        public IEstadoPedidoRepository EstadoPedidoRepository { get; set; }

        public IEstadoRutaRepository EstadoRutaRepository { get; set; }

        public IhistorialEstadoPedidoRepository HistorialEstadoPedidoRepository { get; set; }

        public IParadaEntregaRepository ParadaEntregaRepository { get; set; }
        public IPedidoDetalleRepository PedidoDetalleRepository { get; set; }

        public IPedidoRepository PedidoRepository { get; set; }

        public IRutaEntregaRepository RutaEntregaRepository { get; set; }

        public ITiendaRepository TiendaRepository { get; set; }

        public IVehiculoRepository VehiculoRepository { get; set; }

        public IVwResumenEntregaDiasRepository VwResumenEntregaDiasRepository { get; set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
