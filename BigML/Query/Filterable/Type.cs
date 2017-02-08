namespace BigML.Meta
{
    public class Type : Object
    {
        Type(string name): base(name)
        {
        }

        public static Bool operator ==(Type c1, Source.Type c2)
        {
            return default(Bool);
        }

        public static Bool operator !=(Type c1, Source.Type c2)
        {
            return default(Bool);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}