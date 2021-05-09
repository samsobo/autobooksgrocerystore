using GroceryStoreBL.Infrastructure.Interfaces;
using GroceryStoreBL.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Controllers
{
    /// <summary>
    /// Customer Controller
    /// </summary>
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Customer Service
        /// </summary>
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>List of Customers</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var customers = _customerService.List();
            if (!customers.Any())
                return NotFound();
            return Ok(customers);
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        /// <summary>
        /// Adds a new customer
        /// </summary>
        /// <param name="entity">Customer</param>
        /// <returns>Customer</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)]
        public IActionResult Post(Customer entity)
        {
            entity = _customerService.Add(entity);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>200 or 404</returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Put(Customer customer)
        {
            var entity = _customerService.GetById(customer.Id);
            if (entity == null)
                return NotFound();

            _customerService.Update(customer);
            return Ok();
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>204 or 404</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var entity = _customerService.GetById(id);
            if (entity == null)
                return NotFound();
            _customerService.Delete(entity.Id);
            return NoContent();
        }
    }
}
