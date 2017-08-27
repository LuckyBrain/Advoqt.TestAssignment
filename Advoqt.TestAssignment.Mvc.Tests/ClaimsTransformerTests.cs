namespace Advoqt.TestAssignment.Mvc.Tests
{
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ClaimsTransformerTests
    {
        private Mock<IClaimsRepository> _mockClaimsRepository;
        private ClaimsTransformer _sit;

        [TestInitialize]
        public void ClaimsTransformer_Initialize()
        {
            _mockClaimsRepository = new Mock<IClaimsRepository>();
            _sit = new ClaimsTransformer(_mockClaimsRepository.Object);
        }

        [TestMethod]
        public void Authenticate_NoClaims_AddsRequiredClaims()
        {
            //Arrange
            _mockClaimsRepository
                .Setup(repos => repos.GetClaims(It.IsAny<string>()))
                .Returns(Enumerable.Empty<ClaimEntity>);

            //Act
            var actual = _sit.Authenticate(string.Empty, ClaimsPrincipal.Current);

            //Assert
            Assert.AreEqual(3, actual.Claims.Count());
            AssertContainsRequiredClaims(actual);
        }

        [TestMethod]
        public void Authenticate_SomeClaims_AddsMissingRequiredClaims()
        {
            //Arrange
            _mockClaimsRepository
                .Setup(repos => repos.GetClaims(It.IsAny<string>()))
                .Returns(new[]
                {
                    new ClaimEntity { TypeUrl = ClaimTypes.GivenName, Value = "Any given name" },
                    new ClaimEntity { TypeUrl = ClaimTypes.StreetAddress, Value = "Any street address" }
                });

            //Act
            var actual = _sit.Authenticate(string.Empty, ClaimsPrincipal.Current);

            //Assert
            Assert.AreEqual(4, actual.Claims.Count());
            AssertContainsRequiredClaims(actual);
        }

        [TestMethod]
        public void Authenticate_AllClaims_DoesNotAddClaims()
        {
            //Arrange
            _mockClaimsRepository
                .Setup(repos => repos.GetClaims(It.IsAny<string>()))
                .Returns(new[]
                {
                    new ClaimEntity { TypeUrl = ClaimTypes.GivenName, Value = "Any given name" },
                    new ClaimEntity { TypeUrl = ClaimTypes.Name, Value = "Any name" },
                    new ClaimEntity { TypeUrl = ClaimTypes.NameIdentifier, Value = "The name identifier" },
                    new ClaimEntity { TypeUrl = ClaimTypes.StreetAddress, Value = "Any street address" },
                    new ClaimEntity { TypeUrl = ClaimTypes.PostalCode, Value = "12345" }
                });

            //Act
            var actual = _sit.Authenticate(string.Empty, ClaimsPrincipal.Current);

            //Assert
            Assert.AreEqual(5, actual.Claims.Count());
            AssertContainsRequiredClaims(actual);
        }

        private static void AssertContainsRequiredClaims(ClaimsPrincipal actual)
        {
            Assert.IsTrue(actual.Claims.Any(c => c.Type == ClaimTypes.GivenName));
            Assert.IsTrue(actual.Claims.Any(c => c.Type == ClaimTypes.Name));
            Assert.IsTrue(actual.Claims.Any(c => c.Type == ClaimTypes.NameIdentifier));
        }
    }
}