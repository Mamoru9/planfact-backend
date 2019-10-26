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
    public class ClientsController : ApiController
    {
        private backendEntities db = new backendEntities();

        // GET: api/Clients
        public IQueryable<Client> GetClient()
        {
            return db.Client;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(long id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        //GET: api/Clients/Search/FirstName/Nick
        [ResponseType(typeof(Client))]
        [Route("api/Clients/Search/FirstName/{name}")]
        public IHttpActionResult GetClientSearchByFirstName(string name)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var clients = entities.Client.Where(t => t.FirstName == name).Include(t => t.Oreder).Include("Oreder.Status").ToList();
                if (clients == null)
                {
                    return NotFound();
                }
                return Ok(clients);
            }
        }

        //GET: api/Clients/Search/LastName/Fergison
        [ResponseType(typeof(Client))]
        [Route("api/Clients/Search/LastName/{name}")]
        public IHttpActionResult GetClientSearchByLastName(string name)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var clients = entities.Client.Where(t => t.LastName == name).Include(t => t.Oreder).Include("Oreder.Status").ToList();
                if (clients == null)
                {
                    return NotFound();
                }
                return Ok(clients);
            }
        }

        //GET: api/Clients/Search/DateOfBirth/1999-26-11
        [ResponseType(typeof(Client))]
        [Route("api/Clients/Search/DateOfBirth/{date:DateTime}")]
        public IHttpActionResult GetClientSearchByDate(DateTime date)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var clients = entities.Client.Where(t => t.DateOfBirth == date).Include(t => t.Oreder).Include("Oreder.Status").ToList();
                if (clients == null)
                {
                    return NotFound();
                }
                return Ok(clients);
            }
        }

        //GET: api/Clients/Pagi/1/5
        [ResponseType(typeof(Client))]
        [Route("api/Clients/Pagi/{firstID:long}/{secID:long}")]
        public IHttpActionResult GetClientPagination(long firstID, long secID)
        {
            using (backendEntities entities = new backendEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var clients = entities.Client.Where(t => t.ClientId >= firstID && t.ClientId <= secID).Include(t => t.Oreder).Include("Oreder.Status").ToList();
                if (clients == null)
                {
                    return NotFound();
                }
                return Ok(clients);
            }
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(long id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Client.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(long id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(long id)
        {
            return db.Client.Count(e => e.ClientId == id) > 0;
        }
    }
}