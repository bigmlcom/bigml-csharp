using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {

        /// <summary>
        /// Create a model using supplied arguments.
        /// </summary>
        [System.Obsolete("Create is deprecated, use CreateModel instead.")]
        public Task<Model> Create(Model.Arguments arguments)
        {
            return CreateModel(arguments);
        }

        /// <summary>
        /// Create a model using supplied arguments.
        /// </summary>
        public Task<Model> CreateModel(Model.Arguments arguments)
        {
            return Create<Model>(arguments);
        }

        /// <summary>
        /// Create a model.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new model. </param>
        /// <param name="inputFields">Specifies the ids of the fields that you want to use as predictors to create the model. </param>
        /// <param name="objectiveField">Specifies the id of the field that you want to predict.</param>
        [System.Obsolete("Create is deprecated, use CreateModel instead.")]
        public Task<Model> Create(DataSet dataset, string name = null, string[] inputFields = null, string objectiveField = null, Model.Arguments arguments = null)
        {
            return CreateModel(dataset, name, inputFields, objectiveField, arguments);
        }

        /// <summary>
        /// Create a model.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new model. </param>
        /// <param name="inputFields">Specifies the ids of the fields that you want to use as predictors to create the model. </param>
        /// <param name="objectiveField">Specifies the id of the field that you want to predict.</param>
        public Task<Model> CreateModel(DataSet dataset, string name = null, string[] inputFields = null, string objectiveField = null, Model.Arguments arguments = null)
        {
            arguments = arguments ?? new Model.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            if (inputFields != null && inputFields.Length > 0)
            {
                foreach (var input in arguments.Inputs) arguments.Inputs.Add(input);
            }
            if (!string.IsNullOrWhiteSpace(objectiveField)) arguments.Objective = objectiveField;
            arguments.DataSet = dataset.Resource;
            return Create<Model>(arguments);
        }

        /// <summary>
        /// List all models
        /// </summary>
        public Query<Model.Filterable, Model.Orderable, Model> ListModels()
        {
            return new ModelListing(List<Model>);
        }
    }
}