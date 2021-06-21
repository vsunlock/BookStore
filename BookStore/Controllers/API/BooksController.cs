using BookStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BookStore.Controllers
{
    public class BooksController : ApiController
    {
        BookStoreContext db;
        public BooksController()
        {
            db = new BookStoreContext();
        }
        readonly List<Books> books = new List<Books> {
            new Books { Id=1,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Once upon a time in Nagpur",ISBN="2316541234545",Author="Jeff Hans",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book1.jpg",},
            new Books { Id=2,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Catch me if you can",ISBN="2316541000001",Author="Rough Ren",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book2.jpg",},
            new Books { Id=3,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Tamil hunters",ISBN="2316541000002",Author="Ram khrishan",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book3.jpg",},
            new Books { Id=4,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Dhoni biopic",ISBN="2316541000003",Author="Sabu Sidhik",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book4.jpg",},
            new Books { Id=5,CR_Date=DateTime.Now,CR_By="Book Store Owner",Name="Meluha",ISBN="2316541000004",Author="Neha Wele",Description="First of all, create MVC controller class called StudentController in the Controllers folder as shown below. Right click on the Controllers folder > Add.. > select Controller.. Step 2: We need to access Web API in the Index() action method using HttpClient as shown below.",ImagePath="/Content/Images/book1.jpg",}
        };

        #region OLD CODE
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

        //[HttpPost]
        public IHttpActionResult AddEditAuthor(Author author)
        {
            try
            {
                db.Authors.Add(author);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        public IHttpActionResult DeleteAuthor(int id)
        {
            try
            {
                var author = db.Authors.Find(id);
                db.Authors.Remove(author);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        #endregion

        [HttpGet]
        [Route("api/Author/{ID:int}")]
        public IHttpActionResult Author(int id)
        {
            try
            {
                var author = (from ab in db.Authors.Where(x=>x.author_id==id)
                              select new
                              {
                                  author_id=ab.author_id,
                                  first_name=ab.first_name,
                                  last_name=ab.last_name,
                                  date_of_birth=ab.date_of_birth,
                                  books= (from abb in db.AuthorBooks.Where(x => x.author_id == id)
                                          from b in db.Books.Where(x => x.book_id == abb.book_id)
                                          select b
                             ).ToList()
                              }
                              ).FirstOrDefault();
                
                return Ok(author);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
