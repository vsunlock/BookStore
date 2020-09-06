using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Book Name")]
        public string Name { get; set; }

        [Display(Name = "International Standard Book Number")]
        [MaxLength(13)]
        public string ISBN { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public DateTime CR_Date{ get; set; }
        public string CR_By { get; set; }
        public DateTime MD_Date{ get; set; }
        public string MD_By{ get; set; }

    }
}