using Bussines.Repository;
using Bussines.Repository.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bussines.Repository
{
    public class VwResumenEntregaDiasRepository : Repository<VwResumenEntregasDias>, IVwResumenEntregaDiasRepository
    {
        private readonly DeliveryContext dbcontext;

        public VwResumenEntregaDiasRepository(DeliveryContext _context) : base(_context)
        {
            this.dbcontext = _context;
        }


    }
}

