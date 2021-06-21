using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class AuthorBook
    {
        [Key, Column(Order = 1)]
        public int author_id { get; set; }
        [Key, Column(Order = 2)]
        public int book_id { get; set; }

        public Author Author { get; set; }
        public Book Book { get; set; }
        
    }
}