using System;
using System.Collections.Generic;

namespace DL;

public partial class Editorial
{
    public byte IdEditorial { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
