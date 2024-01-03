using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MedioController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Medio medio = new ML.Medio();
            ML.Result result = BL.Medio.GetAllMedio();
            medio.Autor = new ML.Autor();
            ML.Result resultAutor = BL.Autor.GetAllAutor();

            medio.Medios = new List<object>();

            if (result.Correct)
            {
                medio.Medios = result.Objects.ToList();
                return View(medio);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(medio);
            }

        }




        [HttpGet]
        public IActionResult Form(int? IdMedio)
        {
            ML.Medio medio = new ML.Medio();
            medio.Autor = new ML.Autor();

            ML.Result resultAutor = BL.Autor.GetAllAutor();
            medio.Autor.Autores = resultAutor.Objects;

            ML.Result resultTipoAutor = BL.TipoAutor.GetAllTipoAutor();
            medio.Autor.TipoAutor = new ML.TipoAutor();
            medio.Autor.TipoAutor.TipoAutores = resultTipoAutor.Objects;

            ML.Result resultIdioma = BL.Idioma.GetAllIdioma();
            medio.Idioma = new ML.Idioma();
            medio.Idioma.Idiomas = resultIdioma.Objects.ToList();

            ML.Result resultTipoMedio = BL.TipoMedio.GetAllTipoMedio();
            medio.TipoMedio = new ML.TipoMedio();
            medio.TipoMedio.TipoMedios = resultTipoMedio.Objects;

            ML.Result resultEditorial = BL.Editorial.GetAllEditorial();
            medio.Editorial = new ML.Editorial();
            medio.Editorial.Editoriales = resultEditorial.Objects;

            if (IdMedio == null)
            {
                ViewBag.Accion = "Agregar";
            }
            else
            {
                ML.Result result = BL.Medio.GetByIdMedio(IdMedio.Value);


                if (result.Correct)
                {
                    medio.IdMedio = ((ML.Medio)result.Object).IdMedio;
                    medio.Nombre = ((ML.Medio)result.Object).Nombre;
                    medio.Archivo = ((ML.Medio)result.Object).Archivo;
                    medio.Descripcion = ((ML.Medio)result.Object).Descripcion;
                    medio.Disponibilidad = ((ML.Medio)result.Object).Disponibilidad;
                    medio.Imagen = ((ML.Medio)result.Object).Imagen;

                    medio.Autor.Autores = resultAutor.Objects;
                    medio.Autor.IdAutor = ((ML.Medio)result.Object).Autor.IdAutor;
                    medio.Autor.Nombre = ((ML.Medio)result.Object).Autor.Nombre;

                    //medio.Autor.TipoAutor.TipoAutors = resultTipoAutor.Objects;
                    //medio.Autor.TipoAutor.IdTipoAutor = ((ML.Medio)result.Object).Autor.TipoAutor.IdTipoAutor;
                    //medio.Autor.TipoAutor.Nombre = ((ML.Medio)result.Object).Autor.TipoAutor.Nombre;

                    medio.Idioma.Idiomas = resultIdioma.Objects;
                    medio.Idioma.IdIdioma = ((ML.Medio)result.Object).Idioma.IdIdioma;
                    medio.Idioma.Nombre = ((ML.Medio)result.Object).Idioma.Nombre;

                    medio.TipoMedio.TipoMedios = resultTipoMedio.Objects;
                    medio.TipoMedio.IdTipoMedio = ((ML.Medio)result.Object).TipoMedio.IdTipoMedio;
                    medio.TipoMedio.Nombre = ((ML.Medio)result.Object).TipoMedio.Nombre;

                    medio.Editorial.Editoriales = resultEditorial.Objects;
                    medio.Editorial.IdEditorial = ((ML.Medio)result.Object).Editorial.IdEditorial;
                    medio.Editorial.Nombre = ((ML.Medio)result.Object).Editorial.Nombre;


                }
                ViewBag.Accion = "Actualizar";
        }
         return View(medio);
    }

    [HttpPost]
    public IActionResult Form(ML.Medio medio, IFormFile fuImagen)
    {
        if (ModelState.IsValid)
        {

                //if (fuImagen.ContentLength > 0)
                //{
                //    medio.Imagen = ConvertToBytes(fuImagen);

                //    string imagenBase64 = Convert.ToBase64String(medio.Imagen);
                //}

                ML.Result result = new ML.Result();

            if (medio.IdMedio == 0)
            {
                //add
                medio.Imagen = ConvertToBytes(fuImagen);
                result = BL.Medio.AddMedio(medio);

                if (result.Correct)
                {
                   // ViewBag.Mensaje = result.Message;
                    return RedirectToAction("Modal");
                }
                else
                {
                    ViewBag.Mensaje = result.Message;
                    return View("Modal");
                }
            }
            else
            {
                    //update
                    medio.Imagen = ConvertToBytes(fuImagen);
                    result = BL.Medio.UpdateMedio(medio);

                if (result.Correct)
                {
                    ViewBag.Mensaje = result.Message;
                    return RedirectToAction("Modal");
                }
                else
                {
                    ViewBag.Mensaje = result.Message;
                    return View("Modal");
                }

            }

        }
        else
        {

            ML.Result resultAutor = BL.Autor.GetAllAutor();//DDL INDEPENIENTES

            medio.Autor.Autores = resultAutor.Objects;
            return View(medio);
        }
    }

    [HttpGet]
    public ActionResult Delete(int IdMedio)
    {
       // ML.Medio medio = new ML.Medio();

        ML.Result result = BL.Medio.DeleteMedio(IdMedio);

        if (result.Correct)
        {
            ViewBag.Mensaje = "Se ha eliminado correctamente";
        }
        else
        {
            ViewBag.Mensaje = "No se ha eliminado correctamente " + result.Message;
        }

        return PartialView("Modal");
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
