using AutoMapper;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.DataAccess;
using Vidly.DataAccessLayer;
using Autofac;

namespace Vidly.Controllers.Api
{
    public class CostumersController : ApiController
    {
        private ConfigAutofac builder;

        public CostumersController()
        {
            builder = new ConfigAutofac();
        }
        //Get/api/costumers
        public IHttpActionResult GetCostumers(string query = null)
        {
            using (var x = builder.builder.Build())
            {
                var costumerQuery = x.Resolve<EntityFrameworkCostumerProvider>().GetCostumersApi();

                if (!string.IsNullOrWhiteSpace(query))
                    costumerQuery = costumerQuery.Where(c => c.Name.Contains(query));

                var costumerDtos = costumerQuery
                    .ToList()
                    .Select(Mapper.Map<Models.Costumer, CostumerDto>);
                return Ok(costumerDtos);
            }
        }
        //Get/api/costumers/1
        public IHttpActionResult GetCostumer(int id)
        {
            using (var c = builder.builder.Build())
            {
                var costumer = c.Resolve<EntityFrameworkCostumerProvider>().GetCostumer(id);

                if (costumer == null)
                    return NotFound();
                return Ok(Mapper.Map<Models.Costumer, CostumerDto>(costumer));
            }
        }
        //POST/api/costumers
        [HttpPost]
        public IHttpActionResult CreateCostumer(CostumerDto costumerDto)
        {
            using (var c = builder.builder.Build())
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var costumer = Mapper.Map<CostumerDto, Models.Costumer>(costumerDto);
                c.Resolve<EntityFrameworkCostumerProvider>().AddCostumer(costumer);

                costumerDto.Id = costumer.Id;

                return Created(new Uri(Request.RequestUri + "/" + costumer.Id), costumerDto);
            }
        }
        //PUT/api/customers/1
        [HttpPut]
        public void UpdateCostumer(int id,CostumerDto costumerDto)
        {
            using (var c = builder.builder.Build())
            {
                if (!ModelState.IsValid)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                var costumerInDb = c.Resolve<EntityFrameworkCostumerProvider>().GetCostumer(id);
                if (costumerInDb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                Mapper.Map<CostumerDto, Models.Costumer>(costumerDto, costumerInDb);
            }


        }

        //DELETE/api/costumers/1
        [HttpDelete]
        public void DeleteCostumer(int id)
        {
            using (var c = builder.builder.Build()) {
                var costumerInDb = c.Resolve<EntityFrameworkCostumerProvider>().GetCostumer(id);
                if (costumerInDb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                c.Resolve<EntityFrameworkCostumerProvider>().DeleteCostumers(costumerInDb);
            }
        }
    }
}
