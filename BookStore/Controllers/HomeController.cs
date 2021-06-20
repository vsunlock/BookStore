using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BookStoreContext db;
        public HomeController()
        {
            db = new BookStoreContext();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        #region BOOK
        public ActionResult BooksList()
        {
            var bookList = db.Books.ToList();
            return View(bookList);
        }

        public ActionResult AddEditBook(int Id)
        {
            BookViewModel viewModel = new BookViewModel();
            viewModel.authorList = new List<SelectListItem>();
            viewModel.selectedAuthors = new List<int>();
            var listAuthor = db.Authors.ToList();

            foreach(var item in listAuthor)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Value = item.author_id.ToString();
                listItem.Text = item.first_name + " " + item.last_name;

                viewModel.authorList.Add(listItem);
            }

            if (Id > 0)
            { 
                var book = db.Books.Where(x => x.book_id == Id).FirstOrDefault();
                viewModel.book_id = book.book_id;
                viewModel.book_title = book.book_title;
                viewModel.book_id = book.book_id;

                foreach (var item in book.Authors)
                {
                    viewModel.selectedAuthors.Add(item.author_id);
                }
            }

            return PartialView("_AddEditBook", viewModel);
        }

        [HttpPost]
        public ActionResult AddEditBook(BookViewModel bookView)
        {
            try
            {
                Book book = new Book();
                book.book_id = bookView.book_id;
                book.book_title = bookView.book_title;
                book.Authors = new List<Author>();

                foreach(var authorId in bookView.selectedAuthors)
                {
                    var author = db.Authors.Where(x => x.author_id == authorId).FirstOrDefault();
                    book.Authors.Add(author);
                }

                db.Books.Add(book);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("BooksList");
            }
            return RedirectToAction("BooksList");
        }

        public ActionResult DeleteBook(int id)
        {
            var book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectToAction("AuthorList");
        }
        #endregion

        #region AUTHOR
        public ActionResult AuthorList()
        {
            //if (!string.IsNullOrEmpty(TempData["Message"].ToString()))
            //    TempData.Keep("Message");

            var bookList = db.Authors.ToList();
            return View(bookList);
        }

        public ActionResult AddEditAuthor(int Id)
        {
            Author book = new Author();
            if (Id > 0)
                book = db.Authors.Where(x => x.author_id == Id).FirstOrDefault();

            return PartialView("_AddEditAuthor", book);
        }

        [HttpPost]
        public ActionResult AddEditAuthor(Author author)
        {
            try
            {
                db.Authors.Add(author);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("AuthorList");
            }
            return RedirectToAction("AuthorList");
        }

        public ActionResult DeleteAuthor(int id)
        {
            var author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            
            return RedirectToAction("AuthorList");
        }
        #endregion
    }
}