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

        //SupportsGet = true дозволяє автоматично заповнювати поля моделі з параметрів запиту GET
        //Дозволяє фільтрам зберігатись у URL
        [BindProperty(SupportsGet = true)]
        public LibraryDTO Model { get; set; }

        public SelectList AuthorSelectList { get; set; }

        public LibraryModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        // Метод спрацьовує при завантаженні сторінки або при натисканні кнопки пошуку
        public void OnGet()
        {
            //
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

        public void OnGetSearch()
        {
            // Фільтри вже заповнені завдяки [BindProperty(SupportsGet = true)]
            OnGet();
        }

        public void OnGetReset()
        {
            // Скидаємо фільтри
            Model.SearchTitle = null;
            Model.SearchYear = null;
            Model.SelectedAuthorId = null;
            Model.SortOrder = null;
            //Очищаємо стан моделі (щоб старі значення не залишилися в input-полях)
            ModelState.Clear();
            // Оновлення списку авторів та книг
            OnGet();

        }

        public void OnPostDeleteAuthor(int id)
        {
            string msg;
            // Видалення автора з бази з перевіркою на наявність книг
            _libraryService.DeleteAuthor(id, out msg);
            // Відображення повідомлення про результат видалення
            Message = msg;
            // Оновлення списку авторів та книг
            OnGet();
        }

        public void OnPostDeleteBook(int id)
        {
            //Видалення книги з бази
            _libraryService.DeleteBook(id);

            Message = "Book successfully deleted!";
            // Оновлення списку книг
            OnGet();
        }
    }
}