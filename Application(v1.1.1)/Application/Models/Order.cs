using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Required(ErrorMessage = "Введите Ваше Имя...")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите Ваш адрес...")]
        public string Line1 { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Неправильный формат электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ввведите город...")]
        public string City { get; set; }

        [Required(ErrorMessage = "Введите Ваш телефоный номер...")]
        [StringLength(20, ErrorMessage = "Телефонный номер не может быть больше 20 символов.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Zip { get; set; }

        public DateTime orderTime = DateTime.Now;

        public DateTime OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; } //когда приняли покупку.

        }

        public decimal TotalAmount { get; set; } //Общая сумма покупки
    }
}
