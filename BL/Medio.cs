using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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


                    if (query.Count > 0) //query != null && query.ToList().Count > 0
                    {

                        result.Objects = new List<object>();

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

        public static ML.Result AddMedio(ML.Medio medio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MedioAdd '{medio.Nombre}','{medio.Archivo}','{medio.Descripcion}','{medio.Disponibilidad}', @Imagen, '{medio.Autor.IdAutor}','{medio.Idioma.IdIdioma}','{medio.TipoMedio.IdTipoMedio}','{medio.Editorial.IdEditorial}'", new SqlParameter("@Imagen", medio.Imagen));
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se agrego correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error al realizar la consulta. " + result.Ex;
                throw;
            }

            return result;
        }

        public static ML.Result UpdateMedio(ML.Medio medio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MedioUpdate '{medio.IdMedio}', '{medio.Nombre}','{medio.Archivo}','{medio.Descripcion}','{medio.Disponibilidad}', @Imagen, '{medio.Autor.IdAutor}','{medio.Idioma.IdIdioma}','{medio.TipoMedio.IdTipoMedio}','{medio.Editorial.IdEditorial}'", new SqlParameter("@Imagen", medio.Imagen));
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se actualizó correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error al realizar la consulta. " + result.Ex;
                throw;
            }

            return result;
        }
        public static Result DeleteMedio(int idMedio)
        {
            ML.Result result = new ML.Result();
           // ML.Medio medio = new ML.Medio();
            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"MedioDelete {idMedio}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se elimino correctamente.";
                    }
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "No puede ser eliminado correctamente  " + result.Ex;

            }

            return result;
        }
        public static ML.Result GetByIdMedio(int idMedio)
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
                                 where medioLINQ.IdMedio == idMedio

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
                                 }).FirstOrDefault();



                    if (query != null)
                    {
                        var obj = query;
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



                        result.Object = medio;
                        result.Correct = true;
                    }
                    else
                    {

                        result.Message = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error al realizar la consulta. " + result.Ex;
                throw;
            }

            return result;
        }

        public static ML.Result ChangeStatus(int idMedio, bool disponibilidad)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                   var query = context.Database.ExecuteSqlRaw($"MedioDisponibilidad {idMedio}, '{disponibilidad}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error al realizar la consulta. " + result.Ex;
                throw;
            }
            return result;
        }
    }
}
