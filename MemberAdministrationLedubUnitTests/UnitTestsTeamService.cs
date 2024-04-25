using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubCore.Models;
using MemberAdministrationLedubCore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAdministrationLedubUnitTests
{
    public class UnitTestsTeamService
    {
        [Test]
        public void Get_ShouldReturnAllTeams()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var teams = new List<Team>
            {
                new Team { Id = 1, Name = "Team1" },
                new Team { Id = 2, Name = "Team2" }
            };
            mockRepository.Setup(repo => repo.Get()).Returns(teams);

            var teamService = new TeamService(mockRepository.Object);

            // Act
            var result = teamService.Get();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Team1", result[0].Name);
            Assert.AreEqual("Team2", result[1].Name);
        }

        [Test]
        public void Get_ShouldReturnSingleTeam()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var team = new Team { Id = 1, Name = "Team1" };
            mockRepository.Setup(repo => repo.Get(1)).Returns(team);

            var teamService = new TeamService(mockRepository.Object);

            // Act
            var result = teamService.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Team1", result.Name);
        }

        [Test]
        public void Create_ShouldReturnCreatedTeam()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var teamToCreate = new Team { Name = "Team1" };
            var memberIds = new List<int> { 1, 2 };
            mockRepository.Setup(repo => repo.Create(teamToCreate, memberIds)).Returns(new Team { Id = 1, Name = "Team1" });

            var teamService = new TeamService(mockRepository.Object);

            // Act
            var result = teamService.Create(teamToCreate, memberIds);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Team1", result.Name);
        }

        [Test]
        public void Update_ShouldReturnUpdatedTeam()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var updatedTeam = new Team { Id = 1, Name = "UpdatedTeam1" };
            var memberIds = new List<int> { 1, 2 };
            mockRepository.Setup(repo => repo.Update(1, updatedTeam, memberIds)).Returns(new Team { Id = 1, Name = "UpdatedTeam1" });

            var teamService = new TeamService(mockRepository.Object);

            // Act
            var result = teamService.Update(1, updatedTeam, memberIds);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("UpdatedTeam1", result.Name);
        }

        [Test]
        public void Delete_ShouldReturnDeletedTeam()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var teamToDelete = new Team { Id = 1, Name = "Team1" };
            mockRepository.Setup(repo => repo.Delete(1)).Returns(teamToDelete);

            var teamService = new TeamService(mockRepository.Object);

            // Act
            var result = teamService.Delete(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Team1", result.Name);
        }

        [Test]
        public void Get_WhenTeamNotFound_ShouldReturnNull()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            mockRepository.Setup(repo => repo.Get(1)).Returns(() => null);

            var teamService = new TeamService(mockRepository.Object);

            // Act
            var result = teamService.Get(1);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public void Create_WhenTeamIsNull_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var memberIds = new List<int> { 1, 2 };
            mockRepository.Setup(repo => repo.Create(null, memberIds)).Throws(new System.ArgumentNullException());

            var teamService = new TeamService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<System.ArgumentNullException>(() => teamService.Create(null, memberIds));
        }

        [Test]
        public void Update_WhenTeamNotFound_ShouldReturnNull()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var updatedTeam = new Team { Id = 1, Name = "UpdatedTeam1" };
            var memberIds = new List<int> { 1, 2 };
            mockRepository.Setup(repo => repo.Update(1, updatedTeam, memberIds)).Returns(() => null);

            var teamService = new TeamService(mockRepository.Object);

            // Act
            var result = teamService.Update(1, updatedTeam, memberIds);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public void Delete_WhenTeamNotFound_ShouldReturnNull()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            mockRepository.Setup(repo => repo.Delete(1)).Returns(() => null);

            var teamService = new TeamService(mockRepository.Object);

            // Act
            var result = teamService.Delete(1);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public void Create_WhenMemberIdsAreNull_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var teamToCreate = new Team { Name = "Team1" };

            var teamService = new TeamService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => teamService.Create(teamToCreate, null));
        }

        [Test]
        public void Create_WhenMemberIdsAreEmpty_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var teamToCreate = new Team { Name = "Team1" };
            var memberIds = new List<int>();

            var teamService = new TeamService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => teamService.Create(teamToCreate, memberIds));
        }

        [Test]
        public void Update_WhenMemberIdsAreNull_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var updatedTeam = new Team { Id = 1, Name = "UpdatedTeam1" };

            var teamService = new TeamService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => teamService.Update(1, updatedTeam, null));
        }

        [Test]
        public void Update_WhenMemberIdsAreEmpty_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();
            var updatedTeam = new Team { Id = 1, Name = "UpdatedTeam1" };
            var memberIds = new List<int>();

            var teamService = new TeamService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => teamService.Update(1, updatedTeam, memberIds));
        }

        [Test]
        public void Delete_WhenTeamIdIsZero_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();

            var teamService = new TeamService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => teamService.Delete(0));
        }

        [Test]
        public void Delete_WhenTeamIdIsNegative_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<ITeamRepository>();

            var teamService = new TeamService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => teamService.Delete(-1));
        }
    }
}
