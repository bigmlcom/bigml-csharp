using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create an anomaly score using supplied arguments.
        /// </summary>
        public Task<AnomalyScore> CreateAnomaly(AnomalyScore.Arguments arguments)
        {
            return Create<AnomalyScore>(arguments);
        }

        /// <summary>
        /// Create an Anomaly Score.
        /// </summary>
        /// <param name="anomaly">An Anomaly detector instance</param>
        /// <param name="name">The name you want to give to the new anomaly score. </param>
        /// <param name="arguments">Specifies the values of the fields that you want to use.</param>
        public Task<AnomalyScore> CreateAnomalyScore(Anomaly anomaly, string name = null,
                                            AnomalyScore.Arguments arguments = null)
        {
            arguments = arguments ?? new AnomalyScore.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.Anomaly = anomaly.Resource;
            return Create<AnomalyScore>(arguments);
        }

        /// <summary>
        /// List all the anomalies scores
        /// </summary>
        public Query<AnomalyScore.Filterable, AnomalyScore.Orderable, AnomalyScore> ListScores()
        {
            return new AnomalyScoreListing(List<AnomalyScore>);
        }
    }
}