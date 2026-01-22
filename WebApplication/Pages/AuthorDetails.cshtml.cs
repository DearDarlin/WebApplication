using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Abstracts;
using WebApplication.Models;
using WebApplication.DAL.Entities;

namespace WebApplication.Pages
{
    public class AuthorDetailsModel : PageModel
    {
        private readonly ILibraryService _libraryService;
        public AuthorDetailsModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public AuthorDTO Author { get; set; }
        public List<Book> AuthorBooks { get; set; }
        public void OnGet(int id)
        {
            Author = _libraryService.GetAuthorById(id);

            if (Author != null)
            {
                AuthorBooks = _libraryService.GetFilteredBooks(null, null, id, "title");
            }
        }
    }
}
