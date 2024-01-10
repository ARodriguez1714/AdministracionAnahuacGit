using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors]
    [EnableCors("API")]
    [ApiController]
    public class MedioController : ControllerBase
    {
        // GET: MedioController
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Medio.GetAllMedio();

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
        public IActionResult Add([FromBody] ML.Medio medio)
        {
            ML.Result result = BL.Medio.AddMedio(medio);

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
        public IActionResult Update([FromBody] ML.Medio medio)
        {
            ML.Result result = BL.Medio.UpdateMedio(medio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("delete/{IdMedio}")]
        public IActionResult Delete(int IdMedio)
        {
            ML.Result result = BL.Medio.DeleteMedio(IdMedio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getbyid/{IdMedio}")]
        public IActionResult GetById(int IdMedio)
        {
            ML.Result result = BL.Medio.GetByIdMedio(IdMedio);

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
