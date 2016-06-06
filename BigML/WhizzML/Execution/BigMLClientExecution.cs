using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        ///  Create a whizzml execution using the supplied arguments.
        /// </summary>
        public Task<Execution> CreateExecution(Execution.Arguments arguments)
        {
            return Create<Execution>(arguments);
        }

        /// <summary>
        /// Create a dataset.
        /// </summary>
        /// <param name="script">The source from which you want to generate a dataset.</param>
        /// <param name="name">The optional name you want to give to the new dataset. </param>
        public Task<Execution> CreateExecution(Script script, string name = null, Execution.Arguments arguments = null)
        {
            arguments = arguments ?? new Execution.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            arguments.Script = script;
            return Create<Execution>(arguments);
        }


        public Query<Execution.Filterable, Execution.Orderable, Execution> ListExecutions()
        {
            return new ExecutionListing(List<Execution>);
        }
    }
}