using CC.YiCraftCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Repository
{
     public interface IUserInfoRepository
    {
        UserInfo GetByNameAsync(string Name);
        bool AddUserInfo(UserInfo userInfo);
        bool MailUserExist(string mail, string user);
        public bool GetByNameUpLV(string Name);
        public List<UserInfo> GetALL();
        public Task<bool> ByIdDel(string id);//通过id删除某条玩家
    }
}
