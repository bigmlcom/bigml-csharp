using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BigML
{
    /// <summary>
    /// A forecast is created using a timeseries/id and the new instance (input_data)
    /// that you want to create a forecast for.
    /// The complete and updated reference with all available parameters is in
    /// our <a href="https://bigml.com/api/forecasts">documentation</a>
    /// website.
    /// </summary>
    public partial class Forecast : Response
    {
        /// <summary>
        /// The dataset/id that was used to build the forecast.
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
        /// A dictionary with an entry per field in the input_data or forecast_path.
        /// Each entry includes the column number in original source, the name of the
        /// field, the type of the field, and the specific datatype.
        /// </summary>
        public JObject Fields
        {
            get { return Object.fields; }
        }

        /// <summary>
        /// The dictionary of input fields's ids and valued used as input for the forecast.
        /// </summary>
        public JObject InputData
        {
            get { return Object.input_data; }
        }

        /// <summary>
        /// The forecast's locale.
        /// </summary>
        public string Locale
        {
            get { return Object.locale; }
        }

        /// <summary>
        /// The timeseries/id that was used to create the forecast.
        /// </summary>
        public string TimeSeries
        {
            get { return Object.timeseries; }
        }

        /// <summary>
        /// Whether the timeseries is still available or has been deleted.
        /// </summary>
        public bool TimeSeriesStatus
        {
            get { return Object.timeseries_status; }
        }

        /// <summary>
        /// The name of the forecast as you provided or based on the name of the objective field's name by default.
        /// </summary>
        public string Name
        {
            get { return Object.name; }
        }

        /// <summary>
        /// The source/id that was used to build the dataset.
        /// </summary>
        public string Source
        {
            get { return Object.source_id; }
        }

        /// <summary>
        /// Whether the source is still available or has been deleted.
        /// </summary>
        public bool SourceStatus
        {
            get { return Object.source_status; }
        }

        /// <summary>
        /// A description of the status of the forecast.
        /// It includes a code, a message, and some extra information.
        /// </summary>
        public Status StatusMessage
        {
            get { return new Status(Object.status); }
        }
    }
}