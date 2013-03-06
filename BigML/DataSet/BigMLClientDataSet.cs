using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        ///  Create a dataset using the supplied arguments.
        /// </summary>
        public Task<DataSet> Create(DataSet.Arguments arguments)
        {
            return Create<DataSet>(arguments);
        }

        /// <summary>
        /// Create a dataset.
        /// </summary>
        /// <param name="source">The source from which you want to generate a dataset.</param>
        /// <param name="name">The optional name you want to give to the new dataset. </param>
        public Task<DataSet> Create(Source source, string name = null, DataSet.Arguments arguments = null)
        {
            arguments = arguments ?? new DataSet.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            arguments.Source = source.Resource;
            return Create<DataSet>(arguments);
        }

        public Query<DataSet.Filterable, DataSet.Orderable, DataSet> ListDataSets()
        {
            return new DataSetListing(List<DataSet>);
        }
    }
}