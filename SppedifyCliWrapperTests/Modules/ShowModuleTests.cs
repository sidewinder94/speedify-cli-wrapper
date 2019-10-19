using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedifyCliWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SppedifyCliWrapperTests.Modules
{
    [TestClass]
    public class ShowModuleTests
    {
        [TestMethod]
        public void ShowConfigTest()
        {
            var wrapper = new Speedify();

            var set = wrapper.Show.Settings();

            Assert.IsNotNull(set);
        }

        [TestMethod]
        public void ShowServers()
        {
            var wrapper = new Speedify();

            var set = wrapper.Show.Servers();

            Assert.IsNotNull(set);
            Assert.IsTrue(set.Servers.Any());
        }
    }
}
