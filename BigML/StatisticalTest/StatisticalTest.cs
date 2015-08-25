using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// A statistical test resource automatically runs some advanced
    /// statistical tests on the numeric fields of a dataset. The goal of
    /// these tests is to check whether the values of individual fields or
    /// differ from some distribution patterns. Statistical test are useful in
    /// tasks such as fraud, normality, or outlier detection.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/developers/statisticaltest">
    /// developers</a> website.
    /// </summary>
    public partial class StatisticalTest : Response
    {

        /// <summary>
        /// The name of the Statistical Test as your provided or based on the name
        /// of the dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The dataset/id that was used to build the dataset.
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
        /// A description of the status of the correlation. It includes a code, a message,
        /// and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}
