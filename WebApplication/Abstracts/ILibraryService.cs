using WebApplication.Models;
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
        bool AddBook(Book book);
        List<Book> GetFilteredBooks(string title, int? year, int? authorId, string sortOrder);

        bool DeleteAuthor(int authorId, out string message);

        void DeleteBook(int id);

        AuthorDTO GetAuthorById(int id);
        void UpdateAuthor(AuthorDTO authorDto);
        Book GetBookById(int id);
        void UpdateBook(Book book);
    }
}
