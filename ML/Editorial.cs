using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Editorial
    {
        public byte? IdEditorial { get; set; }
        public string Nombre { get; set; }
        public List<object> Editoriales { get; set; }

        public ML.Direccion? Direccion { get; set; }
    }
}
