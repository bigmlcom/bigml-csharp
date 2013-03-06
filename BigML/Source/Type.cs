namespace BigML
{
    public partial class Source
    {
        /// <summary>
        /// The type of source.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// The source was created using a local file.
            /// </summary>
            Local = 0,

            /// <summary>
            /// The source has been created using a remote URL.
            /// </summary>
            Remote = 1,

            /// <summary>
            /// The source has been created using inline data.
            /// </summary>
            Inline = 2,
        }
    }
}