﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CheapIdeas.Models;
using Microsoft.AspNet.Identity;

namespace CheapIdeas.Controllers
{
    public class IdeasController : ApiController
    {
       private IdeasContext db = new IdeasContext();

        // GET: api/Ideas/ForCurrentUser
        [Authorize]
        [Route("api/Ideas/ForCurrentUser")]
        public IQueryable<Idea> GetIdeasForCurrentUser() 
        {
            string userId = User.Identity.GetUserId();

            return db.Ideas.Where(idea => idea.UserId == userId);
        }

        // GET: api/Ideas
        [Authorize]
        public IQueryable<Idea> GetIdeas()
        {
            string userId = User.Identity.GetUserId();

            return db.Ideas;
        }

        // GET: api/Ideas/5
        [Authorize]
        [ResponseType(typeof(Idea))]
        public IHttpActionResult GetIdea(int id)
        {
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return NotFound();
            }

            return Ok(idea);
        }

        // PUT: api/Ideas/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIdea(int id, Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != idea.Id)
            {
                return BadRequest();
            }

            //Se asigna este operadopr para evitar que otros usuarios 
            //puedan modificar los post de otros usuarios

            //obtenemos usuario
            var userId = User.Identity.GetUserId();

            if (userId != idea.UserId)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }

            db.Entry(idea).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdeaExists(id))
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

        // POST: api/Ideas
        [ResponseType(typeof(Idea))]
        [Authorize]
        public IHttpActionResult PostIdea(Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();
            idea.UserId = userId;

            db.Ideas.Add(idea);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = idea.Id }, idea);
        }

        // DELETE: api/Ideas/5
        [Authorize] 
        [ResponseType(typeof(Idea))]
        public IHttpActionResult DeleteIdea(int id)
        {
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return NotFound();
            }

            //Se asigna este operadopr para evitar que otros usuarios 
            //puedan modificar los post de otros usuarios

            //obtenemos usuario
            string userId = User.Identity.GetUserId();

            if (userId != idea.UserId)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }

            db.Ideas.Remove(idea);
            db.SaveChanges();

            return Ok(idea);
        }
        /*Se crea metodo para poder realizar la busqueda de las ideas*/


        // GET: api/Ideas/Search/{keyword}
        [Authorize]
        [Route("api/Ideas/Search/{keyword}")]
        [HttpGet]//se debe especificar
        public IQueryable<Idea> SearchIdeas(string keyword)
        {
            //realizamos la busqueda en la DB por la palabra clave
            return db.Ideas.Where(idea => idea.Title.Contains(keyword)
                                        || idea.Description.Contains(keyword));
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdeaExists(int id)
        {
            return db.Ideas.Count(e => e.Id == id) > 0;
        }
    }
}