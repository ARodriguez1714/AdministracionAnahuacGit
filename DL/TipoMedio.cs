using System;
using System.Collections.Generic;

namespace DL;

public partial class TipoMedio
{
    public byte IdTipoMedio { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
