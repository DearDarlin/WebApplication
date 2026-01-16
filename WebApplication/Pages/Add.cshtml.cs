using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.DAL.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.Pages
{
    public class AddModel : PageModel
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public AddModel(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Author NewAuthor { get; set; }

        [BindProperty]
        public Book NewBook { get; set; }
        public List<Author> AuthorsList { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            AuthorsList = _authorRepository.GetAll();
        }

        public IActionResult OnPostAddAuthor()
        {
            bool exists = _authorRepository.IsDuplicate(NewAuthor.FirstName, NewAuthor.LastName);

            if (exists)
            {
                ModelState.AddModelError("Error", "Such of author already exists");
                AuthorsList = _authorRepository.GetAll();
                return Page();
            }
            _authorRepository.Add(NewAuthor);
            Message = $"Author {NewAuthor.FirstName} {NewAuthor.LastName} added successfully!";
            return RedirectToPage();
        }

        public IActionResult OnPostAddBook()
        {
            _bookRepository.Add(NewBook);
            Message = $"Book '{NewBook.Title}' added successfully!";
            return RedirectToPage();
        }
    }
}
