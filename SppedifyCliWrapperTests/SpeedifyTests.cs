using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        [TestMethod]
        public void HandleCustomJsonTest()
        {
            var str = @"[""state"",
                        {
                            ""state"":	""CONNECTED""
                        }
                        ]
                        ";
            var pop = new SpeedifyStats();

            var spd = new Speedify();

            spd.HandleCustomJson(str, pop);

            Assert.IsNotNull(pop.State);

        }

        [TestMethod]
        public void StatsTest()
        {
            var wrapper = new Speedify();

            var updateCount = 0;

            var pop = wrapper.Stats();

            pop.PropertyChanged += (sender, args) => updateCount++;

            wrapper.RefreshStats(pop, 60);

            Assert.IsNotNull(pop);
            Assert.IsNotNull(pop.State);
            Assert.IsTrue(updateCount > 3);
        }
    }
}
