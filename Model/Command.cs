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

    }
}
