using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Backend;

namespace Backend.Controllers
{
    public class OredersController : ApiController
    {
        private backendEntities db = new backendEntities();

        // GET: api/Oreders
        public IQueryable<Oreder> GetOreder()
        {
            return db.Oreder;
        }

        // GET: api/Oreders/5
        [ResponseType(typeof(Oreder))]
        public IHttpActionResult GetOreder(long id)
        {
            Oreder oreder = db.Oreder.Find(id);
            if (oreder == null)
            {
                return NotFound();
            }

            return Ok(oreder);
        }

        //GET: api/Oreders/Search/Price/13,00
        [ResponseType(typeof(Oreder))]
        [Route("api/Oreders/Search/Price/{price:decimal}")]
        public IHttpActionResult GetOrederSearchByPrice(decimal price)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var oreder = entities.Oreder.Where(t => t.Price == price).Include(t => t.Client).Include(t => t.Status).ToList();
                if (oreder == null)
                {
                    return NotFound();
                }
                return Ok(oreder);
            }
        }

        //GET: api/Oreders/Search/Date/1999-11-26 00:00:00.000
        [ResponseType(typeof(Oreder))]
        [Route("api/Oreders/Search/Date/{date:DateTime}")]
        public IHttpActionResult GetOrederSearchByDate(DateTime date)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var oreder = entities.Oreder.Where(t => t.Date == date).Include(t => t.Client).Include(t => t.Status).ToList();
                if (oreder == null)
                {
                    return NotFound();
                }
                return Ok(oreder);
            }
        }

        //GET: api/Oreders/Search/Client_ClietnId/3
        [ResponseType(typeof(Oreder))]
        [Route("api/Oreders/Search/Client_ClientId/{id:long}")]
        public IHttpActionResult GetOrederSearchByClient_ClientId(long id)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var oreder = entities.Oreder.Where(t => t.Client_ClientId == id).Include(t => t.Client).Include(t => t.Status).ToList();
                if (oreder == null)
                {
                    return NotFound();
                }
                return Ok(oreder);
            }
        }

        //GET: api/Oreders/Search/Status/Done
        [ResponseType(typeof(Oreder))]
        [Route("api/Oreders/Search/Status/{status}")]
        public IHttpActionResult GetOrederSearchByStatus(string status)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                switch (status)
                {
                    case "Done":
                        var oreder1 = entities.Oreder.Where(t => t.Status_StatusID == 1).Include(t => t.Client).Include(t => t.Status).ToList();
                        if (oreder1 == null)
                        {
                            return NotFound();
                        }
                        return Ok(oreder1);
                    case "Cancelled":
                        var oreder2 = entities.Oreder.Where(t => t.Status_StatusID == 2).Include(t => t.Client).Include(t => t.Status).ToList();
                        if (oreder2 == null)
                        {
                            return NotFound();
                        }
                        return Ok(oreder2);
                    case "Not processed":
                        var oreder3 = entities.Oreder.Where(t => t.Status_StatusID == 1).Include(t => t.Client).Include(t => t.Status).ToList();
                        if (oreder3 == null)
                        {
                            return NotFound();
                        }
                        return Ok(oreder3);
                    default: return NotFound();
                }                    
            }
        }

        //GET: api/Oreders/Pagi/1/5
        [ResponseType(typeof(Oreder))]
        [Route("api/Oreders/Pagi/{firstID:long}/{secID:long}")]
        public IHttpActionResult GetOrederPagination(long firstID, long secID)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var oreder = entities.Oreder.Where(t => t.OrderId >= firstID && t.OrderId <= secID).Include(t => t.Client).Include(t => t.Status).ToList();
                if (oreder == null)
                {
                    return NotFound();
                }
                return Ok(oreder);
            }
        }


        // PUT: api/Oreders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOreder(long id, Oreder oreder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oreder.OrderId)
            {
                return BadRequest();
            }

            db.Entry(oreder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrederExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Oreders
        [ResponseType(typeof(Oreder))]
        public IHttpActionResult PostOreder(Oreder oreder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Oreder.Add(oreder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = oreder.OrderId }, oreder);
        }

        // DELETE: api/Oreders/5
        [ResponseType(typeof(Oreder))]
        public IHttpActionResult DeleteOreder(long id)
        {
            Oreder oreder = db.Oreder.Find(id);
            if (oreder == null)
            {
                return NotFound();
            }

            db.Oreder.Remove(oreder);
            db.SaveChanges();

            return Ok(oreder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrederExists(long id)
        {
            return db.Oreder.Count(e => e.OrderId == id) > 0;
        }
    }
}