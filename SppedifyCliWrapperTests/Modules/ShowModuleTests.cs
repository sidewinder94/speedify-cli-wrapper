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

        [TestMethod]
        public void ShowPrivacy()
        {
            var wrapper = new Speedify();

            var set = wrapper.Show.Privacy();

            Assert.IsNotNull(set);
        }

        [TestMethod]
        public void ShowAdapters()
        {
            var wrapper = new Speedify();

            var set = wrapper.Show.Adapters();

            Assert.IsNotNull(set);
            Assert.IsTrue(set.Count > 0);
        }

        [TestMethod]
        public void ShowCurrentServer()
        {
            var wrapper = new Speedify();

            var set = wrapper.Show.CurrentServer();

            Assert.IsNotNull(set);
        }
    }
}
