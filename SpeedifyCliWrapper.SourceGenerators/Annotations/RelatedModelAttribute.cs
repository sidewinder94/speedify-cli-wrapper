using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.SourceGenerators.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RelatedModelAttribute : Attribute
    { 
        public RelatedModelAttribute(Type model)
        {
            Model = model;
        }

        public Type Model { get; }
    }
}
