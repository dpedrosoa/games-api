using GamesAPI.Models.DTO;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IPlayerService _playerService;

        public TeamController(ITeamService teamService, IPlayerService playerService)
        {
            _teamService = teamService;
            _playerService = playerService;
        }

        // GET: api/<TeamController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> Get()
        {
            var teams = await _teamService.GetAll();
            return Ok(teams);
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var team = await _teamService.Get(id);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        // POST api/<TeamController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TeamCreateDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var saved = await _teamService.Create(dto);
            if (!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }
            return Ok(dto);
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TeamDto dto)
        {

            if (id <= 0 || dto == null || id != dto.Id || !ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _teamService.Any(id))
                return NotFound();

            var saved = await _teamService.Update(dto);
            if(!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok(dto);
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _teamService.Any(id))
                return NotFound();

            var saved = await _teamService.Delete(id);
            if (!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleted");
        }

        [HttpGet("{teamId}/players")]
        public async Task<ActionResult<List<PlayerDto>>> GetPlayersByTeam(int teamId)
        {
            if (teamId <= 0)
                return BadRequest();

            if (!await _teamService.Any(teamId))
                return NotFound();

            var players = await _teamService.GetPlayersByTeam(teamId);
            return Ok(players);
        }

        [HttpPost("{teamId}/players/{playerId}")]
        public async Task<ActionResult> AddPlayerToTeam(int teamId, int playerId)
        {
            if (teamId <= 0 || playerId <= 0)
                return BadRequest();

            if (!await _teamService.Any(teamId))
                ModelState.AddModelError("", "Team not found");

            if (!await _playerService.Any(playerId))
                ModelState.AddModelError("", "Player not found");

            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var saved = await _teamService.AddPlayerToTeam(teamId, playerId);
            if(!saved)
            {
                ModelState.AddModelError("", "Error saving");
                StatusCode(500, ModelState);
            }
            return Ok("Added");
        }


        [HttpDelete("{teamId}/players/{playerId}")]
        public async Task<ActionResult> DeletePlayerFromTeam(int teamId, int playerId)
        {
            if (teamId <= 0 || playerId <= 0)
                return BadRequest();

            if (!await _teamService.Any(teamId))
                ModelState.AddModelError("", "Team not found");

            if (!await _playerService.Any(playerId))
                ModelState.AddModelError("", "Player not found");

            if(!ModelState.IsValid)
                return NotFound(ModelState);

            var saved = await _teamService.DeletePlayerFromTeam(teamId, playerId);
            if(!saved)
            {
                ModelState.AddModelError("", "Error saving");
                StatusCode(500, ModelState);
            }

            return Ok("Deleted");
        }

    }
}
