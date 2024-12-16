using BookApp.Entities;
using BookApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookApp.Controllers
{
    [RoutePrefix("Books")]
    public class BooksController : ApiController
    {
        private IBooksRepository _booksRepository;
        public BooksController()
        {
            _booksRepository = new BooksRepository();
        }

        [Route("GetAllBooks")]
        public IHttpActionResult GetAllBooks()
        {
            var res = _booksRepository.GetAllBooks();
            return Ok(res);
        }

        [Route("AddBook")]
        public IHttpActionResult AddBook([FromBody]Books book)
        {
            _booksRepository.AddBook(book);
            return Ok("Book Added Sucessfully");
        }

        [Route("DeleteBook/{id}")]
        public IHttpActionResult DeleteBook(int id) 
        { 
            _booksRepository.DeleteBook(id);
            return Ok("Book Delete Sucessfully");
        }

        [Route("GetBookById/{id}")]
        public IHttpActionResult GetBookById(int id)
        {
            var res = _booksRepository.GetBookById(id);
            return Ok(res);

        }

        [Route("GetBookByTitle/{title}")]
        public IHttpActionResult GetBookByTitle(string title)
        {
            var res = _booksRepository.GetBookByTitle(title);
            return Ok(res);
        }

        [Route("GetBooksByAuthor/{author}")]
        public IHttpActionResult GetBooksByAuthor(string author)
        {
            var res = _booksRepository.GetBooksByAuthor(author);
            return Ok(res);
        }

        [Route("GetBooksByPublishedYear/{year}")]
        public IHttpActionResult GetBookByPublishedYear(int year)
        {
            var res = _booksRepository.GetBooksByPublishedYear(year);
            return Ok(res);
        }

        [Route("GetBooksByCategory/{category}")]
        public IHttpActionResult GetBooksByCategory(string category)
        {
            var res = _booksRepository.GetBooksByCategory(category);
            return Ok(res);
        }

        [HttpPut,Route("UpdateBook")]
        public IHttpActionResult UpdateBook([FromBody]Books book)
        {
            _booksRepository.UpdateBook(book);
            return Ok("Details updated Sucessfully");

        }


    }
}
