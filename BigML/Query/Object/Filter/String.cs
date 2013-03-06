namespace BigML.Filterable
{
    /// <summary>
    /// queryable type for string.
    /// </summary>
    public struct String
    {
        public static Bool operator ==(String c1, string c2)
        {
            return default(Bool);
        }

        public static Bool operator !=(String c1, string c2)
        {
            return default(Bool);
        }
    }
}