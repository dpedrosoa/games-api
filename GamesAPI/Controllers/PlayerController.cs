using GamesAPI.Models.DTO;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/<PlayerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> Get()
        {
            var players = await _playerService.GetAll();
            return Ok(players);
        }

        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var player = await _playerService.Get(id);
            if (player == null)
                return NotFound();

            return Ok(player);
        }

        // POST api/<PlayerController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PlayerCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saved = await _playerService.Create(dto);
            if(!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok(dto);
        }

        // PUT api/<PlayerController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PlayerDto dto)
        {
            if(id <= 0 || dto == null || id != dto.Id || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _playerService.Any(id))
                return NotFound();

            var saved = await _playerService.Update(dto);
            if(!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }

            return Ok(dto);
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(id <= 0) 
                return BadRequest();

            if (!await _playerService.Any(id))
                return NotFound();

            var saved = await _playerService.Delete(id);
            if (!saved)
            {
                ModelState.AddModelError("", "Error saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Deleted");
        }
    }
}
