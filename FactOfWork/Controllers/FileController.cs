using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FactOfWork.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using FactOfWork.Infrastructure;
using FactOfWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace FactOfWork.Controllers
{
    [Authorize(Roles = "Администратор,Специалист")]
    public class FileController : Controller
    {
        private IRepositoryFile _repofile;
        private IRepositoryPerson _repoperson;
        private IWebHostEnvironment _appEnvironment;

        public FileController(IRepositoryFile repofile,IRepositoryPerson repoperson, IWebHostEnvironment env)
        {
            _repofile = repofile;
            _repoperson = repoperson;
            _appEnvironment = env;
        }
        public IActionResult Index()
        {
            // создание списков месяцев и годов
            LoadingFilesViewModel model = new LoadingFilesViewModel()
            {
                PayMonth = new SelectList(new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12},(object)(DateTime.Now.Month+1)),
                PayYear = new SelectList(MyDateUtils.GetRangeYear(),(object)DateTime.Now.Year),
                ReportMonth = new SelectList(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, (object)(DateTime.Now.Month - 2)),
                ReportYear = new SelectList(MyDateUtils.GetRangeYear(), (object)DateTime.Now.Year),
            };

            return View(model);
        }
        public IActionResult InfoPage() => View();
        
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile, int PayMonth, int PayYear, int ReportMonth, int ReportYear)
        {
            if (uploadedFile != null)
            {
                // проверка выбран ли корректный фаил
                if (Path.GetExtension(uploadedFile.FileName) == ".xls"
                    || Path.GetExtension(uploadedFile.FileName) == ".xlsx")
                {
                    //формируем конечный путь в архиве для выбранного файла
                    
                    //определяем номер района из названия файла
                    var regexpr = new Regex(@"\d+");
                    MatchCollection matches = regexpr.Matches(uploadedFile.FileName);
                    string area = "1";
                    if (matches.Count > 0)
                        area = matches.Last().Value;

                    string path = _appEnvironment.WebRootPath + "/ArhiveFiles";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path += "/"+area;
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path += "/" + ReportMonth.ToString() + "_" + ReportYear.ToString();
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string filenamedest =path + "/" + uploadedFile.FileName;

                    if (System.IO.File.Exists(filenamedest))
                        filenamedest = path + "/" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "_" +
                                       Guid.NewGuid() + Path.GetExtension(uploadedFile.FileName); 

                    using (var fileStream = new FileStream(filenamedest, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    // создаем элемент File
                    Models.File file = new Models.File()
                    {
                        FileName = uploadedFile.FileName,
                        PathArhive = filenamedest
                    };

                    // парсим фаил
                    List<Person> LoadedList = FileLoader.ReadFile(file.PathArhive,_appEnvironment.WebRootPath,area,PayMonth,PayYear,ReportMonth,ReportYear);

                    // при успешном парсинге файла сохраняем всю необходимую информацию в бд
                    foreach (var item in LoadedList)
                        item.File = file;
                    _repoperson.AddRange(LoadedList);

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return RedirectToAction(nameof(InfoPage));
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
