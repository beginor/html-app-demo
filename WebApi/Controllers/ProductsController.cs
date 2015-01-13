using System.Web.Http;
using WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Castle.Core.Logging;

namespace WebApi.Controllers {

    [RoutePrefix("api/products")]
    public class ProductsController : ApiController {

        public ILogger Logger { get; set; } = NullLogger.Instance;

        private static readonly IList<Product> Data;

        static ProductsController() {
            Data = new List<Product> {
                new Product { ProductId =  1, ProductName =  "Chai", SupplierId =  1, CategoryId =  1, QuantityPerUnit =  "10 boxes x 20 bags", UnitPrice =  18, UnitsInStock =  39, UnitsOnOrder =  0, ReorderLevel =  10, Discontinued =  false }, 
                new Product { ProductId =  2, ProductName =  "Chang", SupplierId =  1, CategoryId =  1, QuantityPerUnit =  "24 - 12 oz bottles", UnitPrice =  19, UnitsInStock =  17, UnitsOnOrder =  40, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  3, ProductName =  "Aniseed Syrup", SupplierId =  1, CategoryId =  2, QuantityPerUnit =  "12 - 550 ml bottles", UnitPrice =  10, UnitsInStock =  13, UnitsOnOrder =  70, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  4, ProductName =  "Chef Anton's Cajun Seasoning", SupplierId =  2, CategoryId =  2, QuantityPerUnit =  "48 - 6 oz jars", UnitPrice =  22, UnitsInStock =  53, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  5, ProductName =  "Chef Anton's Gumbo Mix", SupplierId =  2, CategoryId =  2, QuantityPerUnit =  "36 boxes", UnitPrice =  21.35, UnitsInStock =  0, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  true }, 
                new Product { ProductId =  6, ProductName =  "Grandma's Boysenberry Spread", SupplierId =  3, CategoryId =  2, QuantityPerUnit =  "12 - 8 oz jars", UnitPrice =  25, UnitsInStock =  120, UnitsOnOrder =  0, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  7, ProductName =  "Uncle Bob's Organic Dried Pears", SupplierId =  3, CategoryId =  7, QuantityPerUnit =  "12 - 1 lb pkgs.", UnitPrice =  30, UnitsInStock =  15, UnitsOnOrder =  0, ReorderLevel =  10, Discontinued =  false }, 
                new Product { ProductId =  8, ProductName =  "Northwoods Cranberry Sauce", SupplierId =  3, CategoryId =  2, QuantityPerUnit =  "12 - 12 oz jars", UnitPrice =  40, UnitsInStock =  6, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  9, ProductName =  "Mishi Kobe Niku", SupplierId =  4, CategoryId =  6, QuantityPerUnit =  "18 - 500 g pkgs.", UnitPrice =  97, UnitsInStock =  29, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  true }, 
                new Product { ProductId =  10, ProductName =  "Ikura", SupplierId =  4, CategoryId =  8, QuantityPerUnit =  "12 - 200 ml jars", UnitPrice =  31, UnitsInStock =  31, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  11, ProductName =  "Queso Cabrales", SupplierId =  5, CategoryId =  4, QuantityPerUnit =  "1 kg pkg.", UnitPrice =  21, UnitsInStock =  22, UnitsOnOrder =  30, ReorderLevel =  30, Discontinued =  false }, 
                new Product { ProductId =  12, ProductName =  "Queso Manchego La Pastora", SupplierId =  5, CategoryId =  4, QuantityPerUnit =  "10 - 500 g pkgs.", UnitPrice =  38, UnitsInStock =  86, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  13, ProductName =  "Konbu", SupplierId =  6, CategoryId =  8, QuantityPerUnit =  "2 kg box", UnitPrice =  6, UnitsInStock =  24, UnitsOnOrder =  0, ReorderLevel =  5, Discontinued =  false }, 
                new Product { ProductId =  14, ProductName =  "Tofu", SupplierId =  6, CategoryId =  7, QuantityPerUnit =  "40 - 100 g pkgs.", UnitPrice =  23.25, UnitsInStock =  35, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  15, ProductName =  "Genen Shouyu", SupplierId =  6, CategoryId =  2, QuantityPerUnit =  "24 - 250 ml bottles", UnitPrice =  15.5, UnitsInStock =  39, UnitsOnOrder =  0, ReorderLevel =  5, Discontinued =  false }, 
                new Product { ProductId =  16, ProductName =  "Pavlova", SupplierId =  7, CategoryId =  3, QuantityPerUnit =  "32 - 500 g boxes", UnitPrice =  17.45, UnitsInStock =  29, UnitsOnOrder =  0, ReorderLevel =  10, Discontinued =  false }, 
                new Product { ProductId =  17, ProductName =  "Alice Mutton", SupplierId =  7, CategoryId =  6, QuantityPerUnit =  "20 - 1 kg tins", UnitPrice =  39, UnitsInStock =  0, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  true }, 
                new Product { ProductId =  18, ProductName =  "Carnarvon Tigers", SupplierId =  7, CategoryId =  8, QuantityPerUnit =  "16 kg pkg.", UnitPrice =  62.5, UnitsInStock =  42, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  19, ProductName =  "Teatime Chocolate Biscuits", SupplierId =  8, CategoryId =  3, QuantityPerUnit =  "10 boxes x 12 pieces", UnitPrice =  9.199999999999999, UnitsInStock =  25, UnitsOnOrder =  0, ReorderLevel =  5, Discontinued =  false }, 
                new Product { ProductId =  20, ProductName =  "Sir Rodney's Marmalade", SupplierId =  8, CategoryId =  3, QuantityPerUnit =  "30 gift boxes", UnitPrice =  81, UnitsInStock =  40, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  21, ProductName =  "Sir Rodney's Scones", SupplierId =  8, CategoryId =  3, QuantityPerUnit =  "24 pkgs. x 4 pieces", UnitPrice =  10, UnitsInStock =  3, UnitsOnOrder =  40, ReorderLevel =  5, Discontinued =  false }, 
                new Product { ProductId =  22, ProductName =  "Gustaf's Knäckebröd", SupplierId =  9, CategoryId =  5, QuantityPerUnit =  "24 - 500 g pkgs.", UnitPrice =  21, UnitsInStock =  104, UnitsOnOrder =  0, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  23, ProductName =  "Tunnbröd", SupplierId =  9, CategoryId =  5, QuantityPerUnit =  "12 - 250 g pkgs.", UnitPrice =  9, UnitsInStock =  61, UnitsOnOrder =  0, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  24, ProductName =  "Guaraná Fantástica", SupplierId =  10, CategoryId =  1, QuantityPerUnit =  "12 - 355 ml cans", UnitPrice =  4.5, UnitsInStock =  20, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  true }, 
                new Product { ProductId =  25, ProductName =  "NuNuCa Nuß-Nougat-Creme", SupplierId =  11, CategoryId =  3, QuantityPerUnit =  "20 - 450 g glasses", UnitPrice =  14, UnitsInStock =  76, UnitsOnOrder =  0, ReorderLevel =  30, Discontinued =  false }, 
                new Product { ProductId =  26, ProductName =  "Gumbär Gummibärchen", SupplierId =  11, CategoryId =  3, QuantityPerUnit =  "100 - 250 g bags", UnitPrice =  31.23, UnitsInStock =  15, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  27, ProductName =  "Schoggi Schokolade", SupplierId =  11, CategoryId =  3, QuantityPerUnit =  "100 - 100 g pieces", UnitPrice =  43.9, UnitsInStock =  49, UnitsOnOrder =  0, ReorderLevel =  30, Discontinued =  false }, 
                new Product { ProductId =  28, ProductName =  "Rössle Sauerkraut", SupplierId =  12, CategoryId =  7, QuantityPerUnit =  "25 - 825 g cans", UnitPrice =  45.6, UnitsInStock =  26, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  true }, 
                new Product { ProductId =  30, ProductName =  "Nord-Ost Matjeshering", SupplierId =  13, CategoryId =  8, QuantityPerUnit =  "10 - 200 g glasses", UnitPrice =  25.89, UnitsInStock =  10, UnitsOnOrder =  0, ReorderLevel =  15, Discontinued =  false }, 
                new Product { ProductId =  29, ProductName =  "Thüringer Rostbratwurst", SupplierId =  12, CategoryId =  6, QuantityPerUnit =  "50 bags x 30 sausgs.", UnitPrice =  123.79, UnitsInStock =  0, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  true }, 
                new Product { ProductId =  31, ProductName =  "Gorgonzola Telino", SupplierId =  14, CategoryId =  4, QuantityPerUnit =  "12 - 100 g pkgs", UnitPrice =  12.5, UnitsInStock =  0, UnitsOnOrder =  70, ReorderLevel =  20, Discontinued =  false }, 
                new Product { ProductId =  32, ProductName =  "Mascarpone Fabioli", SupplierId =  14, CategoryId =  4, QuantityPerUnit =  "24 - 200 g pkgs.", UnitPrice =  32, UnitsInStock =  9, UnitsOnOrder =  40, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  33, ProductName =  "Geitost", SupplierId =  15, CategoryId =  4, QuantityPerUnit =  "500 g", UnitPrice =  2.5, UnitsInStock =  112, UnitsOnOrder =  0, ReorderLevel =  20, Discontinued =  false }, 
                new Product { ProductId =  34, ProductName =  "Sasquatch Ale", SupplierId =  16, CategoryId =  1, QuantityPerUnit =  "24 - 12 oz bottles", UnitPrice =  14, UnitsInStock =  111, UnitsOnOrder =  0, ReorderLevel =  15, Discontinued =  false }, 
                new Product { ProductId =  35, ProductName =  "Steeleye Stout", SupplierId =  16, CategoryId =  1, QuantityPerUnit =  "24 - 12 oz bottles", UnitPrice =  18, UnitsInStock =  20, UnitsOnOrder =  0, ReorderLevel =  15, Discontinued =  false }, 
                new Product { ProductId =  36, ProductName =  "Inlagd Sill", SupplierId =  17, CategoryId =  8, QuantityPerUnit =  "24 - 250 g  jars", UnitPrice =  19, UnitsInStock =  112, UnitsOnOrder =  0, ReorderLevel =  20, Discontinued =  false }, 
                new Product { ProductId =  37, ProductName =  "Gravad lax", SupplierId =  17, CategoryId =  8, QuantityPerUnit =  "12 - 500 g pkgs.", UnitPrice =  26, UnitsInStock =  11, UnitsOnOrder =  50, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  38, ProductName =  "Côte de Blaye", SupplierId =  18, CategoryId =  1, QuantityPerUnit =  "12 - 75 cl bottles", UnitPrice =  263.5, UnitsInStock =  17, UnitsOnOrder =  0, ReorderLevel =  15, Discontinued =  false }, 
                new Product { ProductId =  39, ProductName =  "Chartreuse verte", SupplierId =  18, CategoryId =  1, QuantityPerUnit =  "750 cc per bottle", UnitPrice =  18, UnitsInStock =  69, UnitsOnOrder =  0, ReorderLevel =  5, Discontinued =  false }, 
                new Product { ProductId =  40, ProductName =  "Boston Crab Meat", SupplierId =  19, CategoryId =  8, QuantityPerUnit =  "24 - 4 oz tins", UnitPrice =  18.4, UnitsInStock =  123, UnitsOnOrder =  0, ReorderLevel =  30, Discontinued =  false }, 
                new Product { ProductId =  41, ProductName =  "Jack's New England Clam Chowder", SupplierId =  19, CategoryId =  8, QuantityPerUnit =  "12 - 12 oz cans", UnitPrice =  9.65, UnitsInStock =  85, UnitsOnOrder =  0, ReorderLevel =  10, Discontinued =  false }, 
                new Product { ProductId =  42, ProductName =  "Singaporean Hokkien Fried Mee", SupplierId =  20, CategoryId =  5, QuantityPerUnit =  "32 - 1 kg pkgs.", UnitPrice =  14, UnitsInStock =  26, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  true }, 
                new Product { ProductId =  43, ProductName =  "Ipoh Coffee", SupplierId =  20, CategoryId =  1, QuantityPerUnit =  "16 - 500 g tins", UnitPrice =  46, UnitsInStock =  17, UnitsOnOrder =  10, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  44, ProductName =  "Gula Malacca", SupplierId =  20, CategoryId =  2, QuantityPerUnit =  "20 - 2 kg bags", UnitPrice =  19.45, UnitsInStock =  27, UnitsOnOrder =  0, ReorderLevel =  15, Discontinued =  false }, 
                new Product { ProductId =  45, ProductName =  "Rogede sild", SupplierId =  21, CategoryId =  8, QuantityPerUnit =  "1k pkg.", UnitPrice =  9.5, UnitsInStock =  5, UnitsOnOrder =  70, ReorderLevel =  15, Discontinued =  false }, 
                new Product { ProductId =  46, ProductName =  "Spegesild", SupplierId =  21, CategoryId =  8, QuantityPerUnit =  "4 - 450 g glasses", UnitPrice =  12, UnitsInStock =  95, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  47, ProductName =  "Zaanse koeken", SupplierId =  22, CategoryId =  3, QuantityPerUnit =  "10 - 4 oz boxes", UnitPrice =  9.5, UnitsInStock =  36, UnitsOnOrder =  0, ReorderLevel =  0, Discontinued =  false }, 
                new Product { ProductId =  48, ProductName =  "Chocolade", SupplierId =  22, CategoryId =  3, QuantityPerUnit =  "10 pkgs.", UnitPrice =  12.75, UnitsInStock =  15, UnitsOnOrder =  70, ReorderLevel =  25, Discontinued =  false }, 
                new Product { ProductId =  49, ProductName =  "Maxilaku", SupplierId =  23, CategoryId =  3, QuantityPerUnit =  "24 - 50 g pkgs.", UnitPrice =  20, UnitsInStock =  10, UnitsOnOrder =  60, ReorderLevel =  15, Discontinued =  false }, 
                new Product { ProductId =  50, ProductName =  "Valkoinen suklaa", SupplierId =  23, CategoryId =  3, QuantityPerUnit =  "12 - 100 g bars", UnitPrice =  16.25, UnitsInStock =  65, UnitsOnOrder =  0, ReorderLevel =  30, Discontinued =  false }
            };
        }

        [Route("")]
        public IHttpActionResult Get() {
            return Ok(Data);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id) {
            var found = Data.FirstOrDefault(p => p.ProductId == id);
            if (found == null) {
                return NotFound();
            }
            return Ok(found);
        }

        [Route("~/api/categories/{categoryId}/products")]
        public IHttpActionResult GetByCategory(int categoryId) {
            Logger.DebugFormat("GET: api/categories/{0}/products", categoryId);
            var query = Data.Where(p => p.CategoryId == categoryId);
            if (query.Any()) {
                return Ok(query.ToList());
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("~/api/suppliers/{supplierId}/products")]
        public IHttpActionResult GetBySupplier(int supplierId) {
            var query = Data.Where(p => p.SupplierId == supplierId);
            if (query.Any()) {
                return Ok(query.ToList());
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}

