using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Creates a LogisticRegression using supplied arguments.
        /// </summary>
        public Task<LogisticRegression> CreateLogisticRegression(LogisticRegression.Arguments arguments)
        {
            return Create<LogisticRegression>(arguments);
        }

        /// <summary>
        /// Creates a Logistic Regression.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new logistic regression. </param>
        /// <param name="arguments">Specifies the id of the field that you want to predict.</param>
        public Task<LogisticRegression> CreateLogisticRegression(DataSet dataset, string name = null,
                                            LogisticRegression.Arguments arguments = null)
        {
            arguments = arguments ?? new LogisticRegression.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<LogisticRegression>(arguments);
        }

        /// <summary>
        /// List all the logistic regressions
        /// </summary>
        public Query<LogisticRegression.Filterable, LogisticRegression.Orderable, LogisticRegression> ListLogisticRegression()
        {
            return new LogisticRegressionListing(List<LogisticRegression>);
        }
    }
}