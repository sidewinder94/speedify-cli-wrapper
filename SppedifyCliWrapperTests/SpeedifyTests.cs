using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedifyCliWrapper;
using SpeedifyCliWrapper.ReturnTypes;

namespace SppedifyCliWrapperTests
{
    [TestClass]
    public class SpeedifyTests
    {
        [TestMethod]
        public void VersionTest()
        {
            Speedify wrapper = new Speedify();

            var version = wrapper.Version();

            Assert.IsNotNull(version);
            Assert.IsInstanceOfType(version, typeof(SpeedifyVersion));
        }
    }
}
