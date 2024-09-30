using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoLab.Models;

namespace TacoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]

        public IActionResult GetAllDrinks(string sortByCost)
        {
            List<Drink> result = dbContext.Drinks.ToList();
            if (sortByCost != null)
            {
                if (sortByCost.ToLower() == "ascending")
                {
                    result = result.OrderBy(x => x.Cost).ToList();
                }
                else if (sortByCost.ToLower() == "descending")
                {
                    result = result.OrderByDescending(x => x.Cost).ToList();
                }
                else
                {
                    return BadRequest("Order by Ascending or Descending");
                }
            }
            //if (Cost != null)
            //{
            //    result = result.OrderBy(x => x.Cost).ToList();
            //}
            return Ok(result);
           
        }
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            Drink result = dbContext.Drinks.FirstOrDefault(i => i.Id == Id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(result);
            }

        }
        [HttpPost()]
        public IActionResult AddUser([FromBody] Drink newDrink)
        {
            //prevents error with increwmenting
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();

            return Created($"/api/User/{newDrink.Id}", newDrink);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteUser(int id)
        {
            Drink u = dbContext.Drinks.FirstOrDefault(x => x.Id == id);
            if (u == null)
            {
                return NotFound("no matching id");
            }
            else
            {
                dbContext.Drinks.Remove(u);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}
