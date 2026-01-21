using WebApplication.DAL.Entities;
using System.Collections.Generic;

namespace WebApplication.Abstracts
{
    public interface ILibraryService
    {
        // Методи для авторів
        IList<Author> GetAllAuthors();
        bool AddAuthor(Author author);

        // Методи для книг
        void AddBook(Book book);
        List<Book> GetFilteredBooks(string title, int? year, int? authorId, string sortOrder);

        bool DeleteAuthor(int authorId, out string message);

        void DeleteBook(int id);
    }
}
