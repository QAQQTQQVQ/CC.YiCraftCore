using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CC.YiCraftCore.Models;
using CC.YiCraftCore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CC.YiCraftCore.Controllers
{
    public class RegisterController : BaseController
    {
        private IUserInfoRepository _userInfoRepository;
        public RegisterController(IUserInfoRepository userInfoRepository)
        {
            IsCheck = false;
            _userInfoRepository = userInfoRepository;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string user,string pwd,string mailcode,string mymail)
        {
            string myMailCode = HttpContext.Session.GetString("MailCode");

            if (mailcode != myMailCode)
            {
                return Content("<script>alert('验证码错误！');window.location.href='/Login/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }

            bool temp = _userInfoRepository.MailUserExist(mymail, user);
            if (temp)
            {
                return Content("<script>alert('该用户ID或邮箱已被创建');window.location.href='/Login/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                HttpContext.Session.Clear();
                UserInfo userInfo1 = new UserInfo();
                userInfo1.CreateTime = DateTime.Now.ToString();
                userInfo1.UserName = user;
                userInfo1.LV = 0;
                userInfo1.Pwd = pwd;
                userInfo1.Mail = mymail;
                _userInfoRepository.AddUserInfo(userInfo1);
                return Content("<script>alert('恭喜你创建成功！由于您未满80岁，服务器自动将您纳为防沉迷对象！');window.location.href='/Login/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
        }
        public IActionResult Processing_mail(string mymail,string user)
        {
            if (!_userInfoRepository.MailUserExist(mymail, user))
            {
                string mailCode = YiMailHelper.GetRandomString();
                HttpContext.Session.SetString("MailCode", mailCode);
                YiMailHelper.Mail(mymail, mailCode);
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
         
        }
  
    }
}
