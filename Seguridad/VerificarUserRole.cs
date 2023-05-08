using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;


namespace pruebaUsuario.Seguridad
{
    public class VerificarUserRole: ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext context)
        {
           
            var url = context.HttpContext.Request.Path;

           

                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                

                    if (context.HttpContext.User.IsInRole("Admin"))
                     {


                         if (context.HttpContext.Request.Path == "/Identity/Account/Login" )
                         {

                            context.HttpContext.Response.Redirect("/Controllers/admin");
               
                         }

                    }

                }

                base.OnActionExecuted(context);
            }
    }
}
