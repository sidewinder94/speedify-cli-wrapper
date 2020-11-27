using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.SourceGenerators.Model
{
    internal class Method
    {
        public Type ReturnType { get; set; }

        public string Name { get; set; }

        public List<Parameter> ParameterList { get; } = new List<Parameter>();
    }
}
