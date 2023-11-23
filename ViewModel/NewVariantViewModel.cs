using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyGem.ViewModel
{
    public class NewVariantViewModel
    {
        public Variant Variant { get; set; }
        public int? ProductId { get; set; }
        public string Title
        {
            get
            {
                return Variant != null ? "Edit Variant" : "New Variant";
            }
        }
    }
}