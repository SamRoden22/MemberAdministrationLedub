using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubCore.Models;
using MemberAdministrationLedubDAL;
using MemberAdministrationLedubDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAdministrationLedubUnitTests
{
    public class UnitTestsMemberRepo
    {
        private DataContext _context;
        private IMemberRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);
            _repository = new MemberRepository(_context);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void GetAll_ReturnsAllMembers()
        {
            // Arrange
            var members = new List<Member>
        {
            new Member { Id = 1, Name = "Member 1", Position = "Position 1" },
            new Member { Id = 2, Name = "Member 2", Position = "Position 2" }
        };
            _context.Members.AddRange(members);
            _context.SaveChanges();

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void Get_ReturnsMemberById()
        {
            // Arrange
            var member = new Member { Id = 1, Name = "Member 1", Position = "Position 1" };
            _context.Members.Add(member);
            _context.SaveChanges();

            // Act
            var result = _repository.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Member 1", result.Name);
        }

        [Test]
        public void Create_AddsNewMember()
        {
            // Arrange
            var member = new Member { Id = 1, Name = "New Member", Position = "New Position" };

            // Act
            var result = _repository.Create(member);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New Member", result.Name);
        }

        [Test]
        public void Update_UpdatesExistingMember()
        {
            // Arrange
            var member = new Member { Id = 1, Name = "Member 1", Position = "Position 1" };
            _context.Members.Add(member);
            _context.SaveChanges();

            var updatedMember = new Member { Id = 1, Name = "Updated Member", Position = "Updated Position" };

            // Act
            var result = _repository.Update(1, updatedMember);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Member", result.Name);
            Assert.AreEqual("Updated Position", result.Position);
        }

        [Test]
        public void Delete_RemovesMember()
        {
            // Arrange
            var member = new Member { Id = 1, Name = "Member 1", Position = "Position 1" };
            _context.Members.Add(member);
            _context.SaveChanges();

            // Act
            var result = _repository.Delete(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Member 1", result.Name);
            Assert.IsNull(_context.Members.Find(1));
        }

        [Test]
        public void GetAll_ReturnsEmptyListWhenNoMembers()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Get_ReturnsNullForNonExistingMember()
        {
            // Arrange
            var member = new Member { Id = 1, Name = "Member 1", Position = "Position 1" };
            _context.Members.Add(member);
            _context.SaveChanges();

            // Act
            var result = _repository.Get(2); // Non-existing ID

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_FailsToAddNullMember()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _repository.Create(null));
        }

        [Test]
        public void Update_ReturnsNullForNonExistingMember()
        {
            // Arrange
            var member = new Member { Id = 1, Name = "Member 1", Position = "Position 1" };
            _context.Members.Add(member);
            _context.SaveChanges();

            var updatedMember = new Member { Id = 2, Name = "Updated Member", Position = "Updated Position" }; // Non-existing ID

            // Act
            var result = _repository.Update(2, updatedMember);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_ReturnsNullForNonExistingMember()
        {
            // Act
            var result = _repository.Delete(2); // Non-existing ID

            // Assert
            Assert.IsNull(result);
        }
    }
}
