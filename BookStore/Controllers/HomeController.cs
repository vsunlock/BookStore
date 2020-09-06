using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BooksList()
        {
            return View();
        }

        public ActionResult BookDetails(int id)
        {
            Books books = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59229/api/");
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
    }
}