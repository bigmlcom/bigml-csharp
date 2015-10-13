using BigML.Meta;

namespace BigML
{
    using Meta.Key;

    public partial class StatisticalTest
    {
        /// <summary>
        /// Orderable properties for statistical test
        /// </summary>
        public class Orderable : Orderable<StatisticalTest>
        {
            /// <summary>
            /// The dataset/id that was used to build the statistical test.
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