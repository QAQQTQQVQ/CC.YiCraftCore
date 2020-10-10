using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Repository
{
    public class JurisdictionHelper:IJurisdictionHelper
    {
        IHttpContextAccessor httpContextAccessor;
        public JurisdictionHelper(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }

        public bool Judge(string jurisdiction)
        {
            int LV = Convert.ToInt32(httpContextAccessor.HttpContext.Session.GetString("LV"));
            switch (jurisdiction)
            { 
                case "Not":
                    if (LV >= 9)
                        return true;
                    break;
                case "Com":
                    if (LV >= 9)
                        return true;
                    break;
                case "User":
                    if (LV >= 9)
                        return true;
                    break;
            }
            return false;
        }
        public string GetStringUrl(string url,string alert="权限不足！")
        { 
               return "<script>alert('"+ alert + "');window.location.href='"+ url + "';</script>";
        }
    }
}
