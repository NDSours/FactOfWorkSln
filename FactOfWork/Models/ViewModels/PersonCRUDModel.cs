using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Models.ViewModels
{
    public class PersonCRUDModel
    {
        public Person Person { get; set; }
        public File File { get; set; }
        public string Action { get; set; }
        public bool ReadOnly { get; set; }
        public string Theme { get; set; }
        public bool ShowAction { get; set; }
        public bool ShowZaproses { get; set; }

        public string GetNameTitile()
        {
            return Action switch
            {
                "Create" => "Создание",
                "Edit" => "Редактирование",
                "Delete" => "Удаление",
                _ => "",
            };
        }

        public string GetNameButton()
        {
            return Action switch
            {
                "Create" => "Создать",
                "Edit" => "Сохранить",
                "Delete" => "Удалить",
                _ => "",
            };
        }


    }
}
