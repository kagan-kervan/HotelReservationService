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
            customerService.AddCustomer(customerVM);
            return Ok();
        }
        [HttpGet("get/all")]
        public IActionResult GetAllCustomers()
        {
            var customers = customerService.GetAllCustomers();
            return Ok(customers);
        }
        [HttpGet("get/{id}")]
        public IActionResult GetCustomers(int id) 
        {
            var customer = customerService.GetCustomerByID(id);
            return Ok(customer);
        }

        [HttpGet("get-customer")]
        public IActionResult GetCustomerWithMail(string email)
        {
            var customer = customerService.GetCustomerByMail(email);
            return Ok(customer);
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
