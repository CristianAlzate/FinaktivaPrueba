using Finaktiva.Repository;
using Finaktiva.Repository.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finaktiva.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                using (var context = services.GetRequiredService<Context>())
                {
                    context.Database.EnsureCreated();
                    if (!context.Roles.Any()) {
                        var adminRole = new Role() { Description = "Admin" };
                        var operativeRole = new Role() { Description = "Operative" };
                        var adminUser = new User()
                        {
                            Name = "calzate",
                            Password = "MQAyADMA",
                            Active = true,
                            Role = adminRole
                        };
                        var operatorUser = new User()
                        {
                            Name = "mencho",
                            Password = "MQAyADMA",
                            Active = true,
                            Role = operativeRole
                        };
                        context.Roles.Add(adminRole);
                        context.Roles.Add(operativeRole);
                        context.Users.Add(adminUser);
                        context.Users.Add(operatorUser);
                        context.SaveChanges();
                    }

                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
