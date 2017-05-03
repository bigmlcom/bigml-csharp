using System.Threading.Tasks;
using System.IO;

namespace BigML
{
    public partial class Client
    {
        /// <summary>
        ///  Create a dataset using the supplied arguments.
        /// </summary>
        [System.Obsolete("Create is deprecated, use CreateDataset instead.")]
        public Task<DataSet> Create(DataSet.Arguments arguments)
        {
            return CreateDataset(arguments);
        }


        /// <summary>
        ///  Create a dataset using the supplied arguments.
        /// </summary>
        public Task<DataSet> CreateDataset(DataSet.Arguments arguments)
        {
            return Create<DataSet>(arguments);
        }


        /// <summary>
        /// Create a dataset.
        /// </summary>
        /// <param name="source">The source from which you want to generate a dataset.</param>
        /// <param name="name">The optional name you want to give to the new dataset. </param>
        [System.Obsolete("Create is deprecated, use CreateDataset instead.")]
        public Task<DataSet> Create(Source source, string name = null, DataSet.Arguments arguments = null)
        {
            return CreateDataset(source, name, arguments);
        }


        /// <summary>
        /// Create a dataset.
        /// </summary>
        /// <param name="source">The source from which you want to generate a dataset.</param>
        /// <param name="name">The optional name you want to give to the new dataset. </param>
        public Task<DataSet> CreateDataset(Source source, string name = null, DataSet.Arguments arguments = null)
        {
            arguments = arguments ?? new DataSet.Arguments();
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            arguments.Source = source.Resource;
            return Create<DataSet>(arguments);
        }


        /// <summary>
        /// Clone, filter or sample a dataset
        /// </summary>
        /// <param name="name">The optional name you want to give to the new dataset. </param>
        public Task<DataSet> Transform(DataSet dataset, string name = null, DataSet.Arguments arguments = null)
        {
            arguments = arguments ?? new DataSet.Arguments();
            arguments.OriginDataset = dataset.Resource;
            if (!string.IsNullOrWhiteSpace(name)) arguments.Name = name;
            return Create<DataSet>(arguments);
        }


        public Task<bool> Download(DataSet dataset, FileStream f)
        {
            return Download(dataset.Resource, f);
        }


        /// <summary>
        /// Completes the process of requests agains the server in order to
        /// create a file with a DataSet content
        /// </summary>
        /// <param name="datasetID"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<bool> DownloadDataset(string datasetID, string path)
        {
            int downloadStatus = 0;
            DataSet ds;
            FileStream fs;

            do
            {
                ds = await this.Get<DataSet>(datasetID);
                downloadStatus = (int)ds.Object["download"]["code"];
                switch (downloadStatus)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        // not started or in progress => request
                        Download(ds, null);
                        await Task.Delay(5000);
                        break;
                    case 5:
                        // finished => save
                        fs = new FileStream(path, System.IO.FileMode.Create);
                        await Download(ds, fs);
                        fs.Flush();
                        fs.Close();
                        break;
                    case -1:
                        new System.Exception("An error occurred downloading the dataset export");
                        break;
                }
            } while (downloadStatus != 5);
            return true;
        }


        public Query<DataSet.Filterable, DataSet.Orderable, DataSet> ListDataSets()
        {
            return new DataSetListing(List<DataSet>);
        }
    }
}