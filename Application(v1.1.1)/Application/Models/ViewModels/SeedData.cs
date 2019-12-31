using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Application.Models.ViewModels
{
    public static class SeedData
    {
        public static void EnsureData(IApplicationBuilder app)
        {
            ApplicationDBContext context = app.ApplicationServices.GetRequiredService<ApplicationDBContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Product 1",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 1",
                        Price = 55
                    },
                    new Product
                    {
                        Name = "Product 2",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 1",
                        Price = 25
                    },
                    new Product
                    {
                        Name = "Product 3",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 1",
                        Price = 5
                    },

                    new Product
                    {
                        Name = "Product 4",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 2",
                        Price = 12
                    },
                    new Product
                    {
                        Name = "Product 5",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 2",
                        Price = 7
                    },
                    new Product
                    {
                        Name = "Product 6",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 2",
                        Price = 92
                    },

                    new Product
                    {
                        Name = "Product 7",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 3",
                        Price = 156
                    },
                    new Product
                    {
                        Name = "Product 8",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 3",
                        Price = 32
                    },
                    new Product
                    {
                        Name = "Product 9",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 3",
                        Price = 4
                    },

                    new Product
                    {
                        Name = "Product 10",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 4",
                        Price = 2
                    },
                    new Product
                    {
                        Name = "Product 11",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 4",
                        Price = 17
                    },
                    new Product
                    {
                        Name = "Product 12",
                        ShortDescription = "Short description",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Category = "Cat - 4",
                        Price = 65
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
