using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Launchpad.Api.Common;
using Launchpad.Core.DTOs;
using Launchpad.Core.Managers;
using Launchpad.Core.Managers.Interfaces;
using Launchpad.Core.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Launchpad.UnitTests.ManagerTests
{
    [TestClass]
    public class LaunchpadManagerTests
    {
        private ILaunchpadManager _manager;
        private IMapper _mapper;
        private Mock<ILaunchpadService> _mockLaunchpadService;

        [TestInitialize]
        public void Initialize()
        {
            _mockLaunchpadService = new Mock<ILaunchpadService>(MockBehavior.Strict);
            _manager = new LaunchpadManager(_mapper, _mockLaunchpadService.Object);

            // Setup automappers
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = new Mapper(mapperConfig);
            Mapper.Initialize(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
        }

        [TestCleanup]
        public void Cleanup()
        {
            Mapper.Reset();
        }

        [TestMethod]
        public async Task GetAllLaunchpads_NoData()
        {
            // Arrange
            _mockLaunchpadService.Setup(m => m.GetAllLaunchpads()).ReturnsAsync(new List<SpaceXLaunchpadDto>());

            // Act
            var actual = await _manager.GetAllLaunchpads(new SearchLaunchpadDto());

            // Assert
            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public async Task GetAllLaunchpads_WithData_NoFilters()
        {
            // Arrange
            var launchpadList = new List<SpaceXLaunchpadDto>
            {
                new SpaceXLaunchpadDto
                {
                     Id = "abc",
                     Full_Name = "ABC123",
                     Status = "active"
                },
                new SpaceXLaunchpadDto
                {
                     Id = "def",
                     Full_Name = "DEF123",
                     Status = "active"
                },
                new SpaceXLaunchpadDto
                {
                     Id = "ghi",
                     Full_Name = "GHI123",
                     Status = "blah"
                }
            };
            _mockLaunchpadService.Setup(m => m.GetAllLaunchpads()).ReturnsAsync(launchpadList);

            // Act
            var actual = await _manager.GetAllLaunchpads(new SearchLaunchpadDto());

            // Assert
            Assert.AreEqual(launchpadList.Count, actual.Count);
        }

        [TestMethod]
        public async Task GetAllLaunchpads_WithData_WithNameFilter()
        {
            // Arrange
            var launchpadList = new List<SpaceXLaunchpadDto>
            {
                new SpaceXLaunchpadDto
                {
                     Id = "abc",
                     Full_Name = "ABC123",
                     Status = "active"
                },
                new SpaceXLaunchpadDto
                {
                     Id = "def",
                     Full_Name = "DEF123",
                     Status = "active"
                },
                new SpaceXLaunchpadDto
                {
                     Id = "ghi",
                     Full_Name = "GHI123",
                     Status = "blah"
                }
            };
            _mockLaunchpadService.Setup(m => m.GetAllLaunchpads()).ReturnsAsync(launchpadList);

            // Act
            var actual = await _manager.GetAllLaunchpads(new SearchLaunchpadDto { Name = "BC" });

            // Assert
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("ABC123", actual.First().Name);
        }

        [TestMethod]
        public async Task GetAllLaunchpads_WithData_WithStatusFilter()
        {
            // Arrange
            var launchpadList = new List<SpaceXLaunchpadDto>
            {
                new SpaceXLaunchpadDto
                {
                     Id = "abc",
                     Full_Name = "ABC123",
                     Status = "active"
                },
                new SpaceXLaunchpadDto
                {
                     Id = "def",
                     Full_Name = "DEF123",
                     Status = "active"
                },
                new SpaceXLaunchpadDto
                {
                     Id = "ghi",
                     Full_Name = "GHI123",
                     Status = "blah"
                }
            };
            _mockLaunchpadService.Setup(m => m.GetAllLaunchpads()).ReturnsAsync(launchpadList);

            // Act
            var actual = await _manager.GetAllLaunchpads(new SearchLaunchpadDto { Status = "active" });

            // Assert
            Assert.AreEqual(2, actual.Count);
        }
    }
}
