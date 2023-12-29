using HotelReservationService.Data.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationService
{
    public class AppDBInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();

                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(new Customer()
                    {
                        Name = "Ahmet",
                        Surname = "Test",
                        Email_Address = "Test@mail.com",
                        Password = "Password",
                        Phone = "55544447897"
                    },
                    new Customer()
                    {
                        Name = "Ahmetto",
                        Surname = "Testto",
                        Email_Address = "ttoTest@mail.com",
                        Password = "Pattssword",
                        Phone = "45544447897"
                    }
                    );
                    context.SaveChanges();
                }
                if(!context.Owners.Any())
                {
                    context.Owners.AddRange(new Owner()
                    {
                        Name = "Ahmet",
                        Surname = "OwnerTest",
                        Email_Address = "Test@owner.com",
                        Password = "Password",
                        Phone = "55544447897"
                    }, new Owner()
                    {
                        Name = "Mehmet",
                        Surname = "OwnerTest",
                        Email_Address = "testto@owner.com",
                        Password = "Passo",
                        Phone = "55544457897"
                    });
                    context.SaveChanges();
                }
                context.SaveChanges(); 
            }
        }
    }
}
