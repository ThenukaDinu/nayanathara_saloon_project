using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saloonAPI.Models;
using saloonAPI.Services;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _sqlService;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository dataAccessRepository, IMapper mapper)
        {
            _sqlService = dataAccessRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<ProductDto>> GetAllProducts()
        {
            List<Product> products = _sqlService.GetAllProducts();
            var vmProducts = _mapper.Map<List<ProductDto>>(products);
            return Ok(vmProducts);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult<Product> AddNewProduct(AddProduct product)
        {
            if (!User.Identity.IsAuthenticated)
                throw new AuthenticationException();
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId");
            product.CreatedUserId = userId.Value;

            var mappedProduct = _mapper.Map<Product>(product);

            Product newProduct = _sqlService.SaveProduct(mappedProduct);

            var ProductToReturn = _mapper.Map<ProductDto>(newProduct);

            return Created("Product", ProductToReturn);
        }

        [HttpPut("{productId}"), Authorize(Roles = "Admin")]
        public ActionResult UpdateProduct(int productId, EditProduct product)
        {
            Product updatingProduct = _sqlService.GetProduct(productId);

            if (updatingProduct is null)
            {
                return NotFound();
            }

            _mapper.Map(product, updatingProduct);

            _sqlService.UpdateProduct(updatingProduct);

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            Product deletingProduct = _sqlService.GetProduct(productId);

            if (deletingProduct is null)
            {
                return NotFound();
            }

            _sqlService.DeleteProduct(deletingProduct);

            return NoContent();
        }
    }
}
