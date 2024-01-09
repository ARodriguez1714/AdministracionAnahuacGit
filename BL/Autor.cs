using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BL
{
    public class Autor
    {
        public static ML.Result GetAllAutor()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from autor in context.Autors
                                 join tipoAutor in context.TipoAutors
                                 on autor.IdTipoAutor equals tipoAutor.IdTipoAutor
                                 select new
                                 {
                                     IdAutor = autor.IdAutor,
                                     NombreAutor = autor.Nombre,
                                     ApellidoPaterno = autor.ApellidoPaterno,
                                     ApellidoMaterno = autor.ApellidoMaterno,
                                     Foto = autor.Foto,
                                     IdTipoAutor = autor.IdTipoAutor,
                                     NombreTipoAutor = tipoAutor.Nombre
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Autor autor = new ML.Autor();

                            autor.IdAutor = item.IdAutor;
                            autor.Nombre = item.NombreAutor;
                            autor.ApellidoPaterno = item.ApellidoPaterno;
                            autor.ApellidoMaterno = item.ApellidoMaterno;
                            autor.Foto = item.Foto;
                            autor.TipoAutor = new ML.TipoAutor();
                            autor.TipoAutor.IdTipoAutor = item.IdTipoAutor.Value;
                            autor.TipoAutor.Nombre = item.NombreTipoAutor;

                            result.Objects.Add(autor);
                        }
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

        public static ML.Result AddAutor(ML.Autor autor)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query =
                        context.Database.ExecuteSqlRaw($"AutorAdd '{autor.Nombre}', '{autor.ApellidoPaterno}', '{autor.ApellidoMaterno}', @Foto, {autor.TipoAutor.IdTipoAutor}", new SqlParameter("@Foto", autor.Foto));

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El autor se agrego correctamente.";
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

        public static ML.Result UpdateAutor(ML.Autor autor)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query =
                        context.Database.ExecuteSqlRaw($"AutorUpdate {autor.IdAutor}, '{autor.Nombre}', '{autor.ApellidoPaterno}', '{autor.ApellidoMaterno}', @Foto, {autor.TipoAutor.IdTipoAutor}", new SqlParameter("@Foto", autor.Foto));

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El autor se actualizó correctamente.";
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

        public static ML.Result GetByIdAutor(int idAutor)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from autor in context.Autors
                                 join tipoAutor in context.TipoAutors
                                 on autor.IdTipoAutor equals tipoAutor.IdTipoAutor
                                 where autor.IdAutor == idAutor
                                 select new
                                 {
                                     IdAutor = autor.IdAutor,
                                     NombreAutor = autor.Nombre,
                                     ApellidoPaterno = autor.ApellidoPaterno,
                                     ApellidoMaterno = autor.ApellidoMaterno,
                                     Foto = autor.Foto,
                                     IdTipoAutor = autor.IdTipoAutor,
                                     NombreTipoAutor = tipoAutor.Nombre
                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        var item = query;
                        ML.Autor autor = new ML.Autor();

                        autor.IdAutor = item.IdAutor;
                        autor.Nombre = item.NombreAutor;
                        autor.ApellidoPaterno = item.ApellidoPaterno;
                        autor.ApellidoMaterno = item.ApellidoMaterno;
                        autor.Foto = item.Foto;
                        autor.TipoAutor = new ML.TipoAutor();
                        autor.TipoAutor.IdTipoAutor = item.IdTipoAutor.Value;
                        autor.TipoAutor.Nombre = item.NombreTipoAutor;

                        result.Object = autor;
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
    }
}