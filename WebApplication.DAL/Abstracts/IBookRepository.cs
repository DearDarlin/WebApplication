using WebApplication.DAL.Entities;
using System.Linq;

namespace WebApplication.DAL.Abstracts
{
    public interface IBookRepository
    {
        IQueryable<Book> GetAll();
        void Add(Book book);
        void Delete(int id);
        Book GetById(int id);
        void Update(Book book);

        bool IsDuplicate(string title, int authorId);
    }
}
