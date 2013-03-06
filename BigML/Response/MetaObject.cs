using System.Json;

namespace BigML
{
        /// <summary>
        /// Meta object that paginates all the values returned in the response
        /// </summary>
        public class MetaObject
        {
            readonly dynamic _meta;
            internal MetaObject(JsonValue json)
            {
                _meta = json;
            }

            /// <summary>
            /// Maximum number of values that you will get in the objects field.
            /// </summary>
            public int Limit
            {
                get { return _meta.limit; }
            }

            /// <summary>
            /// Path to get the next page or null if there is no next page.
            /// </summary>
            public string Next
            {
                get { return _meta.next; }
            }

            /// <summary>
            /// How far off from the first value in your value is the first value in the objects field.
            /// </summary>
            public int Offset
            {
                get { return _meta.offset; }
            }

            /// <summary>
            /// Path to get the previous page, or null if there is no previous page.
            /// </summary>
            public string Previous
            {
                get { return _meta.previous; }
            }

            /// <summary>
            /// Total number of elements.
            /// </summary>
            public int TotalCount
            {
                get { return _meta.total_count; }
            }
        }
}