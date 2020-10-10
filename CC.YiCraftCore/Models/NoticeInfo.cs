using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Models
{
    public class NoticeInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "标题")]
        public string Tit { get; set; }
        [Required]
        [Display(Name = "内容")]
        public string Info { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "发表时间")]
        public string SubTime { get; set; }
        [MaxLength(10)]
        [Display(Name = "作者")]
        public string Uname { get; set; }
        [Display(Name = "类型")]
        public int Type { get; set; }
    }
}
