using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprice_incidents.Ef
{
    class DataClass
    {
        public static Enterprice_incidents__Entities Context { get; } = new Enterprice_incidents__Entities();
    }

    public partial class Worker
    {
        public string FIO { get => $"{FName} {SName} {LName}"; }
    }
}
