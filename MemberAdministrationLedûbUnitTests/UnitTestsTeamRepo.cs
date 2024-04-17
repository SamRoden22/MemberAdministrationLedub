using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberAdministrationLedubDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using MemberAdministrationLedubCore.Models;

namespace MemberAdministrationLedubUnitTests
{
    [TestFixture]
    public class UnitTestsTeamRepo
    {
        private DataContext _context;
        private ITeamRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);
            _repository = new TeamRepository(_context);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Get_ReturnsAllTeams()
        {
            var teams = new List<Team>
            {
                new() { Id = 1, Name = "Team 1" },
                new() { Id = 2, Name = "Team 2" }
            };
            _context.Teams.AddRange(teams);
            _context.SaveChanges();
            
            var result = _repository.Get();
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Team 1", result.First().Name);
            Assert.AreEqual("Team 2", result.Last().Name);
        }

        [Test]
        public void Create_AddsNewTeamWithMembers()
        {
            var team = new Team { Name = "New Team" };
            var members = new List<Member>
            {
                new() { Id = 1, Name = "Member 1", Position = "Position 1" },
                new() { Id = 2, Name = "Member 2", Position = "Position 2" },
                new() { Id = 3, Name = "Member 3", Position = "Position 3" }
            };
            _context.Members.AddRange(members);
            _context.SaveChanges();

            var memberIds = new List<int> { 1, 2 };

            var result = _repository.Create(team, memberIds);

            Assert.IsNotNull(result);
            Assert.AreEqual("New Team", result.Name);
            Assert.AreEqual(2, result.Members.Count);
        }

        [Test]
        public void Update_UpdatesExistingTeamAndMembers()
        {
            var team = new Team { Id = 1, Name = "New Team" };
            _context.Teams.Add(team);
            _context.SaveChanges();

            var members = new List<Member>
            {
                new() { Id = 1, Name = "Member 1", Position = "Position 1" },
                new() { Id = 2, Name = "Member 2", Position = "Position 2" },
                new() { Id = 3, Name = "Member 3", Position = "Position 3" },
                new() { Id = 4, Name = "Member 4", Position = "Position 4" }
            };
            _context.Members.AddRange(members);
            _context.SaveChanges();

            var memberIds = new List<int> { 3, 4 };

            var debugTeam = _context.Teams.Find(1);
            if (debugTeam == null)
            {
                System.Diagnostics.Debug.WriteLine("Team with ID 1 not found in the database.");
            }

            var result = _repository.Update(1, team, memberIds);

            Assert.IsNotNull(result);
            Assert.AreEqual("New Team", result.Name);
            Assert.AreEqual(2, result.Members.Count);
        }

        [Test]
        public void Delete_RemovesTeamAndMembers()
        {
            var team = new Team { Id = 1, Name = "Delete Team" };
            _context.Teams.Add(team);
            _context.SaveChanges();

            var members = new List<Member>
            {
                new() { Id = 1, Name = "Member 1", Position = "Position 1" },
                new() { Id = 2, Name = "Member 2", Position = "Position 2" }
            };
            _context.Members.AddRange(members);
            _context.SaveChanges();

            var debugTeam = _context.Teams.Find(1);
            if (debugTeam == null)
            {
                System.Diagnostics.Debug.WriteLine("Team with ID 1 not found in the database.");
            }

            var result = _repository.Delete(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Delete Team", result.Name);
            Assert.AreEqual(0, result.Members.Count);
        }

        [Test]
        public void Create_FailsToAddNullTeam()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Create(null, new List<int>()));
        }

        [Test]
        public void Create_FailsWithNullMemberIds()
        {
            var team = new Team { Name = "New Team" };
            
            Assert.Throws<ArgumentNullException>(() => _repository.Create(team, null));
        }

        [Test]
        public void Create_FailsWithInvalidMemberIds()
        {
            var team = new Team { Name = "New Team" };
            
            Assert.Throws<KeyNotFoundException>(() => _repository.Create(team, new List<int> { 1, 2 }));
        }

        [Test]
        public void Update_ReturnsNullForNonExistingTeam()
        {
            var team = new Team { Id = 1, Name = "New Team" };
            
            var result = _repository.Update(1, team, new List<int>());
            
            Assert.IsNull(result);
        }

        [Test]
        public void Update_FailsWithNullTeam()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Update(1, null, new List<int>()));
        }

        [Test]
        public void Update_FailsWithNullMemberIds()
        {
            var team = new Team { Id = 1, Name = "New Team" };
            
            Assert.Throws<ArgumentNullException>(() => _repository.Update(1, team, null));
        }

        [Test]
        public void Delete_ReturnsNullForNonExistingTeam()
        {
            var result = _repository.Delete(1);
            
            Assert.IsNull(result);
        }
    }
}
