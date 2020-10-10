using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Repository
{
    public interface IJurisdictionHelper
    {
        public bool Judge(string jurisdiction);
        public string GetStringUrl(string url, string alert = "权限不足！");
    }
}
