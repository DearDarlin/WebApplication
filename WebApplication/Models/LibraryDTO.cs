using WebApplication.DAL.Entities;

namespace WebApplication.Models
{
    public class LibraryDTO
    {
        public List<Author> Authors { get; set; } = new();
        public List<Book> Books { get; set; } = new();

        public string SearchTitle { get; set; }
        public int? SearchYear { get; set; }
        public int? SelectedAuthorId { get; set; }
        public string SortOrder { get; set; }
    }
}
