using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Command
    {
        [Key]
        public int idcmd { get; set; }
        [ForeignKey("product")]
        public int idprod { get; set; }
        public virtual Product product { get; set; }
        [Required]
        public DateTime datecmd { get; set; }

        public DateTime? dateliv { get; set; }

        public int qteprod { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [StringLength(8)]
        public string phone { get; set; }
        [StringLength(8)]
        public string phone2 { get; set; }
        [Required]
        public string adresse { get; set; }
        [Required]
        public string gov { get; set; }
        [Required]
        [StringLength(4)]
        public string code { get; set; }
        [Required]
        public string email { get; set; }
        public bool isComfirmed { get; set; }

    }
}
