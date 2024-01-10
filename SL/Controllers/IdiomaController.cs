using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors]
    [EnableCors("API")]
    [ApiController]
    public class IdiomaController : ControllerBase
    {
        // GET: IdiomaController
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Idioma.GetAllIdioma();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [EnableCors("API")]
        [HttpPost("add")]
        public IActionResult Add([FromBody] ML.Idioma idioma)
        {
            ML.Result result = BL.Idioma.AddIdioma(idioma);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] ML.Idioma idioma)
        {
            ML.Result result = BL.Idioma.UpdateIdioma(idioma);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("delete/{IdIdioma}")]
        public IActionResult Delete(int IdIdioma)
        {
            ML.Result result = BL.Idioma.DeleteIdioma(IdIdioma);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getbyid/{IdIdioma}")]
        public IActionResult GetById(int IdIdioma)
        {
            ML.Result result = BL.Idioma.GetByIdIdioma(IdIdioma);

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
