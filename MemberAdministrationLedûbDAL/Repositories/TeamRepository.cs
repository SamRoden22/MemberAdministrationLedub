﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberAdministrationLedûbCore.Interfaces;
using MemberAdministrationLedûbCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberAdministrationLedûbDAL.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Team> Get()
        {
            return _context.Teams.ToList();
        }

        public Team Get(int id)
        {
            return _context.Teams.FirstOrDefault(t => t.Id == id);
        }

        public Team Create(Team team, List<int> memberIds)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            
            var newTeam = _context.Teams.FirstOrDefault(t => t.Id == team.Id);
            
            foreach (var memberId in memberIds)
            {
                var member = _context.Members.FirstOrDefault(m => m.Id == memberId);
                if (member != null)
                {
                    newTeam.Members.Add(member);
                }
            }

            _context.SaveChanges();

            return newTeam;
        }

        public Team Update(int id, List<int> memberIds)
        {
            return null;
        }

        public Team Delete(int id)
        {
            var team = _context.Teams.Find(id);

            if (team != null)
            {
                foreach (var members in team.Members)
                {
                    var teamMember = members.Teams.FirstOrDefault(tm => tm.Id == team.Id);
                    if (teamMember != null)
                    {
                        members.Teams.Remove(teamMember);
                    }
                }

                _context.Teams.Remove(team);
                _context.SaveChanges();
            }
            return team;
        }
    }
}
