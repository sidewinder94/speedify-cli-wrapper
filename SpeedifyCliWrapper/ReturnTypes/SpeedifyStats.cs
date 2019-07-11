using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyStats : ICustomJson
    {
        public SpeedifyState State { get; private set; }

        public List<SpeedifyAdapter> Adapters { get; private set; } = new List<SpeedifyAdapter>();

        public SpeedifyConnectionStats ConnectionStats { get; private set; }

        public SpeedifySessionStats SessionStats { get; private set; }

        private readonly IReadOnlyDictionary<string, MethodInfo> _accessorDictionary;

        public SpeedifyStats()
        {
            this._accessorDictionary = this.GetType().GetProperties().ToDictionary(kp => kp.Name.ToLower(), vp => vp.SetMethod);
        }


        public MethodInfo this[string part]
        {
            get
            {
                return this._accessorDictionary[part.Replace("_", "")];
            }
        }

    }
}
