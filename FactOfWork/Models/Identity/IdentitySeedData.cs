using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FactOfWork.Models.Identity
{
    public class IdentitySeedData
    {
        public static void CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            CreateAdminAccountAsync(serviceProvider, configuration).Wait();
        }

        private static async Task CreateAdminAccountAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            
            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = configuration["Data:AdminUser:Name"] ?? "Admin";
            string password = configuration["Data:AdminUser:Password"] ?? "Secret123";
            string surname = configuration["Data:AdminUser:Surname"] ?? "Непомнящий";
            string name = configuration["Data:AdminUser:Name"] ?? "Дмитрий";
            string patr = configuration["Data:AdminUser:Patr"] ?? "Сергеевич";
            string position = configuration["Data:AdminUser:Position"] ?? "Главный специалист-эксперт";
            string department = configuration["Data:AdminUser:Department"] ??
                                "группа эксплуатации и сопровождения информационных подсистем";

            string role = configuration["Data:AdminUser:Role"] ?? "Администратор";
            
            if (await roleManager.FindByNameAsync(role) == null)
            {
                IdentityRole admin = new IdentityRole { Name = role };
                await roleManager.CreateAsync(admin);
            }

            IdentityRole viewRole = new IdentityRole {Name = "Просмотр"};
            if (await roleManager.FindByNameAsync(viewRole.Name) == null)
                await roleManager.CreateAsync(viewRole);

            IdentityRole workRole = new IdentityRole {Name = "Специалист"};
            if (await roleManager.FindByNameAsync(workRole.Name) == null)
                await roleManager.CreateAsync(workRole);

            if (await userManager.FindByNameAsync(username) == null)
            {
                AppUser user = new AppUser
                {
                    UserName = username,
                    Surname = surname,
                    Name = name,
                    Patr = patr,
                    Position = position,
                    Department = department
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
