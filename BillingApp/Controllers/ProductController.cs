using BillApp.Models.DTO;
using BillApp.Models.Models;
using BillApp.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<Product> _repository;
        public ProductController(IGenericRepository<Product> repository,IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var res =await  _repository.GetAllAsync();
            if(res == null)
            {
                return NotFound("Product List is Empty");
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var res = await _repository.GetAsync(id);
            if(res == null)
            {
                return NotFound("Product not found");
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if(productDTO == null)
            {
                return BadRequest("Input cannot be null");
            }
            var product = new Product()
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                StockQuantity = productDTO.StockQuantity
            };
            await _repository.CreateAsync(product);
            return CreatedAtAction("Get",new {id=productDTO.ProductId},productDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] ProductDTO productDTO)
        {
            if (productDTO == null || productDTO.ProductId != id)
            {
                return BadRequest("Input cannot be null");
            }
            var product = await _repository.GetAsync(id);
            if(product == null)
            {
                return NotFound("Product not found");
            }
            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.StockQuantity = productDTO.StockQuantity;
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            await _productRepository.DeleteAsync(product);
            return NoContent();
        }
    }
}
