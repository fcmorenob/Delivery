using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class EstadoPedidoRepository : Repository<EstadoPedido>, IEstadoPedidoRepository
    {
        private readonly DeliveryContext dbcontext;

        public EstadoPedidoRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

