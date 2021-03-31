using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprice_incidents.Ef
{
    public partial class Worker
    {
        public string GetFIO { get => $"{FName} {LName} {SName}"; }
    }
}
