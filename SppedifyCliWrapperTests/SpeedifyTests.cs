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
