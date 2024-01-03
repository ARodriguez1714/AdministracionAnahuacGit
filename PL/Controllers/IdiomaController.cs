using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class IdiomaController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Idioma idioma = new ML.Idioma();
            ML.Result result = BL.Idioma.GetAllIdioma();

            idioma.Idiomas = new List<object>();

            if (result.Correct)
            {
                idioma.Idiomas = result.Objects.ToList();
                return View(idioma);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(idioma);
            }

        }

        [HttpGet]
        public ActionResult Form(int idIdioma)
        {
            ML.Idioma idioma = new ML.Idioma();

            if (idIdioma == 0) //null
            {
               // ViewBag.Accion = "Agregar";
                return View(idioma);
            }
            else
            {
                ML.Result result = BL.Idioma.GetByIdIdioma(idIdioma);

                if (result.Correct)
                {
                    idioma = (ML.Idioma)result.Object;
                    return View(idioma);

                }
                ViewBag.Accion = "Actualizar";

            }

            return View(idioma);


        }


        [HttpPost]
        public ActionResult Form(ML.Idioma idioma)
        {
            ML.Result result = new ML.Result();

            if (idioma.IdIdioma == null)
            {
                //add 
                result = BL.Idioma.AddIdioma(idioma);

                if (result.Correct)
                {
                    ViewBag.Mensaje = result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = result.Message;
                    return View("Modal");
                }

            }
            else
            {
                result = BL.Idioma.UpdateIdioma(idioma);

                if (result.Correct)
                {
                    ViewBag.Mensaje = result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = result.Message;
                    return View("Modal");
                }

            }
            //return View(idioma);


        }


        [HttpGet]
        public ActionResult Delete(int idIdioma)
        {
            //ML.Idioma idioma = new ML.Idioma();
            ML.Result result = BL.Idioma.DeleteIdioma(idIdioma);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "No se ha eliminado correctamente" + result.Message;
            }
            return PartialView("Modal");

        }
    }
}
