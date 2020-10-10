using CC.YiCraftCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Repository
{
    public interface IQuestionInfoRepository
    {
        public List<QuestionInfo> GetAll();//得到需要题目的集合
    }
}
