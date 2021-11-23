using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactOfWork.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel;
using File = System.IO.File;

namespace FactOfWork.Infrastructure
{
    public static class FileLoader
    {
        public static FileSetting Setting;

        public static List<Person> ReadFile(string FileName,string webRoot,string area, int PayMonth, int PayYear, int ReportMonth, int ReportYear)
        {
            List<Person> list = new List<Person>();

            string Extention = Path.GetExtension(Path.GetFileName(FileName));
            IWorkbook book;
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            switch (Extention)
            {
                case ".xls":
                    book = new HSSFWorkbook(fs);
                    break;
                case ".xlsx":
                    book = new XSSFWorkbook(fs);
                    break;
                default:
                    book = null;
                    break;
            }

            if (book == null) return list;
            ISheet sheet = book.GetSheetAt(0);
            
            if (sheet == null) return list;
            
            //Создание протокола загрузки документа
            List<string> Logger = new List<string>();
            Logger.Add("Загрузка файла: "+FileName);

            Setting = new FileSetting(sheet);
            for (int i = Setting.NumberRow; i <= sheet.LastRowNum; i++)
            {
                Logger.Add("Начало чтения строки: "+i.ToString());

                var nullablecell = sheet.GetRow(i).GetCell(0);
                if (nullablecell == null)
                {
                    Logger.Add("В строке "+i.ToString() +" отсутствует регион, ПРОПУСК СТРОКИ");
                    continue;
                }
                    

                var currentrow = sheet.GetRow(i);
                if (currentrow != null)
                {
                    int str_numberarea = default;
                    var currentcell = currentrow.GetCell(Setting.Column_NumberArea);
                    if (currentcell != null)
                        str_numberarea = int.Parse(NPOIUtils.GetStringValueFromCell(currentcell));
                    else
                        Logger.Add("Строка "+i.ToString() + " НЕ НАЙДЕНА ЯЧЕЙКА РАЙОН");

                    string str_surname = default;
                    currentcell = currentrow.GetCell(Setting.Column_Surname);
                    if (currentcell != null)
                        str_surname = NPOIUtils.GetStringValueFromCell(currentcell);
                    else
                        Logger.Add("Строка " + i.ToString() + " НЕ НАЙДЕНА ЯЧЕЙКА ФАМИЛИЯ");


                    string str_name = default;
                    currentcell = currentrow.GetCell(Setting.Column_Name);
                    if (currentcell != null)
                        str_name = NPOIUtils.GetStringValueFromCell(currentcell);
                    else
                        Logger.Add("Строка " + i.ToString() + " НЕ НАЙДЕНА ЯЧЕЙКА ИМЯ");


                    string str_patr = default;
                    currentcell = currentrow.GetCell(Setting.Column_Patr);
                    if (currentcell != null)
                        str_patr = NPOIUtils.GetStringValueFromCell(currentcell);
                    else
                        Logger.Add("Строка " + i.ToString() + " НЕ НАЙДЕНА ЯЧЕЙКА ОТЧЕСТВО");


                    string str_snils = default;
                    currentcell = currentrow.GetCell(Setting.Column_SNILS);
                    if (currentcell != null)
                        str_snils = NPOIUtils.GetStringValueFromCell(currentcell);
                    else
                        Logger.Add("Строка " + i.ToString() + " НЕ НАЙДЕНА ЯЧЕЙКА СНИЛС");


                    string str_category = default;
                    currentcell = currentrow.GetCell(Setting.Column_Category);
                    if (currentcell != null)
                        str_category = NPOIUtils.GetStringValueFromCell(currentcell);
                    else
                        Logger.Add("Строка " + i.ToString() + " НЕ НАЙДЕНА ЯЧЕЙКА КАТЕГОРИЯ");

                    Person p = new Person()
                    {
                        Category = str_category,
                        NumberArea = str_numberarea,
                        Surname = str_surname,
                        Name = str_name,
                        Patr = str_patr,
                        Snils = str_snils,
                        Pay_Month = PayMonth,
                        Pay_Year = PayYear,
                        Report_Month = ReportMonth,
                        Report_Year = ReportYear
                    };

                    list.Add(p);
                }
            }



            //сохранение протокола
            string path = webRoot + "/Protocols";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path += "/" + area;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path += "/" + ReportMonth.ToString() + "_" + ReportYear.ToString();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string filedest = path + "/" + "Protocol_" + Path.GetFileNameWithoutExtension(FileName)+".txt";
            if (File.Exists(filedest))
                filedest = path + "/" + "Protocol_" + Guid.NewGuid() + "_" + Path.GetFileNameWithoutExtension(FileName)+".txt";

            File.WriteAllLines(filedest,Logger.ToArray());

            return list;
        }
    }
}
