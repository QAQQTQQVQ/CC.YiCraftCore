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
    public class LoginController : BaseController
    {
        private IUserInfoRepository _userInfoRepository;
        public  LoginController(IUserInfoRepository userInfoRepository)
        {
            IsCheck = false;
            _userInfoRepository = userInfoRepository;
        }

        public IActionResult Index()
        {
            string _user;
            if (HttpContext.Request.Cookies.TryGetValue("user", out _user))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(string user,string pwd,string mycode)
        {
            string Vcode=HttpContext.Session.GetString("Vcode");
            if (mycode.Trim() != Vcode)
            {
                return Content("<script>alert('验证码输入错误');window.location.href='/Login/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            UserInfo userInfo= _userInfoRepository.GetByNameAsync(user.Trim());
            if (userInfo == null)
            {
                return Content("<script>alert('用户不存在！');window.location.href='/Login/Index';</script>","text/html",System.Text.Encoding.UTF8);
            }
            else
            {
                if (userInfo.Pwd != pwd.Trim())
                {
                    return Content("<script>alert('密码输入错误');window.location.href='/Login/Index';</script>", "text/html", System.Text.Encoding.UTF8);
                }
                else
                {
                    HttpContext.Response.Cookies.Append("user", user);
                    HttpContext.Response.Cookies.Append("LV", userInfo.LV.ToString());

                    return RedirectToAction("Index", "Home");
                }
            
            }
        }
        public ActionResult ShowCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);
            HttpContext.Session.SetString("Vcode", strCode);
            byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);
            return File(imgBytes, @"image/jpeg");
        }
        public IActionResult MyInfo()
        {
            string user=HttpContext.Session.GetString("user");
            UserInfo userInfo = new UserInfo();
            userInfo= _userInfoRepository.GetByNameAsync(user);
            return View(userInfo);
        }
        public IActionResult ActionHome()
        {
            return View();
        }
    }
}
