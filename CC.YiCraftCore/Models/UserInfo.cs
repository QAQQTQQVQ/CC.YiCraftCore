using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Models
{
    public class UserInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "密码")]
        public string Pwd { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "等级")]
        public int LV { get; set; }
        [MaxLength(10)]
        [Display(Name = "创建时间")]
        public string CreateTime { get; set; }
        [MaxLength(50)]
        [Display(Name = "邮箱")]
        public string Mail { get; set; }
    }
}
