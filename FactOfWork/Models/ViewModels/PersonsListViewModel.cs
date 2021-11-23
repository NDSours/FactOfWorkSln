using System.Collections.Generic;
using System.Linq;
using FactOfWork.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FactOfWork.Models.ViewModels
{
    public class PersonsListViewModel
    {
        public IEnumerable<Person> Persons { get; set; }
       // public PageingInfo PagingInfo { get; set; }
       
        //фильтры
        public SelectList Area { get; set; }
        public string Snils { get; set; }
        public string Surname { get; set; }
        public string Category { get; set; }
        public SelectList Month { get; set; }
        public SelectList Year { get; set; }
        public bool Arhive { get; set; }
    }
}
