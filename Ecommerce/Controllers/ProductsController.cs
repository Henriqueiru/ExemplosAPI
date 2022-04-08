using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ProductServices _productServices;

    public ProductsController(ProductServices productServices)
    {
      _productServices = productServices;
    }

    [HttpGet]
    public async Task<List<Product>> GetProducts()
    => await _productServices.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(string id)
    {
      var product = await _productServices.GetAsync(id);

      if (product is null)
      {
        return NotFound();
      }
      return product;
    }

    [HttpPost]
    public async Task<Product> PostProduct(Product product)
    {
      await _productServices.CreateAsync(product);

      return product;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(string id, Product updateProduct)
    {
      var product = await _productServices.GetAsync(id);

      if (product is null)
      {
        return NotFound();
      }

      updateProduct.Id = product.Id;

      await _productServices.UpdateAsync(id, updateProduct);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      var product = await _productServices.GetAsync(id);

      if (product is null)
      {
        return NotFound();
      }

      await _productServices.RemoveAsync(id);

      return NoContent();
    }
  }
}