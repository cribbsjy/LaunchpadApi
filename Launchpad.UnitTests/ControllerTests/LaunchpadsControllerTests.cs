using Launchpad.Api.Controllers;
using Launchpad.Core.Managers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Launchpad.UnitTests.ControllerTests
{
    [TestClass]
    public class LaunchpadsControllerTests
    {
        private LaunchpadsController _controller;
        private Mock<ILaunchpadManager> _mockLaunchpadManager;

        [TestInitialize]
        public void Initialize()
        {
            _mockLaunchpadManager = new Mock<ILaunchpadManager>(MockBehavior.Strict);
        }
    }
}
