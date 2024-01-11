using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")] 
    //[EnableCors]
    [EnableCors("API")]
    [ApiController]
    public class TipoMedioController : ControllerBase
    {
        // GET: TipoMedioaController
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.TipoMedio.GetAllTipoMedio();

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
        public IActionResult Add([FromBody] ML.TipoMedio tipoMedio)
        {
            ML.Result result = BL.TipoMedio.AddTipoMedio(tipoMedio);

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
        public IActionResult Update([FromBody] ML.TipoMedio tipoMedio)
        {
            ML.Result result = BL.TipoMedio.UpdateTipoMedio(tipoMedio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("delete/{IdTipoMedio}")]
        public IActionResult Delete(int IdTipoMedio)
        {
            ML.Result result = BL.TipoMedio.DeleteTipoMedio(IdTipoMedio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getbyid/{IdTipoMedio}")]
        public IActionResult GetById(int IdTipoMedio)
        {
            ML.Result result = BL.TipoMedio.GetByIdTipoMedio(IdTipoMedio);

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
