using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestTeamsApi.Models;


namespace BestTeamsApi.Controllers
{
    [Route("api/team")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamPlayersContext _context;

        public TeamController(TeamPlayersContext context)
        {
            _context = context;
            

            if (_context.Teams.Count() == 0)
            {
                //Create a new Team if the list is Empty
                _context.Teams.Add(new Team()
                {                    

                    TeamName = "Thunders",
                    Location = "Oklahoma",
                    Players = new List<Player>(){
                        new Player(){
                            PlayerID = 1,
                            FirstName = "Kevin",
                            LastName = "Durant"
                        }
                    }
                });
                //You can use these extra teams with one player added just uncomment them---------------------------------------------------
                //------------------------------------------------------------------------

                //_context.Teams.Add(new Team()
                //{
                //    TeamName = "Lakers",
                //    Location = "LosAngeles",
                //    Players = new List<Player>(){
                //        new Player(){
                //            PlayerID = 2,
                //            FirstName = "Kobe",
                //            LastName = "Bryant"
                //        }
                //    }
                //});
                //_context.Teams.Add(new Team()
                //{
                    

                //    TeamName = "Celtics",
                //    Location = "Boston",
                //    Players = new List<Player>(){
                //        new Player(){
                //            PlayerID = 3,
                //            FirstName = "Rajon",
                //            LastName = "Rondo"
                //        }
                //    }
                //});

                _context.SaveChanges();
            }
        }
        

        //Get api/team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            //Displays all The teams
            return await _context.Teams.ToListAsync();
        }


        //Get api/team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            //Displays Team with the given ID
            var bestTeam = await _context.Teams.FindAsync(id);

            if (bestTeam == null)
            {
                return NotFound();
            }

            return bestTeam;
        }

        //Get api/orderBy/name or location
        [HttpGet("orderBy/{orderBy}")]
        public async Task<ActionResult<IEnumerable<Team>>> GetorderBy(string orderBy)
        {

            //Orders By Team Name or Location Alphabetically
            List<Team> orderTeam = null;
            if(orderBy.Equals("name",System.StringComparison.InvariantCultureIgnoreCase))
            {
                orderTeam = await _context.Teams.OrderBy(t => t.TeamName).ToListAsync();
            }
            if (orderBy.Equals("location", System.StringComparison.InvariantCultureIgnoreCase))
            {
                orderTeam = await _context.Teams.OrderBy(t => t.Location).ToListAsync();
            }

            if (orderTeam == null)
            {
                return NotFound();
            }

            return orderTeam;
        }



        // POST: api/Team
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team item)
        {
            //Creates a Team

            if (_context.Teams.Any(ac => ac.TeamName.Equals(item.TeamName, System.StringComparison.CurrentCultureIgnoreCase) && _context.Teams.Any(l => l.Location.Equals(item.Location, System.StringComparison.CurrentCultureIgnoreCase))))
            {
               return BadRequest();
            }
            
            else
                _context.Teams.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeam), new { id = item.ID}, item);

        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team item)
        {
            //Update A teams Info
            if (id != item.ID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            //Delete the team
            var delete = await _context.Teams.FindAsync(id);

            if (delete == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(delete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}