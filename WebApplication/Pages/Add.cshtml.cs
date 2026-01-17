using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.DAL.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.Pages
{
    public class AddModel : PageModel
    {
        private readonly IAuthorRepository _authors;
        private readonly IBookRepository _books;

        public AddModel(IAuthorRepository authors, IBookRepository books)
        {
            _authors = authors;
            _books = books;
        }

        [BindProperty]
        public Author NewAuthor { get; set; }

        [BindProperty]
        public Book NewBook { get; set; }
        public IList<Author> AuthorsList { get; private set; }


        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            AuthorsList = _authors.GetAll();
        }

        public IActionResult OnPostAddAuthor()
        {
            bool exists = _authors.IsDuplicate(NewAuthor.FirstName, NewAuthor.LastName);

            if (exists)
            {
                ModelState.AddModelError("Error", "There is such an author in the library! Please, no repetitions!");
                AuthorsList = _authors.GetAll();
                return Page();
            }
            _authors.Add(NewAuthor);
            Message = $"Author {NewAuthor.FirstName} {NewAuthor.LastName} added successfully!";
            return RedirectToPage();
        }

        public IActionResult OnPostAddBook()
        {
            _books.Add(NewBook);
            Message = $"Book '{NewBook.Title}' added successfully!";
            return RedirectToPage();
        }
    }
}
