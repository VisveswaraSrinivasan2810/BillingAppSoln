using BillApp.Models.DTO;
using BillApp.Models.Models;
using BillApp.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IGenericRepository<Invoice> repository;
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceController(IGenericRepository<Invoice> repository , IInvoiceRepository invoiceRepository)
        {
            this.repository = repository;
            this.invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAll()
        {
            var res = await repository.GetAllAsync();
            if (res == null)
            {
                return NotFound("Invoice List Cannot be Empty");
            }
            return Ok(res);
        }

        [HttpGet("id")]
        public async Task<ActionResult<Invoice>> Get(int id)
        {
            var res = await repository.GetAsync(id);
            if(res == null)
            {
                return NotFound("Invoice cannot be found");
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceDTO invoiceDTO)
        {
            if (invoiceDTO == null)
            {
                return BadRequest("Inputs cannot be null");
            }
            var invoice = new Invoice()
            {
                InvoiceDate = DateTime.Now,
                TotalPrice = invoiceDTO.TotalPrice,
                CustomerId = invoiceDTO.CustomerId
            };
            await repository.CreateAsync(invoice);
            return CreatedAtAction("Get", new { id = invoiceDTO.InvoiceId }, invoiceDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InvoiceDTO invoiceDTO)
        {
            if(invoiceDTO == null || invoiceDTO.InvoiceId != id)
            {
                return BadRequest("Input cannot be null");
            }
            var invoice =await repository.GetAsync(id);
            if(invoice == null)
            {
                return NotFound("Invoice cannot be found");
            }
            invoice.InvoiceDate = DateTime.Now;
            invoice.CustomerId = invoiceDTO.CustomerId;
            invoice.TotalPrice = invoiceDTO.TotalPrice;
            await invoiceRepository.UpdateAsync(invoice);    
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await repository.GetAsync(id);
            if(invoice == null)
            {
                return NotFound("Invoice cannot be found");
            }
            await repository.DeleteAsync(invoice);
            return NoContent();
        }
    }
}
