using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        public byte? IdMunicipio { get; set; }
        [Display(Name = "Municipio:")]
        public string? Nombre { get; set; }
        public ML.Estado? Estado { get; set; }
        public List<object>? Municipios { get; set; }
    }
}
