namespace BigML.Meta
{
    public class String : Object
    {
        String(string name): base(name)
        {
        }

        public static Bool operator ==(String c1, string c2)
        {
            return default(Bool);
        }

        public static Bool operator !=(String c1, string c2)
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