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
        private Mock<ILaunchpadService> _mockLaunchpadService;

        [TestInitialize]
        public void Initialize()
        {
            _mockLaunchpadService = new Mock<ILaunchpadService>(MockBehavior.Strict);
        }
    }
}
