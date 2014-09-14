using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create an anomaly detector using supplied arguments.
        /// </summary>
        public Task<Anomaly> CreateAnomaly(Anomaly.Arguments arguments)
        {
            return Create<Anomaly>(arguments);
        }

        /// <summary>
        /// Create an Anomaly.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new anomaly detector. </param>
        /// <param name="arguments">Specifies the id of the field that you want to predict.</param>
        public Task<Anomaly> CreateAnomaly(DataSet dataset, string name = null,
                                            Anomaly.Arguments arguments = null)
        {
            arguments = arguments ?? new Anomaly.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) 
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<Anomaly>(arguments);
        }

        /// <summary>
        /// List all the anomalies
        /// </summary>
        public Query<Anomaly.Filterable, Anomaly.Orderable, Anomaly> ListAnomalies()
        {
            return new AnomalyListing(List<Anomaly>);
        }
    }
}