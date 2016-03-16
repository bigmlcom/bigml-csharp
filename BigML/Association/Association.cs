using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// Association Discovery is a popular method to find out relations among
    /// values in high-dimensional datasets. It is commonly used for basket
    /// market analysis. This analysis seeks for customer shopping patterns
    /// across large transactional datasets. For instance, do customers who buy
    /// hamburgers and ketchup also consume bread? Businesses use those
    /// insights to make decisions on promotions and product placements.
    /// Association Discovery can also be used for other purposes such as early
    /// incident detection, web usage analysis, or software intrusion detection.
    /// 
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/developers/associations">developers</a>
    /// website.
    /// </summary>
    public partial class Association : Response
    {

        /// <summary>
        /// The name of the association as your provided or based on the name of the dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The dataset/id that was used to build the association.
        /// </summary>
        public string DataSet
        {
            get { return Object.dataset; }
        }



        /// <summary>
        /// Whether the dataset is still available or has been deleted.
        /// </summary>
        public bool DataSetStatus
        {
            get { return Object.dataset_status; }
        }


        /// <summary>
        /// A description of the status of the association. It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
