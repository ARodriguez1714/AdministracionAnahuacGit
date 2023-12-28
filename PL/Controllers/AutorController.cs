using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Result result = BL.Autor.GetAllAutor();
            ML.Autor autor = new ML.Autor();
            if (result.Correct)
            {
                autor.Autores = result.Objects;
                return View(autor);
            }
            else
            {
                ViewBag.Error = result.Message;
                return View(autor);
            }
        }

        [HttpGet]
        public IActionResult Form(ML.Autor autor)
        {
            ML.Result resultTipoAutor = BL.TipoAutor.GetAllTipoAutor();
            autor.TipoAutor = new ML.TipoAutor();
            autor.TipoAutor.TipoAutores = resultTipoAutor.Objects;

            if (autor.IdAutor == 0)
            {
                //add
                return View(autor);
            }
            else
            {
                //Update
                ML.Result result = BL.Autor.GetByIdAutor(autor.IdAutor);
                if (result.Correct)
                {

                }
            }

            return View();
        }
        [HttpPost]
        public IActionResult Form(ML.Autor autor, IFormFile fuImagen)
        {
            ML.Result result = new ML.Result();

            //ML.Result resultTipoAutor = BL.TipoAutor.GetAllTipoAutor();
            //autor.TipoAutor = new ML.TipoAutor();
            //autor.TipoAutor.TipoAutores = resultTipoAutor.Objects;


            if (autor.IdAutor == 0)
            {
                //add
                autor.Foto = ConvertToBytes(fuImagen);
                result = BL.Autor.AddAutor(autor);
                if (result.Correct)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    ViewBag.Alert = "danger";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
            else
            {
                //Update
            }

            return View();
        }

        [HttpGet]
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
