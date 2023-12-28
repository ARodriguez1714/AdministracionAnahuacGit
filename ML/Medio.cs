using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medio
    {
        public string IdMedio{ get; set; }
        public string UserName { get; set; }
        public ML.Autor Autor { get; set; }
        public List<object> IdentityUsers { get; set; }
    }
}
