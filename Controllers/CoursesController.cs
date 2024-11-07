using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using d3.Models;

namespace d3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolContext db;

        public CoursesController(SchoolContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult get()
        {
            var courses = db.Courses.ToList();
            if (courses.Count == 0)
                return NotFound();
            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        public IActionResult getById(int id)
        {
            Course course = db.Courses.SingleOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpGet("{name}")]
        public IActionResult courseByName(string name)
        {
            Course course = db.Courses.FirstOrDefault(c => c.Name == name);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public IActionResult add(Course course)
        {
            if (course == null)
                return BadRequest();
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return CreatedAtAction("getById", new { id = course.Id }, course);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult put(int id, Course course)
        {
            if (course == null || id != course.Id)
                return BadRequest();

            var existingCourse = db.Courses.SingleOrDefault(c => c.Id == id);
            if (existingCourse == null)
                return NotFound();

            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            existingCourse.Credits = course.Credits;

            db.Entry(existingCourse).State = EntityState.Modified;
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteCourse(int id)
        {
            var course = db.Courses.SingleOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();

            db.Courses.Remove(course);
            db.SaveChanges();
            return Ok(course);
        }
    }
}
