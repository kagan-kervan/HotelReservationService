using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using System.Net.Mail;

namespace HotelReservationService.Services
{
    public class CustomerService
    {
        AppDBContext dbContext;
        public CustomerService(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddCustomer(CustomerVM customer)
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
                dbContext.Customers.Remove(customer);
                dbContext.SaveChanges() ;
            }
        }
    }
}
