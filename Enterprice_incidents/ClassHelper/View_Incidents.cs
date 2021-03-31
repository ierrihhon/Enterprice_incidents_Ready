using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprice_incidents.Ef
{
    public partial class View_Incidents
    {
        public string ViewFIO { get => $"{FName} {LName} {SName}"; }
    }
}
