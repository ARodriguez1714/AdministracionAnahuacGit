using System;
using System.Collections.Generic;

namespace DL;

public partial class TipoAutor
{
    public byte IdTipoAutor { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Autor> Autors { get; set; } = new List<Autor>();
}
