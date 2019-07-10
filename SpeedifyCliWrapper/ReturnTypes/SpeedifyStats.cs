using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyStats
    {
        public SpeedifyState State { get; private set; }

        public List<SpeedifyAdapter> Adapters { get; } = new List<SpeedifyAdapter>();

        public SpeedifyConnectionStats ConnectionStats { get; private set; }

        public SpeedifySessionStats SessionStats { get; private set; }
    }
}
