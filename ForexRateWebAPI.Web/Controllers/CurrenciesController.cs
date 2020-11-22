using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ForexRateWebAPI.Web.Models;
using ForexRateWebAPI.Web.Models.Interface;
using ForexRateWebAPI.Web.Models.Repository;
using System.Data.Entity.Infrastructure;

namespace ForexRateWebAPI.Web.Controllers
{
    
    [RoutePrefix("api/Currencies")]
    public class CurrenciesController : ApiController
    {
        private IRepository<Currency> repo;

        public CurrenciesController()
        {
            this.repo = new GenericRepository<Currency>();
        }

        
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(this.repo.GetAll());
        }
        
        [HttpGet]
        [ResponseType(typeof(Currency))]
        [Route("{ccy}")]
        public Currency Get(string ccy)
        {
            Currency currency = this.repo.Get(ccy);
            return currency;
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(Currency currency)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                this.repo.Create(currency);
            }
            catch (DbUpdateException)
            {
                if (this.repo.Exists(currency.Ccy))
                    return Conflict();
                else
                    throw;
            }
            
            return Ok("Added");
        }

        [HttpDelete]
        [Route("{ccy}")]
        [ResponseType(typeof(Currency))]
        public IHttpActionResult Delete(string ccy)
        {
            Currency currency = repo.Get(ccy);
            if (currency == null)
                return NotFound();

            repo.Delete(currency);
            return Ok("Deleted.");
        }

        [HttpPost]
        [ResponseType(typeof(ForexRate))]
        [Route("Update")]
        public IHttpActionResult Update(Currency currency)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                repo.Update(currency);
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok("Updated.");
        }

    }
}