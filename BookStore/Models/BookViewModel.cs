using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Models
{
	public class BookViewModel
	{
		[Display(Name = "Book Id")]
		public int book_id { get; set; }

		[Display(Name = "Book Title")]
		public string book_title { get; set; }

		public List<int>selectedAuthors { get; set; }
        public List<SelectListItem> authorList { get; set; }
    }
}