using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberAdministrationLedubCore.Models;

namespace MemberAdministrationLedubCore.Interfaces
{
    public interface ITeamRepository
    {
        public List<Team> Get();

        public Team Get(int id);

        public Team Create(Team team, List<int> memberIds);

        public Team Update(int id, Team updatedTeam, List<int> memberIds);

        public Team Delete(int id);
    }
}
