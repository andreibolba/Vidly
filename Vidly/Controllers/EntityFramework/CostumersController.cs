using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
using Vidly.Models.IdentityModels;
using System.Runtime.Caching;

namespace Vidly.Controllers.EntityFramework
{
    public class CostumersController : Controller
    {
        private ApplicationDbContext _context;

        public CostumersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult AllCostumers()
        {
            return View();
        }

        [Route("Costumers/GetCostumer/{id}")]
        public ActionResult GetCostumer(int id)
        {
            var allCostumers = _context.Customers.Include(c => c.MembershipType).ToList();
            var costumer = allCostumers.SingleOrDefault(c => c.Id == id);

            if (costumer == null)
                return HttpNotFound();

            return View(costumer);
        }

        // GET: Costumers
        public ActionResult Index()
        {
            if (MemoryCache.Default["Genres"] == null)
            {
                MemoryCache.Default["Genres"] = _context.Genres.ToList();
            }
            var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new CostumerFormViewModel()
            {
                MembershipTypes = _context.MembershipTypes.ToList(),
                Costumer = new Models.EntityFramework.Costumer()
            };
            return View("CostumerForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Models.EntityFramework.Costumer costumer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CostumerFormViewModel()
                {
                    MembershipTypes = _context.MembershipTypes.ToList(),
                    Costumer = costumer
                };
                return View("CostumerForm", viewModel);
            }
            if (costumer.Id==0)
            _context.Customers.Add(costumer);
            else
            {
                var costumerInDB = _context.Customers.Single(c => c.Id == costumer.Id);

                //Mapper.Map(costumer,costumerInDb)

                costumerInDB.Name = costumer.Name;
                costumerInDB.BirthDate = costumer.BirthDate;
                costumerInDB.MembershipTypeId = costumer.MembershipTypeId;
                costumerInDB.IsSubscribedToNewsletter = costumer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("AllCostumers", "Costumers");
        }

        public ActionResult Edit(int id)
        {
            var costumer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (costumer == null)
                costumer = new Models.EntityFramework.Costumer();
            var viewModel = new CostumerFormViewModel()
            {
                MembershipTypes = _context.MembershipTypes.ToList(),
                Costumer = costumer
            };
            return View("CostumerForm",viewModel);
        }
    }
}