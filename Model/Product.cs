using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Product
    {
        [Key]
        public int idprod { get; set; }
        [Required]
        public string nameprod { get; set; }
        [Required]
        public int qteprod { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string imgprod { get; set; }
        [Required]
        public float price { get; set; }
        [NotMapped]
        public int vente { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public virtual ICollection<Command> cmd { get; set; }
    }
}
