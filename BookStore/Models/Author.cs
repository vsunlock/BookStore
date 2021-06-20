using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Author
    {
        [Key]
        [Display(Name = "Author Id")]
        public int author_id { get; set; }

        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime date_of_birth { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}