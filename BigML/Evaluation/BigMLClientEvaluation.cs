using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create an evaluation using supplied arguments.
        /// </summary>
        public Task<Evaluation> CreateEvaluation(Evaluation.Arguments arguments)
        {
            return Create<Evaluation>(arguments);
        }

        /// <summary>
        /// Create an Ensemble.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new evaluation. </param>
        /// <param name="arguments">Specifies the id of the model or ensemble that you want to evaluate.</param>
        public Task<Evaluation> CreateEvaluation(DataSet dataset, string name = null,
                                            Evaluation.Arguments arguments = null)
        {
            arguments = arguments ?? new Evaluation.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<Evaluation>(arguments);
        }

        /// <summary>
        /// List all the anomalies
        /// </summary>
        public Query<Evaluation.Filterable, Evaluation.Orderable, Evaluation> ListEvaluations()
        {
            return new EvaluationListing(List<Evaluation>);
        }
    }
}