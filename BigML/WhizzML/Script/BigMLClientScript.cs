using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        ///  Create a whizzml script using the supplied arguments.
        /// </summary>
        public Task<Script> CreateScript(Script.Arguments arguments)
        {
            return Create<Script>(arguments);
        }

        /// <summary>
        /// Create a dataset.
        /// </summary>
        /// <param name="sourceCode">The source from which you want to generate a dataset.</param>
        /// <param name="name">The optional name you want to give to the new dataset. </param>
        public Task<Script> CreateScript(string sourceCode, string name = null, Script.Arguments arguments = null)
        {
            arguments = arguments ?? new Script.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            arguments.SourceCode = sourceCode;
            return Create<Script>(arguments);
        }


        public Query<Script.Filterable, Script.Orderable, Script> ListScripts()
        {
            return new ScriptListing(List<Script>);
        }
    }
}