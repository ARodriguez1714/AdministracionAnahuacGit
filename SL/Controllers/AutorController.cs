using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AutorController : ControllerBase
    {
        // GET: api/<Autor>
        [Authorize]
        [HttpGet("getautores")]
        public IActionResult GetAutores()
        {
            ML.Result result = BL.Autor.GetAllAutor();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // GET api/<Autor>/5
        [HttpGet("getbyid/{idAutor}")]
        public IActionResult GetById(byte idAutor)
        {

            ML.Result result = BL.Autor.GetByIdAutor(idAutor);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [HttpPost("add")]
        public IActionResult Add([FromBody] ML.Autor autor)
        {
            ML.Result result = BL.Autor.AddAutor(autor);

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
        public IActionResult Update([FromBody] ML.Autor autor)
        {
            ML.Result result = BL.Autor.UpdateAutor(autor);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("convertimagen")]
        public IActionResult Convert(IFormFile fuImagen)
        {
            ML.Autor autor = new ML.Autor();
            autor.Foto = ConvertToBytes(fuImagen);
            if (autor.Foto != null)
            {
                return Ok(autor.Foto);
            }
            else
            {
                return BadRequest();
            }
        }

        [NonAction]
        public byte[] ConvertToBytes(IFormFile fuImagen)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fuImagen.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
