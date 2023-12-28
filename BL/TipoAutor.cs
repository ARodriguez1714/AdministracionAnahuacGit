using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoAutor
    {
        public static ML.Result GetAllTipoAutor()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from tipoAutor in context.TipoAutors
                                 select new
                                 {
                                     IdTipoAutor = tipoAutor.IdTipoAutor,
                                     Nombre = tipoAutor.Nombre
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.TipoAutor tipoAutor = new ML.TipoAutor();

                            tipoAutor.IdTipoAutor = item.IdTipoAutor;
                            tipoAutor.Nombre = item.Nombre;

                            result.Objects.Add(tipoAutor);
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
