using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.DAL.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.Pages
{
    public class LibraryModel : PageModel
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public LibraryModel(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
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
            Authors = _authorRepository.GetAll();

            AuthorSelectList = new SelectList(Authors, "Id", "FullName");
            var query = _bookRepository.GetAll().AsEnumerable();

            if (!string.IsNullOrEmpty(SearchTitle))
            {
                query = query.Where(b => b.Title.Contains(SearchTitle, StringComparison.OrdinalIgnoreCase));
            }
            if (SearchYear.HasValue)
            {
                query = query.Where(b => b.PublishYear == SearchYear.Value);
            }


            Books = query.ToList();
            switch (SortOrder)
            {
                case "title_desc":
                    query = query.OrderByDescending(b => b.Title);
                    break;
                case "year_asc":
                    query = query.OrderBy(b => b.PublishYear);
                    break;
                case "year_desc":
                    query = query.OrderByDescending(b => b.PublishYear);
                    break;
                default:
                    query = query.OrderBy(b => b.Title);
                    break;
            }

            Books = query.ToList();
        }
    }
}

