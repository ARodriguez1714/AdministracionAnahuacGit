using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text;

namespace PL.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AutorController : Controller
    {
        public IActionResult GetAll()
        {
            return View();

        }

        //[HttpGet]
        //public  IActionResult GetAutores()
        //{
        //    ML.Result result = BL.Autor.GetAllAutor();
        //    if (result.Correct)
        //    {
        //        //autor.Autores = result.Objects;
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }
        //}
        [HttpGet]
        public  IActionResult GetAllTipoAutor()
        {
            ML.Result result = BL.TipoAutor.GetAllTipoAutor();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        //[HttpGet]
        //public JsonResult GetById(byte idAutor)
        //{

        //    ML.Result result = BL.Autor.GetByIdAutor(idAutor);
        //    if (result.Correct)
        //    {
        //        return Json(result);
        //    }

        //    return Json(result);

        //}
        //[HttpGet]
        //public IActionResult Form(byte idAutor)
        //{
        //    ML.Autor autor = new ML.Autor();
        //    ML.Result resultTipoAutor = BL.TipoAutor.GetAllTipoAutor();
        //    autor.TipoAutor = new ML.TipoAutor();
        //    autor.TipoAutor.TipoAutores = resultTipoAutor.Objects;

        //    if (idAutor == 0)
        //    {
        //        //add
        //        return View(autor);
        //    }
        //    else
        //    {
        //        //Update
        //        ML.Result result = BL.Autor.GetByIdAutor(idAutor);
        //        if (result.Correct)
        //        {
        //            autor = (ML.Autor)result.Object;
        //            autor.TipoAutor.TipoAutores = resultTipoAutor.Objects;
        //            return View(autor);
        //        }
        //        else
        //        {
        //            ViewBag.Alert = "danger";
        //            ViewBag.Message = result.Message;
        //            return View("Modal");
        //        }
        //    }
        //}
        [HttpPost]
        public IActionResult Form(ML.Autor autor, IFormFile fuImagen)
        {
            ML.Result result = new ML.Result();

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
                autor.Foto = ConvertToBytes(fuImagen);
                result = BL.Autor.UpdateAutor(autor);
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
        //public Image ConvertToImage(byte[] imageBytes)
        //{
        //    using (var ms = new MemoryStream(imageBytes))
        //    {
        //        return Image.FromStream(ms);
        //    }
        //}
    }
}
