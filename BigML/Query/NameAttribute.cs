using System;

namespace BigML
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    internal sealed class NameAttribute : Attribute
    {
        public NameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}