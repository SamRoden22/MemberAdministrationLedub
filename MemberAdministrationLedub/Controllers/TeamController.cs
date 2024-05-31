using MemberAdministrationLedub.ViewModels;
using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubCore.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MemberAdministrationLedub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/<TeamController>
        [HttpGet]
        public List<Team> Get()
        {
            var teams = _teamService.Get();
            return teams;
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public Team Get(int id)
        {
            var team = _teamService.Get(id);
            return team;
        }

        // POST api/<TeamController>
        [HttpPost]
        public Team Post([FromBody] TeamViewModel teamViewModel)
        {
            if (teamViewModel == null || teamViewModel.Team == null || teamViewModel.MemberIds.Count == 0)
            {
                return null;
            }
            var newTeam = _teamService.Create(teamViewModel.Team, teamViewModel.MemberIds);
            return newTeam;
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public ActionResult<Team> Put(int id, [FromBody] TeamViewModel updatedTeam)
        {
            var team = _teamService.Update(id, updatedTeam.Team, updatedTeam.MemberIds);
            return team;
        }


        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public ActionResult<Team> Delete(int id)
        {
            var team = _teamService.Delete(id);
            return team;
        }
    }
}
