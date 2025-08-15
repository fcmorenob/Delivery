using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class EstadoParadaRepository : Repository<EstadoParada>, IEstadoParadaRepository
    {
        private readonly DeliveryContext dbcontext;

        public EstadoParadaRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

