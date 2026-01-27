using Microsoft.EntityFrameworkCore;
using WebApplication.DAL.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public IQueryable<Book> GetAll()
        {
            return _context.Books.Include(b => b.Author);
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public Book GetById(int id)
        {
            return _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
    }
}
