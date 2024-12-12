using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Repositories
{
    internal interface IBooksRepository
    {
        void AddBook(Books book);
        void DeleteBook(int id);
        void UpdateBook(Books book);
        List<Books> GetAllBooks();
        Books GetBookById(int id);
        Books GetBookByTitle(string title);

        List<Books> GetBooksByAuthor(string author);

        List<Books> GetBooksByPublishedYear(int year);
        List<Books> GetBooksByCategory(string category);    


    }
}
