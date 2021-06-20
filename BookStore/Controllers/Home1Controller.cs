using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class Home1Controller : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BooksList()
        {
            BookStoreContext db = new BookStoreContext();
            var bookList = db.Books.ToList();
            return View(bookList);
        }

        public ActionResult BookDetails(int id)
        {
            Books books = null;

            using (var client = new HttpClient())
            {
                var baseUri = new Uri(HttpContext.Request.UrlReferrer.AbsoluteUri.ToString()) + "api/";

                client.BaseAddress = new Uri(baseUri);
                //HTTP GET
                var responseTask = client.GetAsync("books/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Books>();
                    readTask.Wait();

                    books = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                }
            }
            return View(books);
        }

        public ActionResult AddEditBook(int? id)
        {
            return View();
        }

        public ActionResult DeleteBook(int id)
        {
            return View();
        }


        public ActionResult AuthorList(int? id)
        {
            return View();
        }
        public ActionResult AddEditAuthor(int? id)
        {
            return View();
        }

        public ActionResult DeleteAuthor(int id)
        {
            return View();
        }
    }
}