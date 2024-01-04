using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using ML;
using System.ComponentModel.DataAnnotations;

namespace PL.Controllers
{
    public class RolController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public RolController(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            ML.Result result = BL.UserIdentity.GetAll();
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
        public IActionResult GetAllUsuarios()
        {
            var result = roleManager.Roles.ToList();
            return Ok(result);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Form([Required] Microsoft.AspNetCore.Identity.IdentityRole rol)
        {

            if (rol.Id == null)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(rol.Name));
                if (result.Succeeded)
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
                IdentityRole role = new IdentityRole();
                role.Id = await roleManager.GetRoleIdAsync(rol);
                role.Name = await roleManager.GetRoleNameAsync(rol);

                IdentityResult result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
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
        public JsonResult GetByIdRol(Guid IdRole)
        {
            ML.Result result = BL.Rol.GetByIdRol(IdRole);
            if (result.Correct)
            {
                return Json(result);
            }

            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(Guid idRole)
        {

            IdentityRole role = await roleManager.FindByIdAsync(idRole.ToString());

            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return Json(result);
                }
                else
                {
                    return Json(result);
                }
            }

            return Json(role);

        }

        [HttpGet]
        public JsonResult Asignar(Guid idRole)
        {
            ML.Result result = BL.UserIdentity.GetAll();
            if (result.Correct)
            {
                ML.Result resultInfo = BL.Rol.GetByIdRol(idRole);
                ML.UserIdentity user = new ML.UserIdentity();
                user.Rol = new ML.Rol();
                user.IdentityUsers = result.Objects;
                user.Rol = (ML.Rol)resultInfo.Object;

                return Json(user);
            }
            return Json(result);
        }
    }
}
