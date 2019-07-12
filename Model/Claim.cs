
using System;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class Claim
    {
        [Key]
        public int idclaim { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string obj {get;set;}
        [Required]
        public string body { get; set; }
        [Required]
        public string name { get; set; }
        public DateTime claimdate { get; set; }
        public bool seen { get; set; }
    }
}
