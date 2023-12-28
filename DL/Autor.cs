using System;
using System.Collections.Generic;

namespace DL;

public partial class Autor
{
    public byte IdAutor { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public byte[]? Foto { get; set; }

    public byte? IdTipoAutor { get; set; }

    public virtual TipoAutor? IdTipoAutorNavigation { get; set; }

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
