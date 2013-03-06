namespace BigML.Filterable
{
    /// <summary>
    /// Filterable type for boolean.
    /// </summary>
    public struct Bool
    {
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