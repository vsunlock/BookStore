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
            var bookList = db.Books.Include("Authors.Author").ToList();
            return View(bookList);
        }

        public ActionResult AddEditBook(int Id)
        {
            BookViewModel viewModel = new BookViewModel();
            viewModel.authorList = new List<SelectListItem>();
            viewModel.selectedAuthors = new List<int>();
            var listAuthor = db.Authors.ToList();

            foreach (var item in listAuthor)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Value = item.author_id.ToString();
                listItem.Text = item.first_name + " " + item.last_name;
                viewModel.authorList.Add(listItem);
            }

            if (Id > 0)
            {
                var test = db.AuthorBooks.Where(x => x.book_id == Id);
                var book = db.Books.Include("Authors").Where(x=>x.book_id==Id).SingleOrDefault();

                viewModel.book_id = book.book_id;
                viewModel.book_title = book.book_title;
              
                foreach (var item in book.Authors)
                {
                    viewModel.selectedAuthors.Add(item.author_id);
                }
            }
            else
            {
                //foreach (var item in listAuthor)
                //{
                //    SelectListItem listItem = new SelectListItem();
                //    listItem.Value = item.author_id.ToString();
                //    listItem.Text = item.first_name + " " + item.last_name;
                //    viewModel.authorList.Add(listItem);
                //}
            }

            return PartialView("_AddEditBook", viewModel);
        }

        [HttpPost]
        public ActionResult AddEditBook(BookViewModel bookView)
        {
            try
            {
                if (bookView.book_id == 0)
                {
                    Book book = new Book();
                    book.book_id = bookView.book_id;
                    book.book_title = bookView.book_title;
                    //var selectedAuthors = new AuthorBook();
                    db.Books.Add(book);
                    db.SaveChanges();

                    foreach (var authorId in bookView.selectedAuthors)
                    {
                        AuthorBook auth = new AuthorBook();

                        auth.author_id = authorId;
                        auth.book_id = book.book_id;
                        db.AuthorBooks.Add(auth);
                    }
                    db.SaveChanges();
                }
                else
                {
                    var bookEdit = db.Books.Where(x => x.book_id == bookView.book_id).FirstOrDefault();
                    bookEdit.book_title = bookView.book_title;

                    db.Books.Add(bookEdit);
                    db.Entry(bookEdit).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();


                    var removeBook = db.AuthorBooks.Where(x => x.book_id == bookView.book_id).ToList();
                    if (removeBook.Count()>0)
                    {
                        foreach (var item in removeBook)
                        {
                            db.AuthorBooks.Remove(item);
                        }
                        db.SaveChanges();
                    }                    

                    foreach (var authorId in bookView.selectedAuthors)
                    {
                        AuthorBook auth = new AuthorBook();

                        auth.author_id = authorId;
                        auth.book_id = bookView.book_id;
                        db.AuthorBooks.Add(auth);
                    }
                    //db.Entry(bookEdit).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                
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
                if (author.author_id == 0)
                {
                    db.Authors.Add(author);
                    db.SaveChanges();
                }
                else
                {
                    var authorEdit = db.Authors.Where(x => x.author_id == author.author_id).FirstOrDefault();
                    authorEdit.first_name = author.first_name;
                    authorEdit.last_name = author.last_name;
                    authorEdit.date_of_birth = author.date_of_birth;

                    db.Authors.Add(authorEdit);
                    db.Entry(authorEdit).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
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