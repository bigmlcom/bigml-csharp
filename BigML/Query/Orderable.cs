namespace BigML.Meta
{
    /// <summary>
    /// Base class for all orderable properties on BigML resources
    /// </summary>
    public class Orderable<S> where S : Response
    {
        protected dynamic Object = new JsonValue();

        /// <summary>
        /// Categories that help classify this resource according to the domain of application.
        /// </summary>
        public Key.Category Category { get { return Object.category; } }

        /// <summary>
        /// This is the date and time in which the source was created with microsecond precision.
        /// </summary>
        public Key.DateTimeOffset Created { get { return Object.created; } }

        /// <summary>
        /// The name of the source as your provided.
        /// </summary>
        public Key.String Name { get { return Object.name; } }

        /// <summary>
        /// The number of bytes of the source.
        /// </summary>
        public Key.Int Size { get { return Object.size; } }

        /// <summary>
        /// Whether the source is public or not. 
        /// In a future version, you will be able to share sources with other coworkers or, if desired, make them publically available.
        /// </summary>
        public Key.Bool Private { get { return Object.@private; } }

        /// <summary>
        /// This is the date and time in which the source was last updated with microsecond precision.
        /// </summary>
        public Key.DateTimeOffset Updated { get { return Object.updated; } }
    }
}