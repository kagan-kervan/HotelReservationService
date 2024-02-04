using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using Microsoft.CodeAnalysis;
using System.Net.Http;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HotelReservationService.Data.Identity;

namespace HotelReservationService.Services
{
    public class CustomerService
    {
        AppDBContext dbContext;
        TableRelationService tableRelationService;
        private readonly UserManager<ApplicationCustomer> userManager;
        public CustomerService(AppDBContext dbContext, TableRelationService tableRelationService, UserManager<ApplicationCustomer> userManager)
        {
            this.dbContext = dbContext;
            this.tableRelationService = tableRelationService;
            this.userManager = userManager;
        }

        public async Task<Customer> AddCustomerAsync(CustomerVM customer)
        {
            string display = customer.Name + " " + customer.Surname;
            var newUser = new ApplicationCustomer
            {
                DisplayName = display,
                Email = customer.Email_Address,
                UserName = customer.Email_Address,
                PhoneNumber = customer.Phone,
            };

            var result = await userManager.CreateAsync(newUser, customer.Password);

            if (result.Succeeded)
            {
                var newCustomer = new Customer
                {
                    Name = customer.Name,
                    Surname = customer.Surname,
                    Email_Address = customer.Email_Address,
                    Phone = customer.Phone,
                    Password = customer.Password,
                    ApplicationCustomer = newUser,
                };

                dbContext.Add(newCustomer);
                dbContext.SaveChanges();

                return newCustomer;
            }
            else
            {
                // Handle the case where user creation failed
                // ...

                return null;
            }
        }
        public ICollection<Customer> GetAllCustomers()
        {
            var AllCustomers = dbContext.Customers.Include(o => o.ApplicationCustomer).ToList();
            return AllCustomers;
        }

        public Customer GetCustomerByID(int id) 
        {
            //Change because if cant find returns null instead of excaption.
            return dbContext.Customers.Include(o => o.ApplicationCustomer).FirstOrDefault(x => x.Id == id);
        }
        public Customer UpdateCustoemrByID(int id, CustomerVM updatedCustomer)
        {
           var customer = dbContext.Customers.Include(o => o.ApplicationCustomer).FirstOrDefault(n => n.Id == id);
            if(customer != null)
            {
                customer.Name = updatedCustomer.Name;
                customer.Surname = updatedCustomer.Surname;
                customer.Email_Address = updatedCustomer.Email_Address;
                customer.Password = updatedCustomer.Password;
                customer.Phone = updatedCustomer.Phone;
                dbContext.SaveChanges();
            }
            return customer;
        }
        public void DeleteCustomerByID(int id)
        {
            var customer = dbContext.Customers.FirstOrDefault(n => n.Id == id);
            if( customer != null )
            {
                //Checks if the customer has any active reservations.
                if (tableRelationService.DoesCustomerHaveAnyReservations(id))
                {
                    //Deletes all reservations that are attached to customer.
                    tableRelationService.DeleteReservationWithCustomerID(customer.Id);
                }
                dbContext.Customers.Remove(customer);
                dbContext.SaveChanges() ;
            }
        }

        public Customer GetCustomerByMail(string mail)
        {
            IQueryable<Customer> customers = dbContext.Customers;
            customers = customers.Where(x => x.Email_Address == mail);
            return customers.Include(o => o.ApplicationCustomer).SingleOrDefault();
        }
    }
}
