using Microsoft.AspNetCore.Mvc;
using BlazorCrudApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorCrudApp.Server.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private static readonly object _lock = new(); 
        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 9.99m },
            new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 19.99m }
        };

        /// <summary>
        /// Gets all products.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            try
            {
                if (Products == null || !Products.Any())
                {
                    return NotFound(new { Message = "No products found" });
                }
                return Ok(Products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }


        /// <summary>
        /// Gets a single product by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new { Message = "Product not found" });
            }
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (product == null || string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
            {
                return BadRequest(new { Message = "Invalid product data" });
            }

            lock (_lock)
            {
                product.Id = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
                Products.Add(product);
            }

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null || string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
            {
                return BadRequest(new { Message = "Invalid product data" });
            }

            Product existingProduct;

            lock (_lock)
            {
                existingProduct = Products.FirstOrDefault(p => p.Id == id);
                if (existingProduct == null)
                {
                    return NotFound(new { Message = "Product not found" });
                }

                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
            }

            return Ok(existingProduct);
        }


        /// <summary>
        /// Deletes a product.
        /// </summary>
        [HttpDelete("{id:int}")]
        public ActionResult DeleteProduct(int id)
        {
            lock (_lock)
            {
                var product = Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(new { Message = "Product not found" });
                }

                Products.Remove(product);
            }

            return Ok(new { Message = "Product deleted successfully" });
        }
    }
}
