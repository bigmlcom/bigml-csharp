using System.Json;

namespace BigML
{
    public partial class DataSet
    {
        /// <summary>
        /// Before a dataset is successfully created, BigML.io makes sure that it has been uploaded in an understandable format, 
        /// that the data that it contains is parseable, and that the types for each column in the data can be inferred successfully. 
        /// The dataset goes through a number of states until all these analyses are completed. 
        /// Through the status field in the dataset you can determine when the dataset has been fully processed and ready to be used to create a model. 
        /// </summary>
        public class Status : Status<DataSet>
        {
            internal Status(JsonValue json)
                : base(json)
            {
            }

            /// <summary>
            /// Number of bytes processed so far.
            /// </summary>
            public int Bytes
            {
                get { return _status.bytes; }
            }

            /// <summary>
            /// Information about ill-formatted fields that includes the total format errors for the field and a sampling of the ill-formatted tokens.
            /// </summary>
            public JsonArray FieldErrors
            {
                get { return _status.field_errors; }
            }

            /// <summary>
            /// Information about ill-formatted rows. 
            /// It includes the total row-format errors and a sampling of the ill-formatted rows.
            /// </summary>
            public JsonArray RowFormatErrors
            {
                get { return _status.row_format_errors; }
            }

            /// <summary>
            /// The number of rows serialized so far.
            /// </summary>
            public int SerializedRows
            {
                get { return _status.serialized_rows; }
            }
        }
    }
}