using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    /// <summary>
    /// An ensemble is a number of models grouped together to create a stronger
    /// model with better predictive performance.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/developers/ensembles">developers</a>
    /// website.
    /// </summary>
    public partial class Ensemble : Response
    {
        /// <summary>
        /// The name of the ensemble as your provided or based on the name 
        /// of the dataset by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }


        /// <summary>
        /// The dataset/id that was used to build the ensemble.
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
        /// A description of the status of the ensemble. It includes a code, a message,
        /// and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }


        private List<string> _modelIds;

        /// <summary>
        /// All the information that you need to recreate or use the ensemble on your own. 
        /// Each model includes a very intuitive description of the tree-like structure that 
        /// made each model up and the field's dictionary describing the fields and their summaries.
        /// </summary>
        public List<string> Models
        {
            get {
                if (this._modelIds == null) { 
                     this._modelIds = new List<string>();
                    for (int i = 0; i < Object.models.Count; i++)
                    {
                        this._modelIds.Add((string) Object.models[i]);
                    }
                }
                return this._modelIds;
            }
        }

        /// <summary>
        /// All the information that you need to recreate or use the ensemble on your own. 
        /// Each model includes a very intuitive description of the tree-like structure that 
        /// made each model up and the field's dictionary describing the fields and their summaries.
        /// </summary>
        public LocalEnsemble EnsembleStructure
        {
            get { return new LocalEnsemble(Object, Object.models); }
        }
    }
}
