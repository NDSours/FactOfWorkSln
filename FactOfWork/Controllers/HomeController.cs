using System;
using System.Collections.Generic;
using System.Linq;
using FactOfWork.Infrastructure;
using FactOfWork.Models;
using Microsoft.AspNetCore.Mvc;
using FactOfWork.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPOI.OpenXmlFormats.Dml;


namespace FactOfWork.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IRepositoryPerson repo;
        public int PageSize = 10;
        public HomeController(IRepositoryPerson rep) => repo = rep;

        [Authorize(Roles = "Администратор,Специалист")]
        public IActionResult Create()
        {
            return View("PersonEdit", PersonCRUDModelFactory.Create(new Person()));
        }

        [HttpPost]
        [Authorize(Roles = "Администратор,Специалист")]
        public IActionResult Create([FromForm] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = default;  //следим чтобы id задавался только сервером
                //так как требуется фаил (обязательная сслыка на фаил с которого берется информация)
                //был создан объект file указывающий на то, что человек добавлен в ручную
                person.FileId = 1;
                if (person.HaveOverPayments)
                    person.Arhive = true;
                repo.AddPerson(person);
                return RedirectToAction(nameof(Index));
            }

            return View("PersonEdit", PersonCRUDModelFactory.Create(person));
        }

        [Authorize(Roles = "Администратор,Специалист")]
        public IActionResult Edit(long Id)
        {
            Person p = repo.GetPersonWithSubEntites(Id);
            PersonCRUDModel model = PersonCRUDModelFactory.Edit(p, p.File);
            return View("PersonEdit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор,Специалист")]
        public IActionResult Edit([FromForm] Person person)
        {
            if (ModelState.IsValid)
            {
                person.File = default;
                if (person.HaveOverPayments)
                    person.Arhive = true;
                repo.Update(person);
                return RedirectToAction(nameof(Index));
            }

            return View("PersonEdit", PersonCRUDModelFactory.Edit(person, person.File));
        }

        [Authorize(Roles = "Администратор,Специалист")]
        public IActionResult Delete(long Id)
        {
            Person p = repo.GetPersonWithSubEntites(Id);
            PersonCRUDModel model = PersonCRUDModelFactory.Delete(p, p.File);
            return View("PersonEdit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор,Специалист")]
        public IActionResult Delete(Person person)
        {
            repo.Delete(person);
            return RedirectToAction(nameof(Index));
        }
      
        public IActionResult Index(string area = "все",string Snils = "",string Surname = "",string Category="",string Month = "", string Year = "",bool arhive = false)
        {
            ViewBag.Login = User.Identity.Name;

            if (String.IsNullOrEmpty(Month))
                Month = DateTime.Now.Month.ToString();

            if (String.IsNullOrEmpty(Year))
                Year = DateTime.Now.Year.ToString();

            //фильтрация данных
            //получить объект для формирования запроса
            IQueryable<Person> persons = repo.QuerablePersons;

            //применить фильтр по району
            if (!String.IsNullOrEmpty(area) && area != "все")
            {
                persons = persons.Where(p => p.NumberArea == Convert.ToInt32(area));
            }

            if (!String.IsNullOrEmpty(Snils))
            {
                persons = persons.Where(p => p.Snils.Contains(Snils));
            }

            if (!String.IsNullOrEmpty(Surname))
            {
                persons = persons.Where(p => p.Surname.ToLower().Contains(Surname.ToLower()));
            }

            if (!String.IsNullOrEmpty(Category))
            {
                persons = persons.Where(p => p.Category.ToLower().Contains(Category.ToLower()));
            }

            if (!String.IsNullOrEmpty(Month) && Month != "все")
            {
                persons = persons.Where(p => p.Report_Month == Convert.ToInt32(Month));
            }

            if (!String.IsNullOrEmpty(Year) && Year != "все")
            {
                persons = persons.Where(p => p.Report_Year == Convert.ToInt32(Year));
            }

            persons = persons.Where(p => p.Arhive == arhive);
            persons = persons.OrderBy(p => p.Surname);
                //.Skip((PersonPage - 1) * PageSize)
                //.Take(PageSize);

            //формируем List<string> для годов, содержащихся в БД
            var YearList = new List<string>();
            YearList.Add("все");
            foreach (var item in MyDateUtils.GetRangeYear())
            {
                YearList.Add(item.ToString());
            }

            var lvm = new PersonsListViewModel()
            {
                Persons = persons.ToArray(),
                
                Area = new SelectList(new List<string>()
                {
                    "все","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22"
                }),
                Month = new SelectList(new List<string>() {"все","1","2","3","4","5","6","7","8","9","10","11","12"} , (object)Month),
                Year = new SelectList(YearList,(object)Year),
                Arhive = arhive
            };
            return View(lvm);
        }
    }
}
