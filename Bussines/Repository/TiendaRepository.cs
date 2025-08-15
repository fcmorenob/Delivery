using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class TiendaRepository : Repository<Tienda>, ITiendaRepository
    {
        private readonly DeliveryContext dbcontext;

        public TiendaRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

