using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create an association using supplied arguments.
        /// </summary>
        public Task<Association> CreateAssociation(Association.Arguments arguments)
        {
            return Create<Association>(arguments);
        }

        /// <summary>
        /// Create an association.
        /// </summary>
        /// <param name="dataset">A DataSet instance</param>
        /// <param name="name">The name you want to give to the new association. </param>
        /// <param name="arguments">An object with more association parameters.</param>
        public Task<Association> CreateAssociation(DataSet dataset, string name = null,
                                            Association.Arguments arguments = null)
        {
            arguments = arguments ?? new Association.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            arguments.DataSet = dataset.Resource;
            return Create<Association>(arguments);
        }

        /// <summary>
        /// List all associations
        /// </summary>
        public Query<Association.Filterable, Association.Orderable, Association> ListAssociations()
        {
            return new AssociationListing(List<Association>);
        }
    }
}