using CC.YiCraftCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Repository
{
    public interface ICommentInfoRepository
    {
        public List<CommentInfo> GetAllByNotID(int id);//通过传公告ID得到所有该评论
        public bool DelInfo(int id);//通过传入公告ID删除所有该评论
        public bool AddInfo(CommentInfo commentInfo);//通过对象添加
        public bool DelComInfo(int id);//删除单一评论
        public bool JudgeType1(string user, int NotID);//判断是否已经投了票
    }
}
