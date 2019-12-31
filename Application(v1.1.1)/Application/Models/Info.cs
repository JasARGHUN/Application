using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class Info //Контакты, телефонные номера, логотип, социальная сеть.
    {
        public int InfoID { get; set; }

        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string AppName { get; set; } //Название приложения.

        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string AppAddress { get; set; } //Адрес организации.

        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string AppPhone1 { get; set; } //Первый телефоный номер.

        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string AppPhone2 { get; set; } //Второй телефонный номер.
        public string AppImage { get; set; } //Логотип приложения.
        public string AppSocialImg { get; set; } //Изображения социальной сети.
        public string AppSocialAddress { get; set; } //Ссылка на социальную сеть.
        public string AppBackgroundImage { get; set; } //Заднии фон приложения.

        [EmailAddress(ErrorMessage = "Неправильный формат электронной почты")]
        public string AppEmail { get; set; } //Почтовый адрес приложения.

    }
}
