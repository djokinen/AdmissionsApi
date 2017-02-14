using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AdmissionsApi.Models;

namespace AdmissionsApi.Controllers
{
	public class GlossaryMessageTypesController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/GlossaryMessageTypes
		public IQueryable<GlossaryMessageType> GetGlossaryMessageTypes()
		{
			return db.GlossaryMessageTypes;
		}

		// GET: api/GlossaryMessageTypes/5
		[ResponseType(typeof(GlossaryMessageType))]
		public async Task<IHttpActionResult> GetGlossaryMessageType(Guid id)
		{
			GlossaryMessageType glossaryMessageType = await db.GlossaryMessageTypes.FindAsync(id);
			if (glossaryMessageType == null)
			{
				return NotFound();
			}

			return Ok(glossaryMessageType);
		}

		// PUT: api/GlossaryMessageTypes/5
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutGlossaryMessageType(Guid id, GlossaryMessageType glossaryMessageType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != glossaryMessageType.Id)
			{
				return BadRequest();
			}

			db.Entry(glossaryMessageType).State = EntityState.Modified;

			try
			{
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GlossaryMessageTypeExists(id))
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

		// POST: api/GlossaryMessageTypes
		[ResponseType(typeof(GlossaryMessageType))]
		public async Task<IHttpActionResult> PostGlossaryMessageType(GlossaryMessageType glossaryMessageType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.GlossaryMessageTypes.Add(glossaryMessageType);

			try
			{
				await db.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (GlossaryMessageTypeExists(glossaryMessageType.Id))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtRoute("DefaultApi", new { id = glossaryMessageType.Id }, glossaryMessageType);
		}

		// DELETE: api/GlossaryMessageTypes/5
		[ResponseType(typeof(GlossaryMessageType))]
		public async Task<IHttpActionResult> DeleteGlossaryMessageType(Guid id)
		{
			GlossaryMessageType glossaryMessageType = await db.GlossaryMessageTypes.FindAsync(id);
			if (glossaryMessageType == null)
			{
				return NotFound();
			}

			db.GlossaryMessageTypes.Remove(glossaryMessageType);
			await db.SaveChangesAsync();

			return Ok(glossaryMessageType);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool GlossaryMessageTypeExists(Guid id)
		{
			return db.GlossaryMessageTypes.Count(e => e.Id == id) > 0;
		}
	}
}