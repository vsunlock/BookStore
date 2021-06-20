using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Book
    {
        [Key]
        [Display(Name ="Book Id")]
        public int book_id { get; set; }

        [Display(Name = "Book Title")]
        public string book_title { get; set; }

        public ICollection<Author> Authors{ get; set; }
        
    }
}