using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Review.DAL;
using Review.Model;
using Review.Model.DTO;

namespace Review.Controllers
{
    [Route("/api/car")]
    [ApiController]
    public class CarApiController : Controller
    {
       
        private CarManagerDbContext _dbContext;

        public CarApiController(CarManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [Route("")]
        public ActionResult<List<CarDTO>> Get()
        {
            List<CarDTO> cars = this._dbContext.Cars
                .Select(CarDTO.SelectorExpression)
                .ToList();

            return cars;
        }

        [Route("{id:int}")]
        public ActionResult<CarDTO> Get(int id)
        {
            var car = this._dbContext.Cars
                .Where(c => c.ID == id)
                .Select(CarDTO.SelectorExpression)
                .FirstOrDefault();

            return car;
        }

        [Route("pretraga/{q}")]
        public ActionResult<List<CarDTO>> Get(string q)
        {
            var cars = this._dbContext.Cars
                .Where(c => c.Brand.Name.Contains(q))
                .Select(CarDTO.SelectorExpression)
                .ToList();

            return cars;
        }

        [Route("")]
        [HttpPost]
        public ActionResult<CarDTO> Post(Car c)
        {
            if (ModelState.IsValid)
            {
                c.Brand.CountryID = c.Brand.CountryID;
                c.Brand.Country = null;
                this._dbContext.Cars.Add(c);
                this._dbContext.SaveChanges();

                return Get(c.ID);
            }

            return BadRequest(ModelState);
        }

        [Route("{id:int}")]
        [HttpPut]
        public async Task<ActionResult<CarDTO>> Put(int id, [FromBody] JObject model)
        {
            var valueProvider = new ObjectSourceValueProvider(model);

            Car existing = _dbContext.Cars.FirstOrDefault(p => p.ID == id);
            if (existing != null && ModelState.IsValid && await TryUpdateModelAsync(existing, "", valueProvider))
            {
                this._dbContext.SaveChanges();
                return Get(id);
            }

            if (existing == null)
            {
                ModelState.AddModelError("id", "Invalid client ID");
            }

            return BadRequest(ModelState);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existing = _dbContext.Cars.FirstOrDefault(p => p.ID == id);
            if (existing != null)
            {
                this._dbContext.Entry(existing).State = EntityState.Deleted;
                this._dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(new { error = "Unable to locate client with provided ID", providedID = id });
            }
        }

    }

    public class ObjectSourceValueProvider : IValueProvider
    {
        private JObject _x;

        public ObjectSourceValueProvider(JObject x)
        {
            this._x = x;
        }

        public bool ContainsPrefix(string prefix)
        {
            return _x.Properties().Any(p => p.Name == prefix);
        }

        public ValueProviderResult GetValue(string key)
        {
            var prop = _x.Properties().Where(p => p.Name.ToLower() == key?.ToLower()).FirstOrDefault();

            if (prop == null)
            {
                return ValueProviderResult.None;
            }
            return new ValueProviderResult(prop.Value.ToString());
        }
    }
}