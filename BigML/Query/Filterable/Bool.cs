namespace BigML.Meta
{
    public class Bool : Object
    {
        Bool(string name): base(name)
        {
        }

        public static Bool operator &(Bool c1, Bool c2)
        {
            return default(Bool);
        }

        public static bool operator true(Bool c1)
        {
            return true;
        }

        public static bool operator false(Bool c1)
        {
            return false;
        }
    }
}