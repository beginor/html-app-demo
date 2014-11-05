using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers {

    public class CategoriesController : ApiController {

        private static readonly IList<Category> Data;

        static CategoriesController() {
            Data = new List<Category> {
                new Category { CategoryId = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, be"},
                new Category { CategoryId = 2, CategoryName = "Condiments", Description = "Sweet and savory sauces, relis"},
                new Category { CategoryId = 3, CategoryName = "Confections", Description = "Desserts, candies, and sweet b"},
                new Category { CategoryId = 4, CategoryName = "Dairy Products", Description = "Cheeses"},
                new Category { CategoryId = 5, CategoryName = "Grains/Cereals", Description = "Breads, crackers, pasta, and c"},
                new Category { CategoryId = 6, CategoryName = "Meat/Poultry", Description = "Prepared meats"},
                new Category { CategoryId = 7, CategoryName = "Produce", Description = "Dried fruit and bean curd"},
                new Category { CategoryId = 8, CategoryName = "Seafood", Description = "Seaweed and fish"}
            };
        }

        public IHttpActionResult GetAll() {
            return Ok(Data);
        }

        public IHttpActionResult Get(int id) {
            var c = Data.FirstOrDefault(cat => cat.CategoryId == id);
            if (c == null) {
                return NotFound();
            }
            return Ok(c);
        }

        public IHttpActionResult Post(Category category) {
            category.CategoryId = Data.Count + 1;
            Data.Add(category);
            var response = Request.CreateResponse(category);
            var url = Url.Link("DefaultApi", new { id = category.CategoryId });
            response.Headers.Location = new Uri(url);
            return ResponseMessage(response);
        }

        public IHttpActionResult Put([FromUri]int id, [FromBody]Category category) {
            var cat = Data.FirstOrDefault(c => c.CategoryId == id);
            if (cat == null) {
                return NotFound();
            }
            cat.CategoryName = category.CategoryName;
            cat.Description = category.Description;
            return Ok(cat);
        }

        public IHttpActionResult Delete(int id) {
            var cat = Data.FirstOrDefault(c => c.CategoryId == id);
            if (cat != null) {
                Data.Remove(cat);
            }
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}