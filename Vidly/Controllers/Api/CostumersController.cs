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
using Vidly.DataAccess;

namespace Vidly.Controllers.Api
{
    public class CostumersController : ApiController
    {
        private EntityFrameworkCostumerProvider providerCostumers;

        public CostumersController()
        {
            providerCostumers = new EntityFrameworkCostumerProvider();
        }

        protected override void Dispose(bool disposing)
        {
            providerCostumers.Dispose();
        }

        //Get/api/costumers
        public IHttpActionResult GetCostumers(string query = null)
        {
            var costumerQuery = providerCostumers.GetCostumersApi();

            if (!string.IsNullOrWhiteSpace(query))
                costumerQuery = costumerQuery.Where(c => c.Name.Contains(query));

            var costumerDtos= costumerQuery
                .ToList()
                .Select(Mapper.Map<Models.Costumer,CostumerDto>);
            return Ok(costumerDtos);
        }
        //Get/api/costumers/1
        public IHttpActionResult GetCostumer(int id)
        {
            var costumer = providerCostumers.GetCostumer(id);

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
            providerCostumers.AddCostumer(costumer);

            costumerDto.Id = costumer.Id;

            return Created(new Uri(Request.RequestUri+"/"+costumer.Id),costumerDto);
        }
        //PUT/api/customers/1
        [HttpPut]
        public void UpdateCostumerUpdateCostumer(int id,CostumerDto costumerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var costumerInDb = providerCostumers.GetCostumer(id);
            if (costumerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map<CostumerDto, Models.Costumer>(costumerDto, costumerInDb);


        }

        //DELETE/api/costumers/1
        [HttpDelete]
        public void DeleteCostumer(int id)
        {
            var costumerInDb = providerCostumers.GetCostumer(id);
            if (costumerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            providerCostumers.DeleteCostumers(costumerInDb);
        }
    }
}
