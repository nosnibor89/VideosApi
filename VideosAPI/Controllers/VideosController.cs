using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideosAPI.Models;

namespace VideosAPI.Controllers
{
    public class VideosController : ApiController
    {
        private VideoDB db;

        public VideosController()
        {
            db = new VideoDB();
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/video
        public IEnumerable<Video> GetAllVideos()
        {
            return db.Videos;
        }

        // GET api/video/5
        public Video Get(int id)
        {
         
             var video = db.Videos.Find(id);
             if (video == null) throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
             return video;
            

        }

        // POST api/video
        //public void Post([FromBody]string value)
        //{
        //}

        //POST api/video
        public HttpResponseMessage Post(Video video)
        {
            if (ModelState.IsValid)
            {
                db.Videos.Add(video);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, video);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = video.Id }));
                return response;
                
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }



        // PUT api/video/5
        public HttpResponseMessage PutVideo(int id, Video video)
        {
            if (ModelState.IsValid && id == video.Id)
            {
                db.Entry(video).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DBConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/video/5
        public HttpResponseMessage DeleteVideo(int id)
        {
            var video = db.Videos.Find(id);

            if (video == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Videos.Remove(video);

            try
            {
                db.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                
               return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, video);
        }
    }
}
