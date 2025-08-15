using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Repository.Interfaces
{
    public interface IUnitWork:IDisposable
    {
        
        void Save();
    }
}
