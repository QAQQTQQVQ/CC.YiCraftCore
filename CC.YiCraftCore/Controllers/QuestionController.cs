using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CC.YiCraftCore.Models;
using CC.YiCraftCore.Repository;
using Microsoft.AspNetCore.Http;

namespace CC.YiCraftCore.Controllers
{
    public class QuestionController : BaseController
    {
        private IQuestionInfoRepository questionInfoRepository;
        private IUserInfoRepository userInfoRepository;
        public QuestionController(IQuestionInfoRepository _questionInfoRepository,IUserInfoRepository _userInfoRepository)
        {
            questionInfoRepository = _questionInfoRepository;
            userInfoRepository = _userInfoRepository;
        }
        public IActionResult Index()
        {
            Random sjs = new Random();
            List<QuestionInfo> questionInfos = new List<QuestionInfo>();
            QuestionInfo questionInfo = new QuestionInfo();
            questionInfos = questionInfoRepository.GetAll();
            for (int i = 0; i < questionInfos.Count; i++)
            {
                int j = sjs.Next(i, questionInfos.Count);
                questionInfo = questionInfos[i];
                questionInfos[i] = questionInfos[j];
                questionInfos[j] = questionInfo;
            }
            SessionExtensionsHelper.Set<List<QuestionInfo>>(HttpContext.Session, "question", questionInfos);
            HttpContext.Session.SetInt32("pagenumber", 0);
            return View();
        }
        public ActionResult Questionlist()
        {
            if (SessionExtensionsHelper.Get<List<QuestionInfo>>(HttpContext.Session, "question") == null)
            {
                Index();
            }
            return View();
        }
        [HttpPost]
        public ActionResult Questionlist(string btn)
        {
            btn= btn.Substring(0, 1);
            List<QuestionInfo> listquest = new List<QuestionInfo>();
            listquest = SessionExtensionsHelper.Get<List<QuestionInfo>>(HttpContext.Session, "question");
            int num = (int)HttpContext.Session.GetInt32("pagenumber");
            if (btn != listquest[num].answer)
            {
               return Content("<script>alert('回答错误，请重新审核');window.location.href='/Question/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                ++num;
                HttpContext.Session.SetInt32("pagenumber", num);
                if (num > 9)
                {
                   bool temp= userInfoRepository.GetByNameUpLV(HttpContext.Session.GetString("user"));
                    if (temp)
                    {
                        HttpContext.Session.SetString("LV", "1");
                        return Content("<script>alert('恭喜完成白名单审核!你的等级提高至1级！');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8); }
                    else
                    {
                        return Content("<script>alert('感谢你又来复习一遍！');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
                    }

             

                }
                return RedirectToAction("Questionlist");
            }
        }
    }
}
