using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class tipoMedioController : Controller
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.TipoMedio tipoMedio = new ML.TipoMedio();
            ML.Result result = BL.TipoMedio.GetAllTipoMedio();

            tipoMedio.TipoMedios = new List<object>();

            if (result.Correct)
            {
                tipoMedio.TipoMedios = result.Objects;
                return View(tipoMedio);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(tipoMedio);
            }

        }

        [HttpGet]
        public IActionResult GetAllTipoMedio()
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

        [HttpPost]
        public JsonResult Form(ML.TipoMedio tipoMedio)
        {
            ML.Result result = new ML.Result();
            if (tipoMedio.IdTipoMedio == null)
            {
                result = BL.TipoMedio.AddTipoMedio(tipoMedio);

                if (result.Correct)
                {
                    return Json(result);
                }
                else
                {
                    return Json(result);
                }
            }
            else
            {
                result = BL.TipoMedio.UpdateTipoMedio(tipoMedio);
                if (result.Correct)
                {
                    return Json(result);
                }
                else
                {
                    return Json(result);
                }
            }
        }

        [HttpGet]
        public IActionResult GetByIdTipoMedio(int idTipoMedio)
        {
            ML.Result result = BL.TipoMedio.GetByIdTipoMedio(idTipoMedio);
            if (result.Correct)
            {
                ML.TipoMedio tipoMedio = new ML.TipoMedio();


                tipoMedio = (ML.TipoMedio)result.Object;

                return Ok(tipoMedio);
            }
            else
            {
                return BadRequest(result);
            }
        }

        public JsonResult Delete(int idTipoMedio)
        {
            ML.Result result = BL.TipoMedio.DeleteTipoMedio(idTipoMedio);
            return Json(result);
        }

    }
}

