using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CC.YiCraftCore.Models;
using CC.YiCraftCore.Repository;
using Microsoft.AspNetCore.Http;

namespace CC.YiCraftCore.Controllers
{
    public class HomeController : BaseController
    {
        private INoticeInfoRepository _noticeInfoRepository;
        private ICommentInfoRepository _commentInfoRepository;
        private IJurisdictionHelper _jurisdictionHelper;
        public HomeController(INoticeInfoRepository noticeInfoRepository,ICommentInfoRepository commentInfoRepository,IJurisdictionHelper jurisdictionHelper)
        {
            _noticeInfoRepository = noticeInfoRepository;
            _commentInfoRepository = commentInfoRepository;
            _jurisdictionHelper = jurisdictionHelper;
        }

        public IActionResult Index()
        {
            List<NoticeInfo> noticeInfos = new List<NoticeInfo>();
            noticeInfos =_noticeInfoRepository.GetAll();
            return View(noticeInfos);
        }
        public IActionResult Details(string id)
        {
            NoticeInfo noticeInfo = new NoticeInfo();
            noticeInfo= _noticeInfoRepository.GetByID(Convert.ToInt32(id));
            List<CommentInfo> commentInfos = new List<CommentInfo>();
            commentInfos = _commentInfoRepository.GetAllByNotID(Convert.ToInt32(id));
            ViewData["Com"] = commentInfos;
            return View(noticeInfo);
        }
        public IActionResult update(string id)
        {
            if (!_jurisdictionHelper.Judge("Not"))
            {
                return Content(_jurisdictionHelper.GetStringUrl("/Home/Index"), "text/html", System.Text.Encoding.UTF8);
            }
            NoticeInfo noticeInfo = new NoticeInfo();
            noticeInfo = _noticeInfoRepository.GetByID(Convert.ToInt32(id));
            return View(noticeInfo);
        }
        [HttpPost]
        public IActionResult update(NoticeInfo noticeInfo)
        {

            bool temp = _noticeInfoRepository.UpdateInfo(noticeInfo);
            if (temp)
            {

                return Content("<script>alert('修改成功！');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("<script>alert('修改失败！');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
           
        }
        public IActionResult del(string id)
        {
            if (!_jurisdictionHelper.Judge("Not"))
            {
                return Content(_jurisdictionHelper.GetStringUrl("/Home/Index"), "text/html", System.Text.Encoding.UTF8);
            }
        
            bool temp= _noticeInfoRepository.DelInfo(Convert.ToInt32(id));
            bool temp1 = _commentInfoRepository.DelInfo(Convert.ToInt32(id));
            if (temp&&temp1)
            {
                return Content("<script>alert('删除成功！');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("<script>alert('删除失败！');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
        }
        public IActionResult add()
        {
            if (!_jurisdictionHelper.Judge("Not"))
            {
                return Content(_jurisdictionHelper.GetStringUrl("/Home/Index"), "text/html", System.Text.Encoding.UTF8);
            }
            return View();
        }
        [HttpPost]
        public IActionResult add(NoticeInfo noticeInfo)
        {

            noticeInfo.SubTime = DateTime.Now.ToString();
            noticeInfo.Uname = HttpContext.Session.GetString("user");

            bool temp= _noticeInfoRepository.AddInfo(noticeInfo);
            if (temp)
            {
                return Content("<script>alert('添加成功！');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("<script>alert('添加失败！');window.location.href='/Home/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
        }
        public IActionResult AddCom(string id)
        {
            ViewBag.NotID = id;
            return View();
        }
        public IActionResult AddCom2(string id)
        {
            string user = HttpContext.Session.GetString("user");
            if (_commentInfoRepository.JudgeType1(user,Convert.ToInt32(id)))
            {
                return Content("<script>alert('你已经投过票了！');window.location.href='/Home/Details/" + Convert.ToInt32(id) + "';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            CommentInfo commentInfo = new CommentInfo();
            commentInfo.Info = "1";
            commentInfo.NotID =Convert.ToInt32(id);
            return AddCom(commentInfo);
        }
        [HttpPost]
        public IActionResult AddCom(CommentInfo commentInfo)
        {
         
            commentInfo.ID = 0;
            commentInfo.Uname = HttpContext.Session.GetString("user");
            commentInfo.SubTime = DateTime.Now.ToString();
           string sid = commentInfo.NotID.ToString(); 
           bool temp= _commentInfoRepository.AddInfo(commentInfo);
            if (temp)
            {
                return Content("<script>alert('添加成功！');window.location.href='/Home/Details/" + sid + "';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("<script>alert('添加失败！');window.location.href='/Home/Details/" + sid + "';</script>", "text/html", System.Text.Encoding.UTF8);
            }
          
        }
        public IActionResult DelCom(string NOTID,string ID)
        {
            if (!_jurisdictionHelper.Judge("Not"))
            {
                return Content(_jurisdictionHelper.GetStringUrl("/Home/Details/" + NOTID), "text /html", System.Text.Encoding.UTF8);
            }
            bool temp= _commentInfoRepository.DelComInfo(Convert.ToInt32(ID));
            if (temp)
            {
                return Content("<script>alert('删除成功！');window.location.href='/Home/Details/" + NOTID + "';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("<script>alert('删除失败！');window.location.href='/Home/Details/" + NOTID + "';</script>", "text/html", System.Text.Encoding.UTF8);
            }
        }
        public IActionResult Out()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("user");
            HttpContext.Response.Cookies.Delete("LV");
            return Content("<script>alert('当前用户已退出！');window.location.href='/Login/Index';</script>", "text/html", System.Text.Encoding.UTF8);
        }
        public IActionResult Download()
        {
            return View();
        }
    }
}
