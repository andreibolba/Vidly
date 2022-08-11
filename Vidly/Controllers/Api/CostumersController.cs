using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;
using Vidly.Models.IdentityModels;

namespace Vidly.Controllers.Api
{
    public class CostumersController : ApiController
    {
        public ApplicationDbContext _context;

        public CostumersController()
        {
            _context = new ApplicationDbContext();
        }

        //Get/api/costumers
        public IHttpActionResult GetCostumers(string query = null)
        {
            var costumerQuey = _context.Customers
                .Include(c => c.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
                costumerQuey = costumerQuey.Where(c => c.Name.Contains(query));

            var costumerDtos= costumerQuey
                .ToList()
                .Select(Mapper.Map<Models.Costumer,CostumerDto>);
            return Ok(costumerDtos);
        }
        //Get/api/costumers/1
        public IHttpActionResult GetCostumer(int id)
        {
            var costumer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (costumer == null)
                return NotFound();
            return Ok(Mapper.Map<Models.Costumer,CostumerDto>(costumer));
        }
        //POST/api/costumers
        [HttpPost]
        public IHttpActionResult CreateCostumer(CostumerDto costumerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var costumer = Mapper.Map<CostumerDto, Models.Costumer>(costumerDto);
            _context.Customers.Add(costumer);
            _context.SaveChanges();

            costumerDto.Id = costumer.Id;

            return Created(new Uri(Request.RequestUri+"/"+costumer.Id),costumerDto);
        }
        //PUT/api/customers/1
        [HttpPut]
        public void UpdateCostumerUpdateCostumer(int id,CostumerDto costumerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var costumerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (costumerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map<CostumerDto, Models.Costumer>(costumerDto, costumerInDb);

            _context.SaveChanges();

        }

        //DELETE/api/costumers/1
        [HttpDelete]
        public void DeleteCostumer(int id)
        {
            var costumerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (costumerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(costumerInDb);
            _context.SaveChanges();
        }
    }
}
