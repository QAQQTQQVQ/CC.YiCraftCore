using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CC.YiCraftCore.Models
{
    public class QuestionInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "标题")]
        public string question { get; set; }
        [Required]
        [Display(Name = "A")]
        public string A { get; set; }
        [Required]
        [Display(Name = "B")]
        public string B { get; set; }
        [Required]
        [Display(Name = "C")]
        public string C { get; set; }
        [Required]
        [Display(Name = "D")]
        public string D { get; set; }
        [Required]
        [Display(Name = "答案")]
        public string answer { get; set; }

    }
}
