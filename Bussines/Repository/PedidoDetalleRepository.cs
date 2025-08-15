using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class PedidoDetalleRepository : Repository<PedidoDetalle>, IPedidoDetalleRepository
    {
        private readonly DeliveryContext dbcontext;

        public PedidoDetalleRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

