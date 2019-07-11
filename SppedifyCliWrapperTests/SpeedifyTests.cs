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

            var r = wrapper.Stats(10);

            Assert.IsNotNull(r);
            Assert.IsNotNull(r.State);
        }


        [TestMethod]
        public void DeserializationTest()
        {
            var str = @"[""state"",
                        {
                            ""state"":	""CONNECTED""
                        }
                        ]
                        ";

            var obj = JsonConvert.DeserializeObject(str);
            var obj2 = ((JArray)obj).Children();
            var obj3 = obj2.Skip(1).First().ToObject<SpeedifyState>();

            Assert.IsNotNull(obj3);
        }
    }
}
