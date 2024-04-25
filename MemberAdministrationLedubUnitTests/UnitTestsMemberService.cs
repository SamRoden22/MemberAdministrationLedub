using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubCore.Models;
using MemberAdministrationLedubCore.Services;
using Moq;

namespace MemberAdministrationLedubUnitTests
{
    public class UnitTestsMemberService
    {
        [Test]
        public void GetAll_ShouldReturnAllMembers()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            var members = new List<Member>
            {
                new Member { Id = 1, Name = "John Doe", Position = "Outside" },
                new Member { Id = 2, Name = "Jane Doe", Position = "Outside" }
            };
            mockRepository.Setup(repo => repo.GetAll()).Returns(members);

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("John Doe", result.First().Name);
            Assert.AreEqual("Outside", result.Last().Position);
        }

        [Test]
        public void Get_ShouldReturnSingleMember()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            var member = new Member { Id = 1, Name = "John Doe", Position = "Outside" };
            mockRepository.Setup(repo => repo.Get(1)).Returns(member);

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("John Doe", result.Name);
        }

        [Test]
        public void Create_ShouldReturnCreatedMember()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            var memberToCreate = new Member { Name = "John Doe", Position = "Outside" };
            mockRepository.Setup(repo => repo.Create(memberToCreate)).Returns(new Member { Id = 1, Name = "John Doe", Position = "Outside" });

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.Create(memberToCreate);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("John Doe", result.Name);
        }

        [Test]
        public void Update_ShouldReturnUpdatedMember()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            var memberToUpdate = new Member { Id = 1, Name = "John Doe", Position = "Outside" };
            mockRepository.Setup(repo => repo.Update(1, memberToUpdate)).Returns(new Member { Id = 1, Name = "Updated John Doe", Position = "Middle" });

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.Update(1, memberToUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Updated John Doe", result.Name);
            Assert.AreEqual("Middle", result.Position);
        }

        [Test]
        public void Delete_ShouldReturnDeletedMember()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            var memberToDelete = new Member { Id = 1, Name = "John Doe", Position = "Outside" };
            mockRepository.Setup(repo => repo.Delete(1)).Returns(memberToDelete);

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.Delete(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual("John Doe", result.Name);
            Assert.AreEqual("Outside", result.Position);
        }

        [Test]
        public void GetAll_WhenRepositoryReturnsNull_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(new List<Member>());

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.GetAll();

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Get_WhenMemberNotFound_ShouldReturnNull()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            mockRepository.Setup(repo => repo.Get(1)).Returns(() => null);

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.Get(1);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public void Create_WhenMemberIsNull_ShouldThrowException()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            mockRepository.Setup(repo => repo.Create(null)).Throws(new ArgumentNullException());

            var memberService = new MemberService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => memberService.Create(null));
        }

        [Test]
        public void Update_WhenMemberNotFound_ShouldReturnNull()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            var memberToUpdate = new Member { Id = 1, Name = "John Doe", Position = "Outside" };
            mockRepository.Setup(repo => repo.Update(1, memberToUpdate)).Returns(() => null);

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.Update(1, memberToUpdate);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public void Delete_WhenMemberNotFound_ShouldReturnNull()
        {
            // Arrange
            var mockRepository = new Mock<IMemberRepository>();
            mockRepository.Setup(repo => repo.Delete(1)).Returns(() => null);

            var memberService = new MemberService(mockRepository.Object);

            // Act
            var result = memberService.Delete(1);

            // Assert
            Assert.Null(result);
        }
    }
}
