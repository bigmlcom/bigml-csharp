using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a model using supplied arguments.
        /// </summary>
        public Task<Cluster> Create(Cluster.Arguments arguments)
        {
            return Create<Cluster>(arguments);
        }

        /// <summary>
        /// Create a cluster.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new model. </param>
        /// <param name="inputFields">Specifies the ids of the fields that you want to use as predictors to create the model. </param>
        /// <param name="objectiveField">Specifies the id of the field that you want to predict.</param>
        public Task<Cluster> Create(DataSet dataset, string name = null, string[] inputFields = null, 
                                    string objectiveField = null, Cluster.Arguments arguments = null)
        {
            arguments = arguments ?? new Cluster.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            if (inputFields != null && inputFields.Length > 0)
            {
                foreach(var input in arguments.Inputs) arguments.Inputs.Add(input);
            }
            if (!string.IsNullOrWhiteSpace(objectiveField)) arguments.Objective = objectiveField;
            arguments.DataSet = dataset.Resource;
            return Create<Cluster>(arguments);
        }

        /// <summary>
        /// List all models
        /// </summary>
        public Query<Cluster.Filterable, Cluster.Orderable, Cluster> ListModels()
        {
            return new ClusterListing(List<Cluster>);
        }
    }
}