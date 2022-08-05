using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relive.Server.Core.Entities.Errors
{
    public class Error
    {
        public int ErrorCode { get; set; } = 1;
        public string Message { get; set; }
    }
}
