using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private BookContext context;
        public BooksRepository()
        {
            context = new BookContext();
        }
        public void AddBook(Books book)
        {
            try
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteBook(int id)
        {
            try
            {

                var res = context.Books.SingleOrDefault(e => e.BookId == id);
                context.Books.Remove(res);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Books> GetAllBooks()
        {
            try
            {

                var res = context.Books.ToList();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Books GetBookById(int id)
        {
            try
            {

                var res = context.Books.SingleOrDefault(e => e.BookId == id);
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Books GetBookByTitle(string title)
        {
            try
            {
                var res = context.Books.SingleOrDefault(e => e.BookTitle == title);
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Books> GetBooksByAuthor(string author)
        {
            try
            {
                var res = context.Books.Where(e => e.Author.Equals(author, StringComparison.CurrentCultureIgnoreCase)).ToList();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Books> GetBooksByCategory(string category)
        {
            try
            {
                var res = context.Books.Where(e => e.Category.Equals(category, StringComparison.CurrentCultureIgnoreCase)).ToList();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Books> GetBooksByPublishedYear(int year)
        {
            try
            {
                var res = context.Books.Where(e => e.PublishedYear == year).ToList();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateBook(Books book)
        {
            try
            {
                var existingBook = context.Books.FirstOrDefault(b => b.BookId == book.BookId);

                if (existingBook != null)
                {
                    
                    existingBook.Price = book.Price;
                    
                    existingBook.BookQuantity = book.BookQuantity;


                    context.SaveChanges();
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}