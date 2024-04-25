using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubCore.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace MemberAdministrationLedubCore.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public List<Team> Get()
        {
            return _teamRepository.Get();
        }

        public Team Get(int id)
        {
            return _teamRepository.Get(id);
        }

        public Team Create(Team team, List<int> memberIds)
        {
            ValidateTeamAndMemberIds(team, memberIds);
            return _teamRepository.Create(team, memberIds);
        }

        public Team Update(int id, Team updatedTeam, List<int> memberIds)
        {
            ValidateTeamAndMemberIds(updatedTeam, memberIds);
            return _teamRepository.Update(id, updatedTeam, memberIds);
        }

        public Team Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than zero", nameof(id));
            }

            return _teamRepository.Delete(id);
        }

        private void ValidateTeamAndMemberIds(Team team, List<int> memberIds)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            if (memberIds == null)
            {
                throw new ArgumentNullException(nameof(memberIds), "MemberIds cannot be null");
            }

            if (memberIds.Count == 0)
            {
                throw new ArgumentException("MemberIds cannot be empty", nameof(memberIds));
            }
        }
    }
}
