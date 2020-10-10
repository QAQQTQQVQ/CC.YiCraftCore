using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CC.YiCraftCore.Models;
using CC.YiCraftCore.Repository;

namespace CC.YiCraftCore.Controllers
{
    public class SearchController : BaseController
    {
        private IUserInfoRepository userInfoRepository;
        private IJurisdictionHelper jurisdictionHelper;
        public SearchController(IUserInfoRepository _userInfoRepository,IJurisdictionHelper _jurisdictionHelper)
        {
            userInfoRepository = _userInfoRepository;
            jurisdictionHelper = _jurisdictionHelper;
        }
        // GET: SearchController
        [HttpPost]
        public ActionResult Index(string Uid)
        {
            List<UserInfo> userInfos = new List<UserInfo>();
            UserInfo userInfo = new UserInfo();
            userInfo= userInfoRepository.GetByNameAsync(Uid);
            if (userInfo == null)
            {
                userInfos = userInfoRepository.GetALL();
            }
            else { userInfos.Add(userInfo); }
         
            return View(userInfos);
        }

   
        // GET: SearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchController/Delete/5
        public async Task<ActionResult>  Delete(string id)
        {
            if (!jurisdictionHelper.Judge("User"))
            {
                return Content(jurisdictionHelper.GetStringUrl("/Home/Index"), "text /html", System.Text.Encoding.UTF8);
            }
            bool temp = await userInfoRepository.ByIdDel(id);
            if (temp)
            {
                return Content("<script>alert('删除成功');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("<script>alert('删除失败');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
         
        }

        // POST: SearchController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
