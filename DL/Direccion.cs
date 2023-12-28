using System;
using System.Collections.Generic;

namespace DL;

public partial class Direccion
{
    public byte IdDireccion { get; set; }

    public string Calle { get; set; } = null!;

    public string NumeroInterior { get; set; } = null!;

    public string NumeroExterior { get; set; } = null!;

    public byte? IdColonia { get; set; }

    public byte? IdEditorial { get; set; }

    public virtual Colonium? IdColoniaNavigation { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }
}
