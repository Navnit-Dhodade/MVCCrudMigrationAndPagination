using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NimapTask.Models
{
    public class ProductMaster
    {
        [Key]
        [Required]
        [DisplayName("Prod Id")]
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Prod Name")]
        public string ProductName { get; set; }


        [Required]
        [DisplayName("Categ Id")]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual CategoryMaster CategoryMaster { get; set; }
    }
}