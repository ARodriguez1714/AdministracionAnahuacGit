using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Editorial
    {
        public static ML.Result GetAllEditorial()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdministracionAnahuacGitContext context = new DL.AdministracionAnahuacGitContext())
                {
                    var query = (from editorialLINQ in context.Editorials
                                 join direccion in context.Direccions
                                 on editorialLINQ.IdEditorial equals direccion.IdEditorial
                                 join colonia in context.Colonia
                                 on direccion.IdColonia equals colonia.IdColonia
                                 join municipio in context.Municipios
                                 on colonia.IdMunicipio equals municipio.IdMunicipio
                                 join estado in context.Estados
                                 on municipio.IdEstado equals estado.IdEstado
                                 join pais in context.Pais
                                 on estado.IdPais equals pais.IdPais
                                 select new
                                 {
                                     IdEditorial = editorialLINQ.IdEditorial,
                                     Nombre = editorialLINQ.Nombre,
                                     IdDireccion = direccion.IdDireccion,
                                     Calle = direccion.Calle,
                                     NumeroInterior = direccion.NumeroInterior,
                                     NumeroExterior = direccion.NumeroExterior,
                                     IdColonia = colonia.IdColonia,
                                     ColoniaNombre = colonia.Nombre,
                                     CodigoPostal = colonia.CodigoPostal,
                                     IdMunicipio = municipio.IdMunicipio,
                                     MunicipioNombre = municipio.Nombre,
                                     IdEstado = estado.IdEstado,
                                     EstadoNombre = estado.Nombre,
                                     IdPais = pais.IdPais,
                                     PaisNombre = pais.Nombre

                                 }).ToList();

                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Editorial editorial = new ML.Editorial();
                            editorial.IdEditorial = obj.IdEditorial;
                            editorial.Nombre = obj.Nombre;
                            //DIRECCION
                            editorial.Direccion = new ML.Direccion();
                            editorial.Direccion.IdDireccion = obj.IdDireccion;
                            editorial.Direccion.Calle = obj.Calle;
                            editorial.Direccion.NumeroInterior = obj.NumeroInterior;
                            editorial.Direccion.NumeroExterior = obj.NumeroExterior;
                            //COLONIA
                            editorial.Direccion.Colonia = new ML.Colonia();
                            editorial.Direccion.Colonia.IdColonia = obj.IdColonia;
                            editorial.Direccion.Colonia.Nombre = obj.ColoniaNombre;
                            editorial.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                            //MUNICIPIO
                            editorial.Direccion.Colonia.Municipio = new ML.Municipio();
                            editorial.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            editorial.Direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre;
                            //ESTADO
                            editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            editorial.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            editorial.Direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre;
                            //Pais
                            editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            editorial.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            editorial.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.PaisNombre;

                            result.Objects.Add(editorial);
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
