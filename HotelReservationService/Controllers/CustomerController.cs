using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerService customerService;
        public  CustomerController(CustomerService customerService)
        {
            this.customerService = customerService;
        }
        [HttpPost("add-customer")]
        public IActionResult AddCustomer([FromBody]CustomerVM customerVM)
        {
            var cust = customerService.AddCustomer(customerVM);
            return Ok(cust);
        }
        [HttpGet("get/all")]
        public IActionResult GetAllCustomers()
        {
            var customers = customerService.GetAllCustomers();
            if(!customers.Any()) // If empty
            {
                return NotFound();
            }
            return Ok(customers);
        }
        [HttpGet("get/{id}")]
        public IActionResult GetCustomers(int id) 
        {
            var customer = customerService.GetCustomerByID(id);
            if(customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

        [HttpGet("get-customer")]
        public IActionResult GetCustomerWithMail(string email)
        {
            var customer = customerService.GetCustomerByMail(email);
            if(customer != null)
                return Ok(customer);
            return NotFound();
        }
        [HttpPut("put/{id}")]
        public IActionResult UpdateCustomerByID(int id, [FromBody]CustomerVM customerVM)
        {
            var updatedCustomer = customerService.UpdateCustoemrByID(id, customerVM);
            return Ok(updatedCustomer);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCustomerByID(int id)
        {
            customerService.DeleteCustomerByID(id);
            return Ok();
        }
    }
}
