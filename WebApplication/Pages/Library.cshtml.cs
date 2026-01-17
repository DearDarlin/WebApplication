using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.DAL.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.Pages
{
    public class LibraryModel : PageModel
    {
        private readonly IAuthorRepository _authors;
        private readonly IBookRepository _books;

        public LibraryModel(IAuthorRepository authors, IBookRepository books)
        {
            _authors = authors;
            _books = books;
        }

        public List<Author> Authors { get; set; } = new();
        public List<Book> Books { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchYear { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedAuthorId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public SelectList AuthorSelectList { get; set; }

        public void OnGet()
        {
            Authors = _authors.GetAll();
            AuthorSelectList = new SelectList(Authors, "Id", "FullName");

            var query = _books.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTitle))
            {
                query = query.Where(b =>
                    b.Title.Contains(SearchTitle, StringComparison.OrdinalIgnoreCase));
            }

            if (SearchYear.HasValue)
            {
                query = query.Where(b => b.PublishYear == SearchYear.Value);
            }

            if (SelectedAuthorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == SelectedAuthorId.Value);
            }

            query = SortOrder switch
            {
                "title_desc" => query.OrderByDescending(b => b.Title),
                "year_asc" => query.OrderBy(b => b.PublishYear),
                "year_desc" => query.OrderByDescending(b => b.PublishYear),
                _ => query.OrderBy(b => b.Title)
            };

            Books = query.ToList();
        }
    }
}

