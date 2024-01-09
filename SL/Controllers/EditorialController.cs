using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class EditorialController : ControllerBase
    {
        // GET: api/<EditorialController>
        [HttpGet("getalleditorial")]
        public IActionResult GetEditoriales()
        {
            ML.Result result = BL.Editorial.GetAllEditorial();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // GET api/<EditorialController>/5
        [HttpGet("getbyideditorial/{idEditorial}")]
        public IActionResult GetByIdEditorial(byte idEditorial)
        {
            ML.Result result = BL.Editorial.GetByIdEditorial(idEditorial);
            if (result.Correct)
            {
                ML.Editorial editorial = new ML.Editorial();
                editorial.Direccion = new ML.Direccion();
                editorial.Direccion.Colonia = new ML.Colonia();
                editorial.Direccion.Colonia.Municipio = new ML.Municipio();
                editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();


                editorial = (ML.Editorial)result.Object;

                ML.Result resultPaises = BL.Pais.GetAll();
                ML.Result resultEstados = BL.Estado.GetByIdPais(editorial.Direccion.Colonia.Municipio.Estado.Pais.IdPais.Value);
                ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(editorial.Direccion.Colonia.Municipio.Estado.IdEstado.Value);
                ML.Result resultColonias = BL.Colonia.GetByIdMunicipio(editorial.Direccion.Colonia.Municipio.IdMunicipio.Value);

                editorial.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                editorial.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                editorial.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                editorial.Direccion.Colonia.Colonias = resultColonias.Objects;
                return Ok(editorial);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("guardar")]
        public IActionResult Guardar([FromBody] ML.Editorial editorial)
        {
            ML.Result result = new ML.Result();
            if (editorial.IdEditorial == null)
            {
                result = BL.Editorial.AddEditorial(editorial);

                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                result = BL.Editorial.UpdateEditorial(editorial);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
        }

        // POST api/<EditorialController>
        [HttpPost("add")]
        public IActionResult Add([FromBody] ML.Editorial editorial)
        {
            ML.Result result = BL.Editorial.AddEditorial(editorial);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/<EditorialController>/5
        [HttpPut("update")]
        public IActionResult Update([FromBody] ML.Editorial editorial)
        {
            ML.Result result = BL.Editorial.UpdateEditorial(editorial);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // DELETE api/<EditorialController>/5
        [HttpDelete("delete/{idEditorial}")]
        public IActionResult Delete(byte idEditorial)
        {
            ML.Result result = BL.Editorial.DeleteEditorial(idEditorial);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
