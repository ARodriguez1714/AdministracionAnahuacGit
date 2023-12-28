using System;
using System.Collections.Generic;

namespace DL;

public partial class Estado
{
    public byte IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public byte? IdPais { get; set; }

    public virtual Pai? IdPaisNavigation { get; set; }

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
}
