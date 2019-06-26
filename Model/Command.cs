using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Command
    {
        [Key]
        public int idcmd { get; set; }

        public int idprod { get; set; }
        public virtual Product product { get; set; }

        public DateTime datecmd { get; set; }
        public DateTime dateliv { get; set; }
        public int qteprod { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string adresse { get; set; }
        public string email { get; set; }
        bool isComfirmed { get; set; }

    }
}
