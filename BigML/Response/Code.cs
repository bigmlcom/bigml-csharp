namespace BigML
{
    /// <summary>
    /// The different status codes BigML.io sends in responses. 
    /// https://bigml.com/developers/status_codes#resource_statuses
    /// (we do not make this strongly typed)
    /// </summary>
    public enum Code
    {
        /// <summary>
        /// The resource is waiting for another resource to be finished before BigML.io can start processing it.
        /// </summary>
        Waiting = 0,

        /// <summary>
        /// The task that is going to create the resource has been accepted but has been queued because there are other tasks using the system.
        /// </summary>
        Queued = 1,

        /// <summary>
        /// The task to create the resource has been is started and you should expect partial results soon.
        /// </summary>
        Started = 2,

        /// <summary>
        /// The task has computed the first partial resource but still needs to do more computations.
        /// </summary>
        InProgress = 3,

        /// <summary>
        /// This status is specific for datasets. It happens when the dataset has been computed but its data has not been serialized yet. 
        /// The dataset is final but you cannot use it yet to create a model or if you use it the model will be waiting until the dataset is finished.
        /// </summary>
        Summarized = 4,

        /// <summary>
        /// The task is completed and the resource is final.
        /// </summary>
        Finished = 5,

        /// <summary>
        /// The task has failed. We either could not process the task as you requested it or have an internal issue.
        /// </summary>
        Faulty = -1,

        /// <summary>
        /// The task has reached a state that we cannot verify at this time. This a status you should never see unless that BigML.io suffered a major downtime.
        /// </summary>
        Unknown = -2,

        /// <summary>
        /// The task has reached a faulty state because a network or computer error occurred, or because one the resources needed was not ready yet. 
        /// If you repeat the request it might work this time.
        /// </summary>
        Runnable = -3,

        /// <summary>
        /// Missing parameter.
        /// </summary>
        MissingParameter = -1200,

        /// <summary>
        /// Invalid Id.
        /// </summary>
        InvalidId = -1201,

        /// <summary>
        /// Field Error.
        /// </summary>
        FieldError = -1203,

        /// <summary>
        /// Bad Request.
        /// </summary>
        BadRequest = -1204,

        /// <summary>
        /// Value Error.
        /// </summary>
        ValueError = -1205,

        /// <summary>
        /// Validation Error.
        /// </summary>
        ValidationError = -1206,

        /// <summary>
        /// Unsupported Format.
        /// </summary>
        UnsupportedFormat = -1207,

        /// <summary>
        /// Invalid Sort Error.
        /// </summary>
        InvalidSort = -1208,
    }
}