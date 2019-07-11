using System.Reflection;

namespace SpeedifyCliWrapper
{
    public interface ICustomJson
    {
        MethodInfo this[string part] { get; }
    }
}