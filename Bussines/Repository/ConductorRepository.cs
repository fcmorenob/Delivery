using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class ConductorRepository : Repository<Conductor>, IConductorRepository
    {
        private readonly DeliveryContext dbcontext;

        public ConductorRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

