using CC.YiCraftCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Repository
{
    public interface INoticeInfoRepository
    {
        public List<NoticeInfo> GetAll();
        public NoticeInfo GetByID(int id);
        public bool UpdateInfo(NoticeInfo noticeInfo);
        public bool DelInfo(int id);
        public bool AddInfo(NoticeInfo noticeInfo);//通过对象添加
    }
}
