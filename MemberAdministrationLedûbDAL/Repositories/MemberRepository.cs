using MemberAdministrationLedûbCore.Interfaces;
using MemberAdministrationLedûbCore.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MemberAdministrationLedûbDAL.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connectionString;

        private readonly DataContext _context;

        public MemberRepository(IConfiguration configuration, DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Member> GetAll()
        {
            return _context.Members.ToList();
        }

        public Member Get(int id)
        {
            return _context.Members.FirstOrDefault(m => m.Id == id);
        }

        public Member Create(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();

            return member;
        }

        public Member Update(int id, Member updatedMember)
        {
            var existingMember = _context.Members.Find(id);
            if (existingMember == null)
            {
                // Handle not found case
                // For example, throw an exception or return null
                return null;
            }

            // Update properties of the existing member with values from updatedMember
            existingMember.Name = updatedMember.Name;
            existingMember.Position = updatedMember.Position;

            _context.SaveChanges();

            return existingMember;
        }

        public Member Delete(int id)
        {
            var member = _context.Members.Find(id);

            //if (member == null)
            //{
            //    // Handle not found case
            //    return null;
            //}

            //// Remove related records from the TeamMember table
            //var teamMembers = _context.TeamMembers.Where(tm => tm.MemberId == id);
            //_context.TeamMembers.RemoveRange(teamMembers);

            //// Remove the member from the context
            //_context.Members.Remove(member);

            //// Save changes to the database
            //_context.SaveChanges();

            return member;
        }
    }
}
