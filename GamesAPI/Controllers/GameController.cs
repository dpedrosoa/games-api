using AutoMapper;
using GamesAPI.Data.UnitOfWork;
using GamesAPI.Models;
using GamesAPI.Models.DTO;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/<GameController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> Get()
        {
            var games = await _gameService.GetAll();

            return Ok(games);
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var game = await _gameService.Get(id);
            if(game == null)
                return NotFound();

            return Ok(game);
        }

        // POST api/<GameController>
        [HttpPost]
        public async Task<ActionResult<Game>> Post([FromBody] GameCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var saved = await _gameService.Create(dto);
            if(!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok(dto);
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> Put(int id, [FromBody] GameUpdateDto dto)
        {
            if (dto == null || id != dto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saved = await _gameService.Update(dto);
            if (!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok(dto);
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(!await _gameService.Any(id))
                return NotFound();

            var saved = await _gameService.Delete(id);
            if (!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleted");
        }
    }
}
