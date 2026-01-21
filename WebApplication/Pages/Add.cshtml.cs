using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.Pages
{
    public class AddModel : PageModel
    {
        private readonly ILibraryService _libraryService;

        public AddModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [BindProperty]
        public Author NewAuthor { get; set; }

        [BindProperty]
        public Book NewBook { get; set; }

        public IList<Author> AuthorsList { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            AuthorsList = _libraryService.GetAllAuthors();
        }

        public void OnPostAddAuthor()
        {
            bool success = _libraryService.AddAuthor(NewAuthor);

            if (!success)
            {
                Message = "There is such an author in the library!";
                this.OnGet();
                return;
            }

            Message = $"Author {NewAuthor.FirstName} {NewAuthor.LastName} added!";
            this.OnGet();
        }

        public void OnPostAddBook()
        {
            _libraryService.AddBook(NewBook);
            Message = $"Book '{NewBook.Title}' added!";
            this.OnGet();
        }
    }
}
