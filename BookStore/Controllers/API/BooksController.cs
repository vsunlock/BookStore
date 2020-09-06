using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Controllers
{
    public class BooksController : ApiController
    {
        readonly List<Books> books = new List<Books> {
            new Books { Id=1,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Once upon a time in Nagpur",ISBN="2316541234545",Author="Jeff Hans",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book1.jpg",},
            new Books { Id=2,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Catch me if you can",ISBN="2316541000001",Author="Rough Ren",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book2.jpg",},
            new Books { Id=3,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Tamil hunters",ISBN="2316541000002",Author="Ram khrishan",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book3.jpg",},
            new Books { Id=4,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Dhoni biopic",ISBN="2316541000003",Author="Sabu Sidhik",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book4.jpg",},
            new Books { Id=5,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Meluha",ISBN="2316541000004",Author="Neha Wele",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book1.jpg",}
        };

        public IEnumerable<Books> BooksList()
        {
            return books;
        }

        //GET / api/books
        public IHttpActionResult GetBooks()
        {
            var books = BooksList().ToList();

            if (books != null)
                return Ok(books);
            else
                return BadRequest("No books in the store.");
        }

        //GET / api/books
        public IHttpActionResult GetBooks(int id)
        {
            var books = BooksList().Where(x=>x.Id==id).SingleOrDefault();

            if (books != null)
                return Ok(books);
            else
                return BadRequest("Book not found!");
        }
    }
}
