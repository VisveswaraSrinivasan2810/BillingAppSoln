using BillApp.Models.DTO;
using BillApp.Models.Models;
using BillApp.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(IGenericRepository<Customer> repository, ICustomerRepository customerRepository)
        {
            _repository = repository;
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var res = await _repository.GetAllAsync();
            if(res==null)
            {
                return NotFound("Customer List Is Empty");
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _repository.GetAsync(id);  
            if(customer == null)
            {
                return NotFound("Customer is not found in the Database");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDTO customerDTO)
        {
            if(customerDTO == null)
            {
                return BadRequest("Customer Details Cannot be Null Here");
            }
            var customer = new Customer()
            {
                Name = customerDTO.Name,
                Phone = customerDTO.Phone
            };

            await _repository.CreateAsync(customer);
            return CreatedAtAction("Get", new { id = customerDTO.CustomerId }, customerDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDTO customerDTO)
        {
            if(customerDTO == null || customerDTO.CustomerId != id)
            {
                return BadRequest();
            }
            var customer = await _repository.GetAsync(id);
            if(customer == null)
            {
                return NotFound("Customer is not found in the Database");
            }
            customer.Name = customerDTO.Name;
            customer.Phone = customerDTO.Phone;
            await _customerRepository.Update(customer);   
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _repository.GetAsync(id);
            if(customer == null)
            {
                return NotFound("Customer is not found in the Database");
            }
            await _repository.DeleteAsync(customer);
            return NoContent();
        }

    }
}
