using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a statisticalTest using supplied arguments.
        /// </summary>
        public Task<StatisticalTest> CreateStatisticalTest(StatisticalTest.Arguments arguments)
        {
            return Create<StatisticalTest>(arguments);
        }

        /// <summary>
        /// Create a StatisticalTest.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new correlation. </param>
        /// <param name="arguments">Other extra parameters.</param>
        public Task<StatisticalTest> CreateStatisticalTest(DataSet dataset, string name = null,
                                            StatisticalTest.Arguments arguments = null)
        {
            arguments = arguments ?? new StatisticalTest.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<StatisticalTest>(arguments);
        }

        /// <summary>
        /// List all the StatisticalTest
        /// </summary>
        public Query<StatisticalTest.Filterable, StatisticalTest.Orderable, StatisticalTest> ListStatisticalTest()
        {
            return new StatisticalTestListing(List<StatisticalTest>);
        }
    }
}