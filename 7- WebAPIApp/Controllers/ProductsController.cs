using System.Formats.Asn1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApp.DTO;
using WebApiApp.Models;

namespace WebApiApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    readonly ProductsContext _context;

    public ProductsController(ProductsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products.Where(p => p.IsActive).Select(p => new ProductDTO { Id = p.Id, Name = p.Name, Price = p.Price }).ToListAsync();
        if (products == null) return NotFound();
        return Ok(products);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int? id)
    {
        if (id == null) return StatusCode(404, "The content you searching couldn't found");

        var prd = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (prd == null) return NotFound();

        return Ok(new ProductDTO { Id = prd.Id, Name = prd.Name, Price = prd.Price });
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductDTO entity)
    {
        _context.Products.Add(new Product { Name = entity.Name, IsActive = true, Price = entity.Price });
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDTO entity)
    {
        if (id != entity.Id) return BadRequest();
        var pr = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (pr == null)
        {
            return NotFound();
        }

        pr.Name = entity.Name;
        pr.Price = entity.Price;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        if (id == null) return NotFound();

        var pr = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (pr == null) return NotFound();

        _context.Products.Remove(pr);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}