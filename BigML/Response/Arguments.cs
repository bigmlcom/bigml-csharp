using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace BigML
{
    public partial class Response
    {
        /// <summary>
        /// Abstract base class for resource creation arguments
        /// </summary>
        public abstract class Arguments<T> where T : Response
        {
            protected Arguments()
            {
                Tags = new HashSet<string>();
            }

            /// <summary>
            /// The category that best describes the dataset. 
            /// </summary>
            public Category Category { get; set; }

            /// <summary>
            /// A description of the resource of up to 8192 characters. 
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// The name you want to give to the new resource. 
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Whether you want your resource to be private or not.  
            /// </summary>
            public bool Private { get; set; }

            /// <summary>
            /// A list of strings that help classify and index your dataset.
            /// </summary>
            public ISet<string> Tags
            {
                get;
                private set;
            }

            public virtual JsonValue ToJson()
            {
                dynamic json = new JsonObject();

                if (Category != Category.Miscellaneous) json.category = (int)Category;
                if (!string.IsNullOrWhiteSpace(Description)) json.description = Description;
                if (!string.IsNullOrWhiteSpace(Name)) json.name = Name;
                if (!Private) json.@private = Private;
                if (Tags.Count > 0) json.tags = new JsonArray(Tags.Select(tag => (JsonValue) tag));

                return json;
            }

            public override string ToString()
            {
                return ToJson().ToString();
            }
        }
    }
}