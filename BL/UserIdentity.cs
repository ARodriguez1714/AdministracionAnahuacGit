using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserIdentity
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from user in context.AspNetUsers
                                 select new
                                 {
                                     idUser = user.Id,
                                     Nombre = user.UserName
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.UserIdentity user = new ML.UserIdentity();

                            user.IdUsuario = item.idUser;
                            user.UserName = item.Nombre;

                            result.Objects.Add(user);
                        }
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

        public static ML.Result Asignar(ML.UserIdentity user)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query =
                        context.Database.ExecuteSqlRaw($"AddAspNetUserRoles '{user.IdUsuario}', '{user.RoleId}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El rol se asigno correctamente.";
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


        public static ML.Result GetByEmail(string? email)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from user in context.AspNetUsers
                                 where user.Email == email
                                 select new
                                 {
                                     idUser = user.Id,
                                     Nombre = user.UserName,
                                     PasswordHash = user.PasswordHash,
                                     Email = user.Email
                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        var item = query;
                        ML.UserIdentity user = new ML.UserIdentity();

                        user.IdUsuario = item.idUser;
                        user.UserName = item.Nombre;
                        user.Password = item.PasswordHash;
                        user.Email = item.Email;

                        result.Object = user;
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
