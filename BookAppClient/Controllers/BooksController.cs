using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using BookAppClient.Models;
using Newtonsoft.Json;

namespace BookAppClient.Controllers
{
    
    
    public class BooksController : Controller
    {
        HttpClient client;
        public BooksController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50983/");//Set Api Address
        }

        [Route("Books/GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            HttpResponseMessage response = client.GetAsync("Books/GetAllBooks").Result;
            List<Books> books = JsonConvert.DeserializeObject<List<Books>>(response.Content.ReadAsStringAsync().Result);
            return View(books);
        }

        [Route("Books/GetBooksByPublishedYear/{year}")]
        public ActionResult GetBooksByPublishedYear(int year)
        {
            HttpResponseMessage response = client.GetAsync($"Books/GetBooksByPublishedYear/{year}").Result;
            List<Books> books = JsonConvert.DeserializeObject<List<Books>>(response.Content.ReadAsStringAsync().Result);
            return View(books);
        }

        [Route("Books/GetBooksByCategory/{category}")]

        public ActionResult GetBooksByCategory(string category)
        {
            HttpResponseMessage response = client.GetAsync($"Books/GetBooksByCategory/{category}").Result;
            List<Books> books = JsonConvert.DeserializeObject<List<Books>>(response.Content.ReadAsStringAsync().Result);
            return View(books);
        }

        [Route("Books/GetBooksByAuthor/{author}")]
        public ActionResult GetBooksByAuthor(string author)
        {
            HttpResponseMessage response = client.GetAsync($"Books/GetBooksByAuthor/{author}").Result;
            List<Books> books = JsonConvert.DeserializeObject<List<Books>>(response.Content.ReadAsStringAsync().Result);
            return View(books);
        }

        [Route("Books/GetBookByTitle/{title}")]

        public ActionResult GetBookByTitle(string title)
        {

            HttpResponseMessage response = client.GetAsync($"Books/GetBookByTitle/{title}").Result;
            Books movie = JsonConvert.DeserializeObject<Books>(response.Content.ReadAsStringAsync().Result);
            return View(movie);

        }

        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddBook(Books book)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(book), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("Books/AddBook", content).Result;
                return RedirectToAction("GetAllBooks");

            }
            else
            {
                return View();
            }


        }

        public ActionResult UpdateBook(int id)
        {
            HttpResponseMessage response = client.GetAsync("Books/GetBookById/" + id).Result;
            Books book = JsonConvert.DeserializeObject<Books>(response.Content.ReadAsStringAsync().Result);
            return View(book);
        }

        [HttpPost]

        public ActionResult UpdateBook(Books book)
        {

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(book), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("Books/UpdateBook", content).Result;
                return RedirectToAction("GetAllBooks");

            }
            else
            {
                return View();
            }
        }
        
        public ActionResult DeleteBook(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"Books/DeleteBook/{id}").Result;
            return RedirectToAction("GetAllBooks");




        }




    }
}