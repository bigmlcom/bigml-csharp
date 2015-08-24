using BigML.Meta;

namespace BigML
{
    public partial class StatisticalTest
    {
        /// <summary>
        /// Filterable properties for statistical test
        /// </summary>
        public class Filterable : Filterable<StatisticalTest>
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
