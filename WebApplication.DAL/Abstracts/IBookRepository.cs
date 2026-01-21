using WebApplication.DAL.Entities;

namespace WebApplication.DAL.Abstracts
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        void Add(Book book);

        void Delete(int id);
    }
}
