using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Abstracts;
using WebApplication.DAL.Entities;

namespace WebApplication.Pages
{
    public class EditBookModel : PageModel
    {
        private readonly ILibraryService _service;

        [BindProperty]
        public Book Book { get; set; }

        public SelectList AuthorSelectList { get; set; }

        // Додаємо властивість для сповіщення
        public string StatusMessage { get; set; }

        public EditBookModel(ILibraryService service)
        {
            _service = service;
        }

        public void OnGet(int id)
        {
            Book = _service.GetBookById(id);
            LoadAuthors();
        }

        public void OnPost()
        {
            ModelState.Remove("Book.Author");
            // Перевірка обмеження року
            int currentYear = DateTime.Now.Year;
            if (Book.PublishYear < 1450 || Book.PublishYear > currentYear)
            {
                ModelState.AddModelError("Book.PublishYear", $"Рік має бути від 1450 до {currentYear}");
            }

            if (ModelState.IsValid)
            {
                _service.UpdateBook(Book);
                StatusMessage = "Зміни успішно збережено!"; // Повідомляємо про успіх
            }

            // Обов'язково перезавантажуємо список авторів, щоб dropdown не став порожнім
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            var authors = _service.GetAllAuthors();
            // Використовуємо FullName з твого LibraryDTO
            AuthorSelectList = new SelectList(authors, "Id", "FullName");
        }
    }



}
