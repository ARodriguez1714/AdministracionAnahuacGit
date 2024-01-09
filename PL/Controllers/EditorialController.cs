using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{

    [Authorize(Roles = "Administrador")]
    public class EditorialController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Editorial editorial = new ML.Editorial();

            ML.Result result = BL.Editorial.GetAllEditorial();

            ML.Result resultPaises = BL.Pais.GetAll();
            editorial.Direccion = new ML.Direccion();
            editorial.Direccion.Colonia = new ML.Colonia();
            editorial.Direccion.Colonia.Municipio = new ML.Municipio();
            editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            editorial.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

            if (result.Correct)
            {
                editorial.Editoriales = result.Objects;
                ViewBag.Message = result.Message;
                return View(editorial);
            }
            else
            {
                ViewBag.Alert = "danger";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
        }

        //[HttpGet]
        //public IActionResult GetAllEditorial()
        //{
        //    ML.Result result = BL.Editorial.GetAllEditorial();
        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }
        //}


        //[HttpGet]
        //public IActionResult GetByIdEditorial(byte idEditorial)
        //{
        //    ML.Result result = BL.Editorial.GetByIdEditorial(idEditorial);
        //    if (result.Correct)
        //    {
        //        ML.Editorial editorial = new ML.Editorial();
        //        editorial.Direccion = new ML.Direccion();
        //        editorial.Direccion.Colonia = new ML.Colonia();
        //        editorial.Direccion.Colonia.Municipio = new ML.Municipio();
        //        editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
        //        editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();


        //        editorial = (ML.Editorial)result.Object;

        //        ML.Result resultPaises = BL.Pais.GetAll();
        //        ML.Result resultEstados = BL.Estado.GetByIdPais(editorial.Direccion.Colonia.Municipio.Estado.Pais.IdPais.Value);
        //        ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(editorial.Direccion.Colonia.Municipio.Estado.IdEstado.Value);
        //        ML.Result resultColonias = BL.Colonia.GetByIdMunicipio(editorial.Direccion.Colonia.Municipio.IdMunicipio.Value);

        //        editorial.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
        //        editorial.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
        //        editorial.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
        //        editorial.Direccion.Colonia.Colonias = resultColonias.Objects;
        //        return Ok(editorial);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }
        //}

        [HttpPost]
        public JsonResult GetEstado(byte idPais)
        {
            var result = BL.Estado.GetByIdPais(idPais);

            return Json(result.Objects);
        }

        [HttpPost]
        public JsonResult GetMunicipio(byte idEstado)
        {
            var result = BL.Municipio.GetByIdEstado(idEstado);

            return Json(result.Objects);
        }

        [HttpPost]
        public JsonResult GetColonia(byte idMunicipio)
        {
            var result = BL.Colonia.GetByIdMunicipio(idMunicipio);

            return Json(result.Objects);
        }

        [HttpPost]
        public JsonResult Form(ML.Editorial editorial)
        {
            ML.Result result = new ML.Result();
            if (editorial.IdEditorial == null)
            {
                result = BL.Editorial.AddEditorial(editorial);

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
                result = BL.Editorial.UpdateEditorial(editorial);
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
        //public JsonResult Delete(byte idEditorial)
        //{
        //    ML.Result result = BL.Editorial.DeleteEditorial(idEditorial);
        //    return Json(result);
        //}
    }
}
