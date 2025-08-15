using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class HistorialEstadoPedidoRepository : Repository<HistorialEstadoPedido>, IhistorialEstadoPedidoRepository
    {
        private readonly DeliveryContext dbcontext;

        public HistorialEstadoPedidoRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

