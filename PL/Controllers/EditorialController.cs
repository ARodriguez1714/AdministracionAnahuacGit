using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EditorialController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Editorial editorial = new ML.Editorial();

            ML.Result result = BL.Editorial.GetAllEditorial();

            if (result.Correct)
            {
                editorial.Editoriales = result.Objects;
                return View(editorial);
            }
            else
            {
                ViewBag.Alert = "danger";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
        }
    }
}
