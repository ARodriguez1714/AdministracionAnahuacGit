using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Idioma
    {
        public static ML.Result GetAllIdioma()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from idiomaLINQ in context.Idiomas
                                 select new
                                 {
                                     IdIdioma = idiomaLINQ.IdIdioma,
                                     Nombre = idiomaLINQ.Nombre,


                                 }).ToList();

                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Idioma idioma = new ML.Idioma();
                            idioma.IdIdioma = obj.IdIdioma;
                            idioma.Nombre = obj.Nombre;

                            result.Objects.Add(idioma);
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

        public static ML.Result AddIdioma(ML.Idioma idioma)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"IdiomaAdd '{idioma.Nombre}'");
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

        public static ML.Result UpdateIdioma(ML.Idioma idioma)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"IdiomaUpdate {idioma.IdIdioma}, '{idioma.Nombre}'");
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

        public static ML.Result GetByIdIdioma(int idIdioma)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from idiomaLINQ in context.Idiomas
                                 where idiomaLINQ.IdIdioma == idIdioma
                                 select new
                                 {
                                     IdIdioma = idiomaLINQ.IdIdioma,
                                     Nombre = idiomaLINQ.Nombre,
                                 }).FirstOrDefault();



                    if (query != null)
                    {
                        var obj = query;
                        ML.Idioma idioma = new ML.Idioma();

                        idioma.IdIdioma = obj.IdIdioma;
                        idioma.Nombre = obj.Nombre;

                        result.Object = idioma;
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
