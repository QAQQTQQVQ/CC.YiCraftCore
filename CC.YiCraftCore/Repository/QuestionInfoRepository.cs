using CC.YiCraftCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Repository
{
    public class QuestionInfoRepository:IQuestionInfoRepository
    {
        private YIContext yiContext;
        public QuestionInfoRepository(YIContext _yiContext)
        {
            yiContext = _yiContext;
        }
        
        public List<QuestionInfo> GetAll()//得到需要题目的集合
        {
            List<QuestionInfo> data = new List<QuestionInfo>();
            foreach (QuestionInfo u in yiContext.QuestionInfo)
            {
                data.Add(u);
            }
            return data;
        }
    }
}
