using System;
using System.Json;

namespace BigML
{
    public partial class Response
    {
        /// <summary>
        /// Abstract base class for Field type.
        /// </summary>
        public abstract class Field<T> where T : Response
        {
            protected readonly dynamic _entry;
            internal Field(JsonValue json)
            {
                _entry = json;
            }

            /// <summary>
            /// Specifies the number of column in the original file.
            /// </summary>
            public int Column
            {
                get { return _entry.column_number; }
            }

            /// <summary>
            /// Name of the column if provided in the header of the source or a name generated automatically otherwise.
            /// </summary>
            public string Name
            {
                get { return _entry.name; }
            }

            /// <summary>
            /// Id of the column if provided in the header of the source or a name generated automatically otherwise.
            /// </summary>
            public string Id
            {
                get { return _entry.id; }
            }

            /// <summary>
            /// Specifies the type of the field. It can be numerical, categorical, or text.
            /// </summary>
            public OpType OpType
            {
                get
                {
                    OpType result;
                    Enum.TryParse((string)_entry.optype, true, out result);
                    return result;
                }
            }
        }
    }
}