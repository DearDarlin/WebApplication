using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Abstracts;
using WebApplication.Models;

namespace WebApplication.Pages
{
    public class LibraryModel : PageModel
    {
        private readonly ILibraryService _libraryService;

        public string Message { get; set; }

        [BindProperty]
        public LibraryDTO Model { get; set; }

        public SelectList AuthorSelectList { get; set; }

        public LibraryModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public void OnGet()
        {
            if (Model == null)
            {
                Model = new LibraryDTO();
            }

            var authors = _libraryService.GetAllAuthors().ToList();
            Model.Authors = authors;

            AuthorSelectList = new SelectList(authors, "Id", "FullName");

            Model.Books = _libraryService.GetFilteredBooks(
                Model.SearchTitle,
                Model.SearchYear,
                Model.SelectedAuthorId,
                Model.SortOrder
            );
        }



        public void OnPostDeleteAuthor(int id)
        {
            string msg;
            _libraryService.DeleteAuthor(id, out msg);

            Message = msg;

            OnGet();
        }

        public void OnPostDeleteBook(int id)
        {
            _libraryService.DeleteBook(id);

            Message = "Book successfully deleted!";

            OnGet();
        }
    }
}