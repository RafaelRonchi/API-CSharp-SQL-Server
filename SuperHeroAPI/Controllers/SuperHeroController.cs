using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Model;
using SuperHeroAPI.DTO;
using System.Reflection;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHeroModel> heroes = new List<SuperHeroModel> { new SuperHeroModel { Id = 1, Name = "Teste", FirstName = "testudo", LastName = "testalha", Place = "goias" } };

        [HttpGet]
        public async Task<IActionResult> SearchHeros()
        {

            return Ok(heroes);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHeroModel>>> addHero(SuperHeroModel model)
        {
            heroes.Add(model);
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroModel>> findById(int id)
        {
            var hero = heroes.FirstOrDefault(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Not found");
            }
            else
            {
                return Ok(hero);
            }
        }


        [HttpPut]
        public async Task<ActionResult<SuperHeroModel>> atualizarheroi(SuperHeroDTO s)
        {
            var hero = heroes.FirstOrDefault(hero => hero.Id == s.Id);
            if (hero == null) return NotFound("Not found");
            hero.LastName = s.LastName;
            hero.FirstName = s.FirstName;
            hero.Place = s.Place;

            heroes.Add(hero);

            return Ok(hero);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delHero(int id)
        {
            var hero = heroes.FirstOrDefault(hero
                => hero.Id == id);
            if (hero == null) return NotFound("Not found");
            heroes.Remove(hero);
            return Ok("Deleted");
        }
    }
}
