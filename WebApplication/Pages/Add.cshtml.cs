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
            bool success_author = _libraryService.AddAuthor(NewAuthor);

            if (!success_author)
            {
                Message = "There is such an author in the library!";
                OnGet();
                return;
            }

            Message = $"Author {NewAuthor.FullName} added!";
            OnGet();
        }

        public void OnPostAddBook()
        {
            bool success_book = _libraryService.AddBook(NewBook); 

            if (!success_book)
            {
                Message = "There is such a book in the library!";
                OnGet();
                return;
            }
            
            Message = $"Book '{NewBook.Title}' added!";
            OnGet();
        }
    }
}
