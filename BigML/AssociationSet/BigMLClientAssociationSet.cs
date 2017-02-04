using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create an association set using supplied arguments.
        /// </summary>
        public Task<AssociationSet> CreateAssociationSet(AssociationSet.Arguments arguments)
        {
            return Create<AssociationSet>(arguments);
        }

        /// <summary>
        /// Create an Association Set.
        /// </summary>
        /// <param name="association">An Association discovery instance</param>
        /// <param name="name">The name you want to give to the new association set. </param>
        /// <param name="arguments">Specifies the values of the fields that you want to use.</param>
        public Task<AssociationSet> CreateAssociationSet(Association association, string name = null,
                                            AssociationSet.Arguments arguments = null)
        {
            arguments = arguments ?? new AssociationSet.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.Association = association.Resource;
            return Create<AssociationSet>(arguments);
        }

        /// <summary>
        /// List all the associations sets
        /// </summary>
        public Query<AssociationSet.Filterable, AssociationSet.Orderable, AssociationSet> ListAssociationSets()
        {
            return new AssociationSetListing(List<AssociationSet>);
        }
    }
}