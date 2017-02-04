using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// An association set is created from an association and the input_data
    /// for which you wish to create an association set.
    /// Association Sets are useful to know which items have stronger
    /// associations with a given set of values for your fields.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/associationsets">documentation</a>
    /// website.
    /// </summary>
    public partial class AssociationSet : Response
    {

        /// <summary>
        /// The name of the association set as your provided or based on the name
        /// of the association by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The association/id that was used to build the score.
        /// </summary>
        public string Association
        {
            get { return Object.association; }
        }



        /// <summary>
        /// Whether the association is still available or has been deleted.
        /// </summary>
        public bool AssociationStatus
        {
            get { return Object.association_status; }
        }


        /// <summary>
        /// A description of the status of the association set. It includes a
        /// code, a message, and some extra information.
        /// </summary>
        public Status<AssociationSet> StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
