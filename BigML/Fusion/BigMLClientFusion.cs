using System.Threading.Tasks;
using System.Collections.Generic;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        /// Create a fusion using supplied arguments.
        /// </summary>
        public Task<Fusion> CreateFusion(Fusion.Arguments arguments)
        {
            return Create<Fusion>(arguments);
        }

        /// <summary>
        /// Create a Fusion.
        /// </summary>
        /// <param name="models">A list of Models</param>
        /// <param name="name">The name you want to give to the new fusion.</param>
        /// <param name="arguments">Other extra parameters.</param>
        public Task<Fusion> CreateFusion(List<dynamic> models, string name = null,
                                            Fusion.Arguments arguments = null)
        {
            arguments = arguments ?? new Fusion.Arguments();
            if (!string.IsNullOrWhiteSpace(name))
                arguments.Name = name;
            arguments.Models = models;
            return Create<Fusion>(arguments);
        }

        /// <summary>
        /// List all the Fusion
        /// </summary>
        public Query<Fusion.Filterable, Fusion.Orderable, Fusion> ListFusion()
        {
            return new FusionListing(List<Fusion>);
        }
    }
}