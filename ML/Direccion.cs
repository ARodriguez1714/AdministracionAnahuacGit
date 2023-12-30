using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int? IdDireccion { get; set; }
        [Display(Name = "Calle:")]
        public string? Calle { get; set; }
        [MaxLength(50, ErrorMessage = "*Solo se permiten 50 carácteres")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "*Solo usar letras y números")]
        [Display(Name = "Número Interior:")]
        public string? NumeroInterior { get; set; }
        [MaxLength(50, ErrorMessage = "*Solo se permiten 50 carácteres")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "*Solo usar letras y números")]
        [Display(Name = "Número Exterior:")]
        public string? NumeroExterior { get; set; }
        public ML.Colonia? Colonia { get; set; }
        public ML.Editorial? Editorial { get; set; }
    }
}
