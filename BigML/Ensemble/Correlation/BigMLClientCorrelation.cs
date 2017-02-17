using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a correlation using supplied arguments.
        /// </summary>
        public Task<Correlation> CreateCorrelation(Correlation.Arguments arguments)
        {
            return Create<Correlation>(arguments);
        }

        /// <summary>
        /// Create a Correlation.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new correlation. </param>
        /// <param name="arguments">Other extra parameters.</param>
        public Task<Correlation> CreateCorrelation(DataSet dataset, string name = null,
                                            Correlation.Arguments arguments = null)
        {
            arguments = arguments ?? new Correlation.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<Correlation>(arguments);
        }

        /// <summary>
        /// List all the Correlations
        /// </summary>
        public Query<Correlation.Filterable, Correlation.Orderable, Correlation> ListCorrelations()
        {
            return new CorrelationListing(List<Correlation>);
        }
    }
}