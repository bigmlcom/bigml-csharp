using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create an anomaly detector using supplied arguments.
        /// </summary>
        public Task<BatchPrediction> CreateAnomaly(BatchPrediction.Arguments arguments)
        {
            return Create<BatchPrediction>(arguments);
        }

        /// <summary>
        /// Create a BatchPrediction.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new batch predictions. </param>
        /// <param name="arguments">Specifies the id of the field that you want to predict.</param>
        public Task<BatchPrediction> CreateBatchPrediction(DataSet dataset, string name = null,
                                            BatchPrediction.Arguments arguments = null)
        {
            arguments = arguments ?? new BatchPrediction.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<BatchPrediction>(arguments);
        }

        /// <summary>
        /// List all the Batch Predictions
        /// </summary>
        public Query<BatchPrediction.Filterable, BatchPrediction.Orderable, BatchPrediction> ListBatchPredictions()
        {
            return new BatchPredictionListing(List<BatchPrediction>);
        }
    }
}