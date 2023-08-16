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
        
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> SearchHeros()
        {
            var hero = await _dataContext.SuperHeroes.ToListAsync();
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHeroModel>>> addHero(SuperHeroModel model)
        {
            _dataContext.SuperHeroes.Add(model);
            await _dataContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroModel>> findById(int id)
        {
            var hero = await _dataContext.SuperHeroes.FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Not found");
            }
            else
            {
                return Ok(hero);
            }
        }

        [HttpGet("cidade/{cidade}")]
        public async Task<ActionResult<List<SuperHeroModel>>> BuscarHeroisPorCidade(string cidade)
        {
            var hero = await _dataContext.SuperHeroes.AllAsync(h => h.Place == cidade);
            if (hero == null)
                return BadRequest("Hero Not Found");
            return Ok(hero);
        }

        [HttpPut]
        public async Task<ActionResult<SuperHeroModel>> atualizarheroi(SuperHeroDTO s)
        {
            var dbHero = await _dataContext.SuperHeroes.FirstOrDefaultAsync(hero => hero.Id == s.Id);
            if (dbHero == null) return NotFound("Not found");
            dbHero.LastName = s.LastName;
            dbHero.FirstName = s.FirstName;
            dbHero.Place = s.Place;

            _dataContext.SuperHeroes.Add(dbHero);
            await _dataContext.SaveChangesAsync();

            return Ok(dbHero);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> delHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null) return NotFound("Not found");

            _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
