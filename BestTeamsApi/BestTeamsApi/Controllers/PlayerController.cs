using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestTeamsApi.Models;


namespace BestTeamsApi.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly TeamPlayersContext _context;


        public PlayerController(TeamPlayersContext context)
        {
            _context = context;
        }


        //Get api/player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            //Displays All Players from EveryTeam
            return await _context.Players.ToListAsync();
        }


        //Get api/player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {

            //Grab the player with the given ID
            var bestPlayer= await _context.Players.FindAsync(id);

            if (bestPlayer == null)
            {
                return NotFound();
            }

            return bestPlayer;
        }


        //Get api/byLastName/Valdez
        [HttpGet("byLastName/{lastName}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetLastName(string lastName)
        {
            //Grabs the players with the same last names
            var bestPlayer = await _context.Players.Where(l => l.LastName.Equals(lastName, System.StringComparison.CurrentCultureIgnoreCase)).ToListAsync();

            if (bestPlayer == null)
            {
                return NotFound();
            }

            return bestPlayer;
        }


        //Get api/byTeam/3
        [HttpGet("byTeam/{id}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetTeamPlayers(int id)
        {
            //Displays All the players in the same team using the teamID
            var bestPlayer = await _context.Players.Where(l => l.TeamID.Equals(id)).ToListAsync();

            if (bestPlayer == null)
            {
                return NotFound();
            }

            return bestPlayer;
        }


        // POST: api/Player
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player item)
        {
            //Create a player with the post 
            //Can create up to 8 players with the same team ID
            //Each team allows only 8 players
            var maxPlayer = 8;
            var currentCount = _context.Players.Where(p => p.TeamID == item.TeamID).Count();
            if(currentCount >= maxPlayer)
            { 
                return BadRequest(); 
            }
            else if(_context.Players.Any(p=> p.FirstName.Equals(item.FirstName, System.StringComparison.CurrentCultureIgnoreCase)))
            {
                return BadRequest();
            }
            _context.Players.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayer), new { id = item.PlayerID }, item);
        }


        // PUT: api/Player/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player item)
        {
            //Update Players info 
            if (id != item.PlayerID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            //Delete a Player
            var delete = await _context.Players.FindAsync(id);

            if (delete == null)
            {
                return NotFound();
            }

            _context.Players.Remove(delete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}