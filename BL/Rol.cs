using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetByIdRol(Guid idRole)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = context.AspNetRoles.FromSqlRaw($"GetByIdRoles '{idRole}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        var item = query;
                        ML.Rol rol = new ML.Rol();
                        rol.RoleId = Guid.Parse(item.Id);
                        rol.Name = item.Name;

                        result.Object = rol;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = true;
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
