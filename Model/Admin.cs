﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Admin
    {
        [Key]
        public int idAdmin { get; set; }
        public string username { get; set; }
        public string password { get; set; }
     
        public string email { get; set; }


    }
}
