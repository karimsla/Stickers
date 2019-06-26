using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Product
    {
        [Key]
        public int idprod { get; set; }
        public string nameprod { get; set; }
        public int qteprod { get; set; }
        public string description { get; set; }
        public string imgprod { get; set; }

        public virtual ICollection<Product> prods { get; set; }
    }
}
