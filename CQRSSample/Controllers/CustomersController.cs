using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSample.Models.SQLite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerSQLiteRepository _sqliteRepository;
        public CustomersController(CustomerSQLiteRepository sqliteRepository)
        {
            _sqliteRepository = sqliteRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _sqliteRepository.GetAll();
            if (customers == null)
            {
                return NotFound();
            }
            return new ObjectResult(customers);
        }
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(long id)
        {
            var customer = _sqliteRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }
        [HttpPost]
        public IActionResult Post([FromBody] CustomerRecord customer)
        {
            CustomerRecord created = _sqliteRepository.Create(customer);
            return CreatedAtRoute("GetCustomer", new { id = created.Id }, created);
        }
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] CustomerRecord customer)
        {
            var record = _sqliteRepository.GetById(id);
            if (record == null)
            {
                return NotFound();
            }
            customer.Id = id;
            _sqliteRepository.Update(customer);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var record = _sqliteRepository.GetById(id);
            if (record == null)
            {
                return NotFound();
            }
            _sqliteRepository.Remove(id);
            return NoContent();
        }
    }
}