using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class EstadoRutaRepository : Repository<EstadoRuta>, IEstadoRutaRepository
    {
        private readonly DeliveryContext dbcontext;

        public EstadoRutaRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

