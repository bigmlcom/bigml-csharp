namespace BigML.Meta
{
    public class Filterable<S> where S : Response
    {
        protected dynamic Object = new JsonValue();

        /// <summary>
        /// Categories that help classify this resource according to the domain of application.
        /// </summary>
        public Category Category { get { return Object.category; } }

        /// <summary>
        /// This is the date and time in which the source was created with microsecond precision.
        /// </summary>
        public DateTimeOffset Created { get { return Object.created; } }

        /// <summary>
        /// The numbers of credits that has cost you to create this source.
        /// </summary>
        public Double Credits { get { return Object.credits; } }

        /// <summary>
        /// The name of the resource as your provided.
        /// </summary>
        public String Name { get { return Object.name; } }

        /// <summary>
        /// Whether the resource is public or not. 
        /// In a future version, you will be able to share resources with other coworkers or, if desired, make them publically available.
        /// </summary>
        public Bool Private { get { return Object.is_private; } }

        /// <summary>
        /// The number of bytes of the source.
        /// </summary>
        public Int Size { get { return Object.size; } }

        /// <summary>
        /// This is the date and time in which the source was last updated with microsecond precision.
        /// </summary>
        public DateTimeOffset Updated { get { return Object.updated; } }
    }
}