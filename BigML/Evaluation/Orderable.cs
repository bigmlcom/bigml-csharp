using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class Evaluation
    {
        /// <summary>
        /// Orderable properties for Evaluations
        /// </summary>
        public class Orderable : Orderable<Evaluation>
        {
            /// <summary>
            /// The dataset/id that was used to build the dataset.
            /// </summary>
            public String DataSet
            {
                get { return Object.dataset; }
            }


            /// <summary>
            /// Whether the dataset is still available or has been deleted.
            /// </summary>
            public Bool DataSetStatus
            {
                get { return Object.dataset_status; }
            }
        }
    }
}