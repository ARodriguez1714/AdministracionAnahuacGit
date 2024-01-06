using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoMedio
    {
            public static ML.Result GetAllTipoMedio()
            {
                ML.Result result = new ML.Result();

                try
                {
                    using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                    {
                        var query = (from tipoMedio in context.TipoMedios
                                     select new
                                     {
                                         IdTipoMedio = tipoMedio.IdTipoMedio,
                                         Nombre = tipoMedio.Nombre
                                     }).ToList();

                        if (query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.TipoMedio tipoMedio = new ML.TipoMedio();

                                tipoMedio.IdTipoMedio = item.IdTipoMedio;
                                tipoMedio.Nombre = item.Nombre;

                                result.Objects.Add(tipoMedio);
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
        public static ML.Result AddTipoMedio(ML.TipoMedio tipoMedio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"TipoMedioAdd '{tipoMedio.Nombre}'");
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

        public static ML.Result UpdateTipoMedio(ML.TipoMedio tipoMedio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"TipoMedioUpdate '{tipoMedio.IdTipoMedio}', '{tipoMedio.Nombre}'");
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

        public static Result DeleteTipoMedio(int idTipoMedio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"TipoMedioDelete {idTipoMedio}");

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
        public static ML.Result GetByIdTipoMedio(int idTipoMedio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from tipoMedioLINQ in context.TipoMedios
                                 where tipoMedioLINQ.IdTipoMedio == idTipoMedio
                                 select new
                                 {
                                     IdTipoMedio = tipoMedioLINQ.IdTipoMedio,
                                     Nombre = tipoMedioLINQ.Nombre,
                                 }).FirstOrDefault();



                    if (query != null)
                    {
                        var obj = query;
                        ML.TipoMedio tipoMedio = new ML.TipoMedio();

                        tipoMedio.IdTipoMedio = obj.IdTipoMedio;
                        tipoMedio.Nombre = obj.Nombre;

                        result.Object = tipoMedio;
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
