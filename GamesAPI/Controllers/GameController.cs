using AutoMapper;
using GamesAPI.Data.UnitOfWork;
using GamesAPI.Models;
using GamesAPI.Models.DTO;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ITeamService _teamService;

        public GameController(IGameService gameService, ITeamService teamService)
        {
            _gameService = gameService;
            _teamService = teamService;
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
        public async Task<ActionResult> Post([FromBody] GameCreateDto dto)
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
        public async Task<ActionResult> Put(int id, [FromBody] GameDto dto)
        {
            if (dto == null || id != dto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _gameService.Any(id))
                return NotFound();

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


        [HttpGet("{gameId}/teams")]
        public async Task<ActionResult<List<TeamDto>>> GetTeamsByGame(int gameId)
        {
            if (gameId <= 0)
                return BadRequest();

            if (!await _gameService.Any(gameId))
                return NotFound();

            var teams = await _gameService.GetTeamsByGame(gameId);
            return Ok(teams);
        }

        [HttpPost("{gameId}/teams/{teamId}")]
        public async Task<ActionResult> AddTeamToGame(int gameId, int teamId)
        {
            if (gameId <= 0 || teamId <= 0)
                return BadRequest();

            if (!await _gameService.Any(gameId))
                ModelState.AddModelError("", "Game not found");

            if (!await _teamService.Any(teamId))
                ModelState.AddModelError("", "Team not found");

            if(!ModelState.IsValid)
                return NotFound(ModelState);

            var saved = await _gameService.AddTeamToGame(gameId, teamId);
            if (!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Added");
        }

        [HttpDelete("{gameId}/teams/{teamId}")]
        public async Task<ActionResult> DeleteTeamFromGame(int gameId, int teamId)
        {
            if(gameId <= 0 || teamId <= 0)
                return BadRequest();

            if (!await _gameService.Any(gameId))
                ModelState.AddModelError("", "Game not found");

            if (!await _teamService.Any(teamId))
                ModelState.AddModelError("", "Team not found");

            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var saved = await _gameService.DeleteTeamFromGame(gameId, teamId);
            if (!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleted");
        }

    }
}
