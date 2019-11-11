using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpeedifyCliWrapper;
using SpeedifyCliWrapper.Enums;
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

        [TestMethod]
        public void StatsTest()
        {
            var wrapper = new Speedify();

            var updateCount = 0;

            var pop = wrapper.Stats();

            Assert.IsNotNull(pop);
            Assert.IsNotNull(pop.State);

            pop.PropertyChanged += (sender, args) => updateCount++;

            wrapper.RefreshStats(pop, 15);

            Assert.IsNotNull(pop);
            Assert.IsNotNull(pop.State);
            Assert.IsTrue(updateCount > 3);
        }

        [TestMethod]
        public void SwitchModes()
        {
            var wrapper = new Speedify();

            var set = wrapper.Show.Settings();

            var originalMode = set.BondingMode;

            var notUsedString = Enum.GetNames(typeof(BondingMode)).First(bm => bm != originalMode.ToString());

            wrapper.Mode((BondingMode)Enum.Parse(typeof(BondingMode), notUsedString));

            Thread.Sleep(15);

            wrapper.Mode(originalMode);

            Assert.IsNotNull(set);
        }
    }
}
