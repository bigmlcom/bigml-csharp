using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create an anomaly detector using supplied arguments.
        /// </summary>
        public Task<Ensemble> CreateEnsemble(Ensemble.Arguments arguments)
        {
            return Create<Ensemble>(arguments);
        }

        /// <summary>
        /// Create an Ensemble.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new ensemble. </param>
        /// <param name="arguments">Specifies the id of the field that you want to predict.</param>
        public Task<Ensemble> CreateEnsemble(DataSet dataset, string name = null,
                                            Ensemble.Arguments arguments = null)
        {
            arguments = arguments ?? new Ensemble.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) 
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<Ensemble>(arguments);
        }

        /// <summary>
        /// List all the anomalies
        /// </summary>
        public Query<Ensemble.Filterable, Ensemble.Orderable, Ensemble> ListEnsembles()
        {
            return new EnsembleListing(List<Ensemble>);
        }
    }
}