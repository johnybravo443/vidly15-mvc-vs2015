using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly15.Dto;
using Vidly15.Models;
using System.Data.Entity;

namespace Vidly15.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/customers
        public IHttpActionResult GetCustomers(string query = null) //IEnumerable<CustomerDto>
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET /api/customers/1
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/customers
        //Convention : when we generate a resource, we return a generated resource as it will have an id or some unique key property.
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/customers/1
        //we can return either a customer or void
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return Ok();
        }

        //PUT /api/customers/1
        //we can return either a customer or void
        //[HttpPut]
        //public void UpdateCustomer(int id, CustomerDto customerDto)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customerInDb == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    Mapper.Map(customerDto, customerInDb);

        //    _context.SaveChanges();
        //}

        //DELETE /api/customers/1
        //[HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)  //void  IHttpActionResult
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/customers/1   IHttpActionResult
        //[HttpDelete]
        //public void DeleteCustomer(int id)  //void  IHttpActionResult
        //{
        //    var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customerInDb == null)
        //        //return NotFound();
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    _context.Customers.Remove(customerInDb);
        //    _context.SaveChanges();
        //}
    }
}
