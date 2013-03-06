namespace BigML.Meta
{
    public class Category : Object
    {
        Category(string name): base(name)
        {
        }

        public static Bool operator ==(Category c1, /*BigML.Category*/ int c2)
        {
            return default(Bool);
        }

        public static Bool operator !=(Category c1, /*BigML.Category*/ int c2)
        {
            return default(Bool);
        }
    }
}