
using System;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class Claim
    {
        [Key]
        public int idclaim { get; set; }
        public string email { get; set; }
        public string obj {get;set;}
        public string body { get; set; }
        public string name { get; set; }
        public DateTime claimdate { get; set; }
        public bool seen { get; set; }
    }
}
