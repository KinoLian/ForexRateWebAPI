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
    [RoutePrefix("api/ForexRates")]
    public class ForexRatesController : ApiController
    {

        private IRepository<ForexRate> repo;

        public ForexRatesController()
        {
            this.repo = new GenericRepository<ForexRate>();
        }

        [HttpGet]
        public IQueryable<ForexRate> GetForexRate()
        {
            return repo.GetAll();
        }

        [HttpGet]
        [ResponseType(typeof(ForexRate))]
        [Route("{Date}/{Ccy}")]
        public IHttpActionResult GetForexRate(DateTime date, string ccy)
        {
            ForexRate forexRate = repo.Get(date, ccy);
            if (forexRate == null) return NotFound();
            return Ok(forexRate);
        }

        [ResponseType(typeof(ForexRate))]
        [Route("Create")]
        public IHttpActionResult CreateForexRate(ForexRate forexRate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                repo.Create(forexRate);
            }
            catch (DbUpdateException)
            {
                if (repo.Exists(forexRate.Date, forexRate.Ccy))
                    return Conflict();
                else
                    throw;
            }

            return Ok(forexRate.Date + forexRate.Ccy + "Add Success.");
        }

        [HttpDelete]
        [Route("{Date}/{Ccy}")]
        [ResponseType(typeof(ForexRate))]
        public IHttpActionResult DeleteForexRate(DateTime date, string ccy)
        {
            ForexRate forexRate = repo.Get(date, ccy);
            if (forexRate == null)
                return NotFound();

            repo.Delete(forexRate);
            return Ok("Delete Success.");
        }


        [HttpPost]
        [ResponseType(typeof(ForexRate))]
        [Route("Update")]
        public IHttpActionResult UpdateForexRate(ForexRate forexRate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                repo.Update(forexRate);
            }
            catch (DbUpdateException)
            {
               throw;
            }

            return Ok(forexRate.Date + forexRate.Ccy + "Update Success.");
        }

    }
}