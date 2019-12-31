using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.ViewModels
{
    public class InfoCreateViewModel
    {
        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string AppName { get; set; } //Название приложения.

        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string AppAddress { get; set; } //Адрес организации.

        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string AppPhone1 { get; set; } //Первый телефоный номер.

        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string AppPhone2 { get; set; } //Второй телефонный номер.
        public List<IFormFile> AppImages { get; set; } //Логотип приложения.
        public List<IFormFile> AppSocialImgs { get; set; } //Изображения социальной сети.
        public string AppSocialAddress { get; set; } //Ссылка на социальную сеть.
        public List<IFormFile> AppBackgroundImages { get; set; } //Заднии фон приложения.
        public string AppEmail { get; set; } //Почтовый адрес приложения.
    }
}
