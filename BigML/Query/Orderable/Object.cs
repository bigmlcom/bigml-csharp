namespace BigML.Meta.Key
{
    public class Object
    {
        readonly string _name;
        protected Object(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}