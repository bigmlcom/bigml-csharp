using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a Batch Score using supplied arguments.
        /// </summary>
        public Task<BatchAnomalyScore> CreateBatchAnomalyScore(BatchAnomalyScore.Arguments arguments)
        {
            return Create<BatchAnomalyScore>(arguments);
        }

        /// <summary>
        /// Create a Batch Score.
        /// </summary>
        /// <param name="anomaly">A Anomaly instance</param>
        /// <param name="name">The name you want to give to the new batch score. </param>
        /// <param name="arguments">Specifies other arguments for the batch score.</param>
        public Task<BatchAnomalyScore> CreateBatchAnomalyScore(Anomaly anomaly, string name = null,
                                            BatchAnomalyScore.Arguments arguments = null)
        {
            arguments = arguments ?? new BatchAnomalyScore.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) 
                arguments.Name = name;
            arguments.Anomaly = anomaly.Resource;
            return Create<BatchAnomalyScore>(arguments);
        }

        /// <summary>
        /// List all the batch scores
        /// </summary>
        public Query<BatchAnomalyScore.Filterable, BatchAnomalyScore.Orderable, BatchAnomalyScore> ListBatchAnomalyScores()
        {
            return new BatchAnomalyScoreListing(List<BatchAnomalyScore>);
        }
    }
}