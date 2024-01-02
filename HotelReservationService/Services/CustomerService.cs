using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using Microsoft.CodeAnalysis;
using System.Net.Http;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationService.Services
{
    public class CustomerService
    {
        AppDBContext dbContext;
        TableRelationService tableRelationService;
        public CustomerService(AppDBContext dbContext, TableRelationService tableRelationService)
        {
            this.dbContext = dbContext;
            this.tableRelationService = tableRelationService;
        }

        public Customer AddCustomer(CustomerVM customer)
        {
            var newCustomer = new Customer()
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Email_Address = customer.Email_Address,
                Password = customer.Password,
                Phone = customer.Phone,
            };
            dbContext.Add(newCustomer);
            dbContext.SaveChanges();
            return newCustomer;
        }

        public ICollection<Customer> GetAllCustomers()
        {
            var AllCustomers = dbContext.Customers.ToList();
            return AllCustomers;
        }

        public Customer GetCustomerByID(int id) 
        {
            //Change because if cant find returns null instead of excaption.
            return dbContext.Customers.FirstOrDefault(x => x.Id == id);
        }
        public Customer UpdateCustoemrByID(int id, CustomerVM updatedCustomer)
        {
           var customer = dbContext.Customers.FirstOrDefault(n => n.Id == id);
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
            return customers.SingleOrDefault();
        }
    }
}
