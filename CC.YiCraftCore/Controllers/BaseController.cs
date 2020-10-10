using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CC.YiCraftCore.Controllers
{
    public class BaseController : Controller
    {
        public bool IsCheck =true;
        public override void OnActionExecuting(ActionExecutingContext fileterContext)
        {
            if (IsCheck)
            {
                //if (fileterContext.HttpContext.Session.GetString("user") == null)
                    string user;
                if (!fileterContext.HttpContext.Request.Cookies.TryGetValue("user", out user))
                {
                    fileterContext.Result = Redirect("/Login/Index");
                    return;
                }
              else
                {
                    string LV;
                    fileterContext.HttpContext.Request.Cookies.TryGetValue("LV", out LV);
                    fileterContext.HttpContext.Session.SetString("user", user);
                    fileterContext.HttpContext.Session.SetString("LV", LV);
                    return;
                }
            }
        }
    }
}
