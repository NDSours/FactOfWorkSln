using System.Collections;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FactOfWork.Models
{
    public class Person
    {
        [Key]
        public long Id { get; set; }
        public int NumberArea { get; set; }
        
        [Required(ErrorMessage = "Ошибка ввода данных в поле 'Фамилия'")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Ошибка ввода данных в поле 'Имя'")]
        public string Name { get; set; }
        public string Patr { get; set; }

        //[Required(ErrorMessage = "Ошибка ввода данных в поле 'СНИЛС'")]
        [RegularExpression("^\\d{3}-\\d{3}-\\d{3} \\d{2}$", ErrorMessage = "Ошибка ввода данных в поле 'СНИЛС' требуемая форма:111-111-111 11")]
        public string Snils { get; set; }
        public string Category { get; set; }
        public bool HaveOverPayments { get; set; }
        public bool HaveWorkBook { get; set; }
        public int Pay_Year { get; set; }
        public int Pay_Month { get; set; }
        public int Report_Year { get; set; }
        public int Report_Month { get; set; }
        public long FileId { get; set; }
        public File File { get; set; }
        public long ZaprosId { get; set; }
        public List<Zapros> Zaproses { get; set; } = new List<Zapros>();
        public bool Arhive { get; set; } = false;


        public override string ToString()
        {
            return this.Surname + " " + this.Name + " " + this.Patr;
        }
    }
}
