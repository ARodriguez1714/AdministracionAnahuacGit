using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medio
    {
        public static ML.Result GetAllMedio()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from medioLINQ in context.Medios
                                 join autorLINQ in context.Autors on medioLINQ.IdAutor equals autorLINQ.IdAutor
                                 join idiomaLINQ in context.Idiomas on medioLINQ.IdIdioma equals idiomaLINQ.IdIdioma
                                 join tipoMedioLINQ in context.TipoMedios on medioLINQ.IdTipoMedio equals tipoMedioLINQ.IdTipoMedio
                                 join editorialLINQ in context.Editorials on medioLINQ.IdEditorial equals editorialLINQ.IdEditorial
                                 select new
                                 {
                                     IdMedio = medioLINQ.IdMedio,
                                     Nombre = medioLINQ.Nombre,
                                     Archivo = medioLINQ.Archivo,
                                     Descripcion = medioLINQ.Descripcion,
                                     Disponibilidad = medioLINQ.Disponibilidad,
                                     Imagen = medioLINQ.Imagen,
                                     IdAutor = medioLINQ.IdAutor,
                                     AutorNombre = autorLINQ.Nombre,
                                     AutorApellidoPaterno = autorLINQ.ApellidoPaterno,
                                     AutorApellidoMaterno = autorLINQ.ApellidoMaterno,
                                     AutorFoto = autorLINQ.Foto,
                                     IdIdioma = medioLINQ.IdIdioma,
                                     IdiomaNombre = idiomaLINQ.Nombre,
                                     IdTipoMedio = medioLINQ.IdTipoMedio,
                                     TipoMedioNombre = tipoMedioLINQ.Nombre,
                                     IdEditorial = medioLINQ.IdEditorial,
                                     EditorialNombre = editorialLINQ.Nombre,


                                 }).ToList();

                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Medio medio = new ML.Medio();
                            medio.IdMedio = obj.IdMedio;
                            medio.Nombre = obj.Nombre;
                            medio.Archivo = obj.Archivo;
                            medio.Descripcion = obj.Descripcion;
                            medio.Disponibilidad = obj.Disponibilidad;
                            medio.Imagen = obj.Imagen;

                            medio.Autor = new ML.Autor();
                            medio.Autor.IdAutor = obj.IdAutor.Value;
                            medio.Autor.Nombre = obj.AutorNombre;
                            medio.Autor.ApellidoPaterno = obj.AutorApellidoPaterno;
                            medio.Autor.ApellidoMaterno = obj.AutorApellidoMaterno;
                            medio.Autor.Foto = obj.AutorFoto;

                            medio.Idioma = new ML.Idioma();
                            medio.Idioma.IdIdioma = obj.IdIdioma;
                            medio.Idioma.Nombre = obj.IdiomaNombre;

                            medio.TipoMedio = new ML.TipoMedio();
                            medio.TipoMedio.IdTipoMedio = obj.IdTipoMedio;
                            medio.TipoMedio.Nombre = obj.TipoMedioNombre;

                            medio.Editorial = new ML.Editorial();
                            medio.Editorial.IdEditorial = obj.IdEditorial;
                            medio.Editorial.Nombre = obj.EditorialNombre;

                            result.Objects.Add(medio);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
