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

            medio.Idioma = new ML.Idioma();
            ML.Result resultIdioma = BL.Idioma.GetAllIdioma();

            medio.TipoMedio = new ML.TipoMedio();
            ML.Result resultTipoMedio = BL.TipoMedio.GetAllTipoMedio();

            medio.Editorial = new ML.Editorial();
            ML.Result resultEditorial = BL.Editorial.GetAllEditorial();

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
        public IActionResult GetAllMedio()
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


        

        [HttpGet]
        public IActionResult Form(int? IdMedio)
        {
            ML.Medio medio = new ML.Medio();
            medio.Autor = new ML.Autor();

            ML.Result resultAutor = BL.Autor.GetAllAutor();
            medio.Autor = new ML.Autor();
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

            if (IdMedio == 0)
            {
                ViewBag.Accion = "Agregar"; //return View(medio);
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
            

            //if (ModelState.IsValid)
            //{
            ML.Result result = new ML.Result();

            if (medio.IdMedio == 0)
            {
                //add
                medio.Imagen = ConvertToBytes(fuImagen);
                result = BL.Medio.AddMedio(medio);

                if (result.Correct)
                {
                   // ViewBag.Mensaje = result.Message;
                    return RedirectToAction("GetAll");
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
                    return RedirectToAction("GetAll");
                }
                else
                {
                    ViewBag.Mensaje = result.Message;
                    return View("Modal");
                }

            }

            //}
            //else
            //{

            //    ML.Result resultAutor = BL.Autor.GetAllAutor();//DDL INDEPENIENTES

            //    medio.Autor.Autores = resultAutor.Objects;
            //    return View(medio);
            //}
    }
    [HttpGet]
        public IActionResult GetByIdMedio(int idMedio)
        {
            ML.Result result = BL.Medio.GetByIdMedio(idMedio);
            if (result.Correct)
            {
                ML.Medio medio = new ML.Medio();


                medio = (ML.Medio)result.Object;

                return Ok(medio);
            }
            else
            {
                return BadRequest(result);
            }
        }

    [HttpGet]
    public JsonResult Delete(int idMedio)
        {
            ML.Result result = BL.Medio.DeleteMedio(idMedio);
            return Json(result);
        }

    [HttpPost]
        public JsonResult CambiarStatus(int idMedio, bool disponibilidad)
        {
            ML.Result result = BL.Medio.ChangeStatus(idMedio, disponibilidad);

            return Json(result);
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
