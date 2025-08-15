using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public enum ErrorCode
    {
        ok = 0,
        Unautorized = 100,
        InvalidCredentials = 110,
        AccountLocked = 120,
        RepeatInformation= 150,
        NotFound=160,
        ServerError = 500,
        InvalidRequest= 400,
        NotUpdate=410
    }
}
