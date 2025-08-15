using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Repository.Interfaces
{
    public interface IUnitWork:IDisposable
    {
        IClienteRepository ClienteRepository { get; }
        IConductorRepository ConductorRepository { get; }
        IEstadoParadaRepository EstadoParadaRepository { get; }
        IEstadoPedidoRepository EstadoPedidoRepository { get; }
        IEstadoRutaRepository EstadoRutaRepository { get; }
        IhistorialEstadoPedidoRepository HistorialEstadoPedidoRepository { get; }
        IParadaEntregaRepository ParadaEntregaRepository { get; }
        IPedidoDetalleRepository PedidoDetalleRepository { get; }
        IPedidoRepository PedidoRepository { get; }
        IRutaEntregaRepository RutaEntregaRepository { get; }
        ITiendaRepository TiendaRepository { get; }
        IVehiculoRepository VehiculoRepository { get; }
        IVwResumenEntregaDiasRepository VwResumenEntregaDiasRepository { get; }

        void Save();
    }
}
