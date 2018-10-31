using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CAStest.Data;
using CAStest.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CAStest
{
    public class ModelData
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public ModelData(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async void Initialize()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if (!context.PropertyTypes.Any())
                {
                    List<PropertyType> types = new List<PropertyType>() { new PropertyType() { Name = "Строка" }, new PropertyType() { Name = "Число" }, new PropertyType() { Name = "Дата" } };
                    context.PropertyTypes.AddRange(types);
                    context.SaveChanges();

                }

                if (!roleManager.Roles.Select(r => r.Name).Contains("Admin"))
                {
                    await roleManager.CreateAsync(new Role() {Name = "Admin"});
                    context.SaveChanges();
                }

                if (!userManager.Users.Select(u => u.UserName).Contains("Admin"))
                {
                    var user = new ApplicationUser { UserName = "Admin", Fullname = "Admin"};
                    var result = await userManager.CreateAsync(user, "123456");
                    context.SaveChanges();
                    await userManager.AddToRoleAsync(user, "Admin");
                    context.SaveChanges();
                }
                if (!roleManager.Roles.Select(r => r.Name).Contains("User"))
                {
                    await roleManager.CreateAsync(new Role() { Name = "User" });
                    context.SaveChanges();
                }

                
            }


        }
    }
}
