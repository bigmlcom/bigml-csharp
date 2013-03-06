using System;
using System.Dynamic;
using System.Reflection;

namespace BigML.Meta
{
    public class JsonValue : DynamicObject
    {
        string _name = ".ctor";
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = new JsonValue() { _name = binder.Name };
            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = Activator.CreateInstance
                (binder.Type, BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { _name }, null);
            return true;
        }
    }
}