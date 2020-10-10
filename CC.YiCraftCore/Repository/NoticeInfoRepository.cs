using CC.YiCraftCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace CC.YiCraftCore.Repository
{
    public class NoticeInfoRepository:INoticeInfoRepository
    {
        private YIContext yiContext;
        public NoticeInfoRepository(YIContext _yiContext)
        {
            yiContext = _yiContext;
        }
        public List<NoticeInfo> GetAll()
        {
            List<NoticeInfo> data = new List<NoticeInfo>();
            foreach (NoticeInfo u in yiContext.NoticeInfo)
            {
                data.Add(u);
            }
            return data;
        }
        public NoticeInfo GetByID(int id)//通过传用户名得到该用户
        {
            return yiContext.NoticeInfo.FirstOrDefault(r => r.ID == id);
        }
        public bool UpdateInfo(NoticeInfo noticeInfo)//通过对象更新
        {
            yiContext.Entry<NoticeInfo>(noticeInfo).State = EntityState.Modified;
            return yiContext.SaveChanges() > 0;
        }
        public bool DelInfo(int id)//通过ID删除
        {
            NoticeInfo noticeInfo = new NoticeInfo();
            noticeInfo.ID = id;
            yiContext.Entry<NoticeInfo>(noticeInfo).State = EntityState.Deleted;
            return yiContext.SaveChanges() > 0;
        }
        public bool AddInfo(NoticeInfo noticeInfo)//通过对象添加
        {
            yiContext.Add(noticeInfo);
            return yiContext.SaveChanges() > 0;
        }
    }
}
