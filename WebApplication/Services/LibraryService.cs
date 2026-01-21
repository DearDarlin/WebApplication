using Microsoft.EntityFrameworkCore;
using WebApplication.Abstracts;
using WebApplication.DAL.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IAuthorRepository _authors;
        private readonly IBookRepository _books;

        public LibraryService(IAuthorRepository authors, IBookRepository books)
        {
            _authors = authors;
            _books = books;
        }

        public IList<Author> GetAllAuthors() => _authors.GetAll();

        public bool AddAuthor(Author author)
        {
            if (_authors.IsDuplicate(author.FirstName, author.LastName))
            {
                return false;
            }

            _authors.Add(author);
            return true;
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public List<Book> GetFilteredBooks(string title, int? year, int? authorId, string sortOrder)
        {
            var query = _books.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (year.HasValue)
            {
                query = query.Where(b => b.PublishYear == year.Value);
            }

            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }

            query = sortOrder switch
            {
                "title_desc" => query.OrderByDescending(b => b.Title),
                "year_asc" => query.OrderBy(b => b.PublishYear),
                "year_desc" => query.OrderByDescending(b => b.PublishYear),
                _ => query.OrderBy(b => b.Title)
            };

            return query.ToList();
        }

        public bool DeleteAuthor(int authorId, out string message)
        {
            var author = _authors.GetAll()
                                 .FirstOrDefault(a => a.Id == authorId);


            var hasBooks = _books.GetAll()
                                 .Any(b => b.AuthorId == authorId);

            if (hasBooks)
            {
                message = "You cannot delete this author because there are books associated with him";
                return false;
            }

            _authors.Delete(authorId);

            message = "Author was successfully deleted";
            return true;
        }

        public void DeleteBook(int id)
        {
            _books.Delete(id);
        }

    }
}
