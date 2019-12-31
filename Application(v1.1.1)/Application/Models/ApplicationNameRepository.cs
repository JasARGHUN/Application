using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Application.Models
{
    public static class ApplicationNameRepository
    {
        public static void SeedData(IApplicationBuilder app)
        {
            ApplicationDBContext context = app.ApplicationServices.GetRequiredService<ApplicationDBContext>();
            context.Database.Migrate();
            if (!context.Infos.Any())
            {
                context.Infos.Add(
                    new Info
                    {
                        AppName = "Application Name",
                        AppAddress = "Custom address 142/52",
                        AppPhone1 = "8 (700) 100 10 10",
                        AppPhone2 = "8 (702) 200 20 20",
                        AppSocialAddress = "https://web.facebook.com/",
                        AppEmail = "Application@email.com"
                    });
                context.SaveChanges();
            }
        }
    }
}
