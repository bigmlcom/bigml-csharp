using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

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
                DynArgs = new Dictionary<string, dynamic>();
            }

            /// <summary>
            /// Dynamic Arguments (internal usage)
            /// </summary>
            private Dictionary<string, dynamic> DynArgs { get; set; }

            public Arguments<T> Add(string name, dynamic value)
            {
                DynArgs.Add(name, value);
                return this;
            }

            public Arguments<T> Remove(string name)
            {
                DynArgs.Remove(name);
                return this;
            }

            public Arguments<T> Update(string name, dynamic value)
            {
                DynArgs.Remove(name);
                DynArgs.Add(name, value);
                return this;
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

            public virtual JObject ToJson()
            {
                dynamic json = new JObject();

                if (Category != Category.Miscellaneous) json.category = (int)Category;
                if (!string.IsNullOrWhiteSpace(Description)) json.description = Description;
                if (!string.IsNullOrWhiteSpace(Name)) json.name = Name;
                if (Tags.Count > 0) json.tags = new JArray(Tags.Select(tag => (JValue) tag));
                foreach (KeyValuePair<string, dynamic> entry in DynArgs)
                {
                    dynamic inObjectVal;
                    System.Type valType = entry.Value.GetType();
                    if (valType.IsPrimitive || (valType == typeof(string)))
                    {
                        inObjectVal = (JValue)entry.Value;
                    }
                    else if (!valType.IsClass || valType == typeof(Newtonsoft.Json.Linq.JObject))
                    {
                        inObjectVal = entry.Value;
                    }
                    else
                    {
                        continue;
                    }
                    json[entry.Key] = inObjectVal;
                }

                return json;
            }

            public override string ToString()
            {
                return ToJson().ToString();
            }
        }
    }
}
