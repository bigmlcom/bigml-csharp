using System.Threading.Tasks;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        ///  Create a whizzml library using the supplied arguments.
        /// </summary>
        public Task<Library> CreateLibrary(Library.Arguments arguments)
        {
            return Create<Library>(arguments);
        }

        /// <summary>
        /// Create a dataset.
        /// </summary>
        /// <param name="script">The source from which you want to generate a dataset.</param>
        /// <param name="name">The optional name you want to give to the new dataset. </param>
        public Task<Library> CreateLibrary(string name = null, Library.Arguments arguments = null)
        {
            arguments = arguments ?? new Library.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            return Create<Library>(arguments);
        }


        public Query<Library.Filterable, Library.Orderable, Library> ListLibraries()
        {
            return new LibraryListing(List<Library>);
        }
    }
}