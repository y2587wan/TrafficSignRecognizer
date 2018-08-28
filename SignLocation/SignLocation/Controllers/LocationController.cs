using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignLocation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignLocation.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private TrafficContext _context;

        public LocationController(TrafficContext context)
        {
            _context = context;
            if(_context.Signs.Count() == 0)
            {
                _context.Signs.Add(new Sign { Name = "Stop", Description = String.Empty });
                _context.Signs.Add(new Sign { Name = "School Zone", Description = String.Empty });
                _context.SaveChanges();
            }

            if(_context.Locations.Count() == 0)
            {
                var signs = _context.Signs.Where(s => s.Name == "Stop");

                _context.Locations.Add(new Location { Latitude = 0, Longitude = 0, Signs = signs.ToList() });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public List<Location> GetAll()
        {
            return _context.Locations.ToList();
        }

        [HttpGet("{latitute}/{longitude}", Name = "GetLocation")]
        public IActionResult GetByLatitudeAndLongitude(float latitute, float longitude)
        {
            var item = _context.Locations.Where(l => l.Latitude == latitute && l.Longitude == longitude);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PostLocationData data)
        {

            if (data == null || !data.SignNames.Any())
            {
                return BadRequest();
            }

            var signs = new List<Sign>();

            signs.AddRange(_context.Signs.Where(s => data.SignNames.Contains(s.Name))
                                         .GroupBy(s => s.Name)
                                         .Select(group => group.First()));

            Location location = new Location();

            if (!_context.Locations.Any(l => l.Latitude == data.Latitude && l.Longitude == data.Longitude))
            {
                location.Longitude = data.Latitude;
                location.Latitude = data.Longitude;
                location.Signs = signs;
                _context.Locations.Add(location);
            }
            else
            {
                location = _context.Locations.FirstOrDefault(l => l.Latitude == data.Latitude && l.Longitude == data.Longitude);
                location.Signs = signs;
            }

            _context.SaveChanges();

            return CreatedAtRoute("GetLocation", new { latitute = location.Latitude, longitude = location.Longitude }, location);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
