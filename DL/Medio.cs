using System;
using System.Collections.Generic;

namespace DL;

public partial class Medio
{
    public byte IdMedio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Archivo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool? Disponibilidad { get; set; }

    public byte[]? Imagen { get; set; }

    public byte? IdAutor { get; set; }

    public byte? IdIdioma { get; set; }

    public byte? IdTipoMedio { get; set; }

    public byte? IdEditorial { get; set; }

    public virtual Autor? IdAutorNavigation { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }

    public virtual Idioma? IdIdiomaNavigation { get; set; }

    public virtual TipoMedio? IdTipoMedioNavigation { get; set; }
}
