namespace BigML.Meta
{
    public abstract class Object
    {
        string _name;
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