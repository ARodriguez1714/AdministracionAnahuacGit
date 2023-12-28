using System;
using System.Collections.Generic;

namespace DL;

public partial class Municipio
{
    public byte IdMunicipio { get; set; }

    public string Nombre { get; set; } = null!;

    public byte? IdEstado { get; set; }

    public virtual ICollection<Colonium> Colonia { get; set; } = new List<Colonium>();

    public virtual Estado? IdEstadoNavigation { get; set; }
}
