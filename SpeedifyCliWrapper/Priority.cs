using System;
using System.Linq;
using System.Reflection;

namespace SpeedifyCliWrapper
{
    public class Priority
    {
        private Priority(string value) { this.Value = value; }

        public string Value { get; private set; }

        public static Priority Always => new Priority("always");
        public static Priority Backup => new Priority("backup");
        public static Priority Secondary => new Priority("secondary");
        public static Priority Never => new Priority("never");


        public static implicit operator Priority(string source)
        {
            var result = typeof(Priority).GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Select(p => (Priority)p.GetGetMethod().Invoke(null, null)).FirstOrDefault(v => v.Value == source);

            if (result == null)
            {
                throw new InvalidOperationException($"{source} is not a member of the {typeof(Priority).Name} enum class");
            }

            return result;
        }

        public static bool operator ==(Priority first, Priority second)
        {
            return first != null && first.Equals(second);
        }

        public static bool operator !=(Priority first, Priority second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            var priority = obj as Priority;
            return priority != null &&
                   this.Value == priority.Value;
        }
    }
}