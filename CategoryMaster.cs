using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NimapTask.Models
{
    public class CategoryMaster
    {
        [Key]
        [Required]
        [DisplayName("Categ ID")]
        public int CategoryID { get; set; }

        [Required]
        [DisplayName("Categ Name")]
        public string CategoryName { get; set; }
    }
}