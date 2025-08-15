using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class ParadaEntregaRepository : Repository<ParadaEntrega>, IParadaEntregaRepository
    {
        private readonly DeliveryContext dbcontext;

        public ParadaEntregaRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

