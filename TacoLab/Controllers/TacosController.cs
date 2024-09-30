using System.Net.Sockets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoLab.Models;

namespace TacoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]

        public IActionResult GetAll(string? Name = null, bool? SoftShell = null)
        {
            List<Taco> result = dbContext.Tacos.ToList();
            if(Name != null)
            {
                result = result.Where(x => x.Name.ToLower().Trim() == Name.ToLower().Trim()).ToList();
            }
            return Ok(result);
            if (SoftShell != null)
            { 
                result = result.Where(x => x.SoftShell == SoftShell).ToList();       
            }
        }
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            Taco result = dbContext.Tacos.FirstOrDefault(i => i.Id == Id);
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
        public IActionResult AddUser([FromBody] Taco newTaco)
        {
            //prevents error with increwmenting
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();

            return Created($"/api/User/{newTaco.Id}", newTaco);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteUser(int id)
        {
            Taco u = dbContext.Tacos.FirstOrDefault(x => x.Id == id);
            if (u == null)
            {
                return NotFound("no matching id");
            }
            else
            {
                dbContext.Tacos.Remove(u);
                dbContext.SaveChanges();
                return NoContent();
            }
        }

    }
}
