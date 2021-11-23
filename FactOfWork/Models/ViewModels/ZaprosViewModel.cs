using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Models.ViewModels
{
    public class ZaprosViewModel
    {
        public long PersonId { get; set; }
        public Zapros Zapros { get; set; }
        public string Action { get; set; } = "createzapros";
        public string Theme { get; set; } = "primary";

        public string GetNameTitile()
        {
            return Action switch
            {
                "createzapros" => "Создание запроса",
                "editzapros" => "Редактирование запроса",
                "deletezapros" => "Удаление",
                _ => ""
            };
        }

        public string GetNameButton()
        {
            return Action switch
            {
                "createzapros" => "Создать",
                "editzapros" => "Сохранить",
                "deletezapros" => "Удалить",
                _ => ""
            };
        }
    }
}
