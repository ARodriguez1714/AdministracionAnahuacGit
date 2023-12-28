using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medio
    {
        public byte? IdMedio{ get; set; }
        public string Nombre { get; set; }
        public string Archivo { get; set; }
        public string Descripcion { get; set; }
        public bool? Disponibilidad { get; set; }
        public byte[] Imagen { get; set; }
        public ML.Autor Autor { get; set; }
        public ML.Idioma Idioma { get; set; }
        public ML.Editorial Editorial { get; set; }
        public ML.TipoMedio TipoMedio { get; set; }
        public List<object> Medios { get; set; }
    }
}
