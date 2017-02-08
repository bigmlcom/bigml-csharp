namespace BigML.Meta
{
    public class Int: Object
    {
        Int(string name): base(name)
        {
        }

        public static Bool operator ==(Int c1, int c2)
        {
            return default(Bool);
        }

        public static Bool operator !=(Int c1, int c2)
        {
            return default(Bool);
        }

        public static Bool operator >(Int c1, int c2)
        {
            return default(Bool);
        }

        public static Bool operator <(Int c1, int c2)
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