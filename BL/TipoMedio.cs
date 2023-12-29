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
        }
}
