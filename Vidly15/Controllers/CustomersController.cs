using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using Vidly15.Models;
using Vidly15.ViewModels;

namespace Vidly15.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes;

            var ViewModel = new CustomerFormViewModel
            {
                Customer = new Customer(), //initializes the customer properties to default values.
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer) //or CustomerFormViewModel viewModel will also work
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", ViewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer); // changes in memory and not yet commited to DB
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            _context.SaveChanges(); //changes commited to DB in one transaction.

            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers
        public ActionResult Index()
        {
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customers);
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var ViewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", ViewModel);
        }

        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "John Smith" },
        //        new Customer { Id = 2, Name = "Mary Williams" }
        //    };

        //}
    }


}