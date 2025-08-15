using Bussines.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly DeliveryContext context;
        public UnitWork(DeliveryContext _context)
        {
            context = _context;
           
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
