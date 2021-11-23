using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FactOfWork.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            SqlDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<SqlDbContext>();
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
            if (!context.Persons.Any())
            {
                Models.File HandMaidFile = new File()
                {
                    FileName = "NONE"
                };
                context.Files.Add(HandMaidFile);
                context.SaveChanges();

                
                /*Models.File file = new File()
                {
                    FileName = "TestFile"
                };
                context.Persons.AddRange(
                        new Person()
                        {
                            NumberArea = 1,
                            Surname = "Иванов",
                            Name = "Иван",
                            Patr = "Иванович",
                            Snils = "123-345-566 99",
                            Category = "Пенсионер",
                            HaveOverPayments = true,
                            HaveWorkBook = false,
                            Pay_Year = 2021,
                            Pay_Month = 11,
                            Report_Year = 2021,
                            Report_Month = 8,
                            File = file
                        },
                        new Person()
                        {
                            NumberArea = 1,
                            Surname = "Сидоров",
                            Name = "Анатолий",
                            Patr = "Иванович",
                            Snils = "124-345-566 98",
                            Category = "Пенсионер",
                            HaveOverPayments = true,
                            HaveWorkBook = true,
                            Pay_Year = 2021,
                            Pay_Month = 11,
                            Report_Year = 2021,
                            Report_Month = 8,
                            File = file
                        },
                        new Person()
                        {
                            NumberArea = 2,
                            Surname = "Петренко",
                            Name = "Андрей",
                            Patr = "Васильевич",
                            Snils = "123-345-566 99",
                            Category = "Рабочий",
                            HaveOverPayments = false,
                            HaveWorkBook = true,
                            Pay_Year = 2021,
                            Pay_Month = 10,
                            Report_Year = 2021, 
                            Report_Month = 7,
                            File = file
                        }
                );
                context.SaveChanges();*/
            }
            
        }
    }
}
