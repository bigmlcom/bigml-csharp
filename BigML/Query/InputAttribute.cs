using System;

namespace BigML
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class InputAttribute : Attribute
    {
    }
}