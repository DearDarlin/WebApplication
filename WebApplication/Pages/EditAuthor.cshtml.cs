using WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Abstracts;

namespace WebApplication.Pages
{
    public class EditAuthorModel : PageModel
    {
        private readonly ILibraryService _libraryService;

        public EditAuthorModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [BindProperty]
        public AuthorDTO Author { get; set; }

        public string StatusMessage { get; set; }

        // Завантажуємо дані автора для форми
        public void OnGet(int id)
        {
            Author = _libraryService.GetAuthorById(id);
        }

        // Збереження змінених даних автора після перевірки валідації
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                _libraryService.UpdateAuthor(Author);
                StatusMessage = "Author data successfully updated!";
            }
        }
    }
}
