﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyGem.Models
{
    public class Catagories
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}