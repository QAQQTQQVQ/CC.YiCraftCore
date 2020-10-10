using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Models
{
    public class YIContext : DbContext
    {
        public YIContext(DbContextOptions<YIContext> options) : base(options)
        {

        }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<NoticeInfo> NoticeInfo { get; set; }
        public DbSet<CommentInfo> CommentInfo { get; set; }
        public DbSet<QuestionInfo> QuestionInfo { get; set; }
    }
}
