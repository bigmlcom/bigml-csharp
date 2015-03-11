using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a sample using supplied arguments.
        /// </summary>
        public Task<Sample> CreateSample(Sample.Arguments arguments)
        {
            return Create<Sample>(arguments);
        }

        /// <summary>
        /// Create a sample.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new dataset sample. </param>
        /// <param name="arguments">The other parameters you want to specify (description, tags, etc.)</param>
        public Task<Sample> CreateSample(DataSet dataset, string name = null,
                                            Sample.Arguments arguments = null)
        {
            arguments = arguments ?? new Sample.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<Sample>(arguments);
        }

        /// <summary>
        /// List all the samples
        /// </summary>
        public Query<Sample.Filterable, Sample.Orderable, Sample> ListSamples()
        {
            return new SampleListing(List<Sample>);
        }
    }
}