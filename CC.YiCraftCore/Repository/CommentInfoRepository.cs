using CC.YiCraftCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Repository
{
    public class CommentInfoRepository:ICommentInfoRepository
    {
        private YIContext yiContext;
        public CommentInfoRepository(YIContext _yiContext)
        {
            yiContext = _yiContext;
        }
        public List<CommentInfo> GetAllByNotID(int id)//通过传公告ID得到所有该评论
        {
            List<CommentInfo> commentInfos = new List<CommentInfo>();
            var data = from u in yiContext.CommentInfo
                       where u.NotID == id
                       select u;
            foreach (CommentInfo k in data)
            {
                commentInfos.Add(k);
            }
            return commentInfos;
        }
        public bool DelInfo(int id)//通过id删除
        {
            List<CommentInfo> commentInfos = new List<CommentInfo>();
            commentInfos = GetAllByNotID(id);
            if (commentInfos.Count==0)
            {
                return true;
            }
            yiContext.CommentInfo.RemoveRange(commentInfos);
            return yiContext.SaveChanges() >0;
        }
        public bool AddInfo(CommentInfo commentInfo)//通过对象添加
        {
            yiContext.CommentInfo.Add(commentInfo);
            return yiContext.SaveChanges() > 0;
        }

        public bool DelComInfo(int id)//删单一个评论
        {
            CommentInfo commentInfo = new CommentInfo();
            commentInfo.ID = id;
            yiContext.Entry<CommentInfo>(commentInfo).State = EntityState.Deleted;
            return yiContext.SaveChanges() > 0;
        }
        public bool JudgeType1(string user,int NotID)//判断是否已经投了票
        {
            var Com = from u in yiContext.CommentInfo
                      where u.NotID == NotID && u.Uname == user
                      select u;
            if (Com.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
