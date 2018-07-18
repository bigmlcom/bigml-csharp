using BigML.Meta;

namespace BigML
{
    public partial class Fusion
    {
        /// <summary>
        /// Filterable properties for fusions
        /// </summary>
        public class Filterable : Filterable<Fusion>
        {
            /// <summary>
            /// The current number of batch predictions that use this fusion
            /// </summary>
            public Int NumberOfBatchpredictions
            {
                get { return Object.number_of_batchpredictions; }
            }

            /// <summary>
            /// The current number of evaluations that use this fusion
            /// </summary>
            public Int NumberOfEvaluations
            {
                get { return Object.number_of_evaluations; }
            }

            /// <summary>
            /// The current number of predictions that use this fusion
            /// </summary>
            public Int NumberOfPredictions
            {
                get { return Object.number_of_predictions; }
            }
        }
    }
}
