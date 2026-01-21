using Microsoft.EntityFrameworkCore;
using WebApplication.DAL.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public List<Author> GetAll()
        {
            return _context.Authors
                .Include(a => a.Books)
                .OrderBy(a => a.FirstName)
                .ToList();
        }

        public Author GetById(int id)
        {
            return _context.Authors.Find(id);
        }

        public void Update(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
        }

        public bool IsDuplicate(string firstName, string lastName)
        {
            return _context.Authors.Any(a =>
                a.FirstName == firstName &&
                a.LastName == lastName);
        }

        public void Delete(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }
    }
}