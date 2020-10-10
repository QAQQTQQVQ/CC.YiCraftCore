using CC.YiCraftCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CC.YiCraftCore.Repository
{
    public class UserInfoRepository:IUserInfoRepository
    {
        private YIContext YIContext;
        public UserInfoRepository(YIContext _YIContext)
        {
            YIContext = _YIContext;
        }

        public UserInfo GetByNameAsync(string Name)//通过传用户名得到该用户
        {
            return YIContext.UserInfo.FirstOrDefault(r => r.UserName == Name);
        }
        public bool AddUserInfo(UserInfo userInfo)//给一个用户，加入到数据库
        {
            YIContext.UserInfo.Add(userInfo);
            return YIContext.SaveChanges() > 0 ? true : false; 
        }
        public bool MailUserExist(string mail,string user)//判断是否存在邮箱或者用户
        {
            var pm = from u in YIContext.UserInfo
                     where u.Mail == mail||u.UserName==user
                     select u;

            if (pm.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetByNameUpLV(string Name)//通过传用户名得到该用户0级到1级
        {
            UserInfo userInfo = new UserInfo();
            userInfo= YIContext.UserInfo.FirstOrDefault(r => r.UserName == Name);
            if (userInfo.LV == 0)
            {
                userInfo.LV++;
                YIContext.Entry<UserInfo>(userInfo).State = EntityState.Modified;
                return YIContext.SaveChanges() > 0;
            }
            else
            {
                return false;
            }

        }
        public List<UserInfo> GetALL()//得到所有玩家数据
        {
            List<UserInfo> userInfos = new List<UserInfo>();
            foreach (UserInfo k in YIContext.UserInfo)
            {
                userInfos.Add(k);
            }
            return userInfos;
        }
        public async Task<bool> ByIdDel(string id)//通过id删除某条玩家
        {
            UserInfo userInfo = new UserInfo();
            userInfo.ID = Convert.ToInt32(id);
            YIContext.Remove(userInfo);
            return await YIContext.SaveChangesAsync()>0 ;
        }
    }
}
