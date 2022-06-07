using GeneralStoreMVC.Data;
using GeneralStoreMVC.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly GeneralStoreDbContext _ctx;

        public CustomerController(GeneralStoreDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            var customers = _ctx.Customers.Select(customer => new CustomerIndexModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,

            });
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "ModelState is Invalid";
                return View(model);
            }
            _ctx.Customers.Add(new Customer
            {
                Name = model.Name,
                Email = model.Email,
            });
            if (_ctx.SaveChanges() == 1)
            {
                return Redirect("/Customer");
            }
            TempData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);
        }

        // GET: customer/details/{id}
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var customer = _ctx.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerDetailModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = _ctx.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerDetailModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit (int id, CustomerEditModel model)
        {
            var customer = _ctx.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Name = model.Name;
            customer.Email = model.Email;

            if (_ctx.SaveChanges() == 1)
            {
                return Redirect("/customer");
            }

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);

        }
    }
}
