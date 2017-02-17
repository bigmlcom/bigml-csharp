using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    /// <summary>
    /// When you list a resource the response that you get back is a meta object that paginates all the resources returned in the response. 
    /// It enumerates all the resources filtered and ordered according to the criteria that you supply in your request. 
    /// </summary>
    public class Listing<T> : Response, IEnumerable<T> where T: Response, new()
    {
        /// <summary>
        /// Specifies in which page of the the listing you are, 
        /// how to get to the previous page and next page, 
        /// and the total number of resources.
        /// </summary>
        public MetaObject Meta 
        { 
            get { return new MetaObject(Object.meta); }
        }

        IEnumerable<T> Objects
        {
            get
            {
                List<T> _objects = new List<T>();
                if (Object.objects != null)
                {
                    foreach (JObject resource in Object.objects)
                    {
                        _objects.Add(new T { Object = resource });
                    }
                }
                return _objects;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}