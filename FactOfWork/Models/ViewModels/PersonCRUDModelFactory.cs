using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Models.ViewModels
{
    public static class PersonCRUDModelFactory
    {
        public static PersonCRUDModel Create(Person person)
        {
            return new PersonCRUDModel()
            {
                Person = person,
                Action = "Create", 
                ReadOnly = false,
                Theme = "primary",
                ShowAction = true,
                ShowZaproses = false
            };
        }

        public static PersonCRUDModel Edit(Person person, File file)
        {
            return new PersonCRUDModel()
            {
                Person = person,
                File = file,
                Theme = "warning",
                Action = "Edit",
                ReadOnly = false,
                ShowAction = true,
                ShowZaproses = true
            };
        }

        public static PersonCRUDModel Delete(Person person, File file)
        {
            return new PersonCRUDModel()
            {
                Person = person,
                File = file,
                Theme = "danger",
                Action = "Delete",
                ReadOnly = true,
                ShowAction = true,
                ShowZaproses = true
            };
        }
    }
}
