using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pruebaUsuario.Controllers
{

    public class AccessController : Controller
    {
        
        public AccessController() 
        {
            
            
        }
        public IActionResult Index()
        {


            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                 return   Redirect("Identity/Account/Login");
            }


            if (User.IsInRole("Admin"))
            {
               return   Redirect("/Admin");
            }
            else
            {
               return Redirect("/Home");
            }




        }
    }
}
