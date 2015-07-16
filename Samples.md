Examples / Gists
-----------------

You can view some code examples in the Gists folder or in these links below:


### Create a model from an Azure source
This gist use a CSV file stored in an Azure space, BigML download it, create a dataset and a model from it.

See code [here](Gists/ModelFromAzureSource.cs).

### Create a cluster in development mode using a source in Azure
This gist creates a user connection in Development mode, as the previous one use a source from Azure.
Creates a dataset from it and a cluster of their values.

See code [here](Gists/DevClusterFromAzureSource.cs).

### Calculate score with a know anomaly
In this gist show how to retrieve an anomaly detector previously created in BigML (retrievement is very similar for every kind of resources) and creates an score for a set of data.

See code [here](Gists/ScoreAfterRetrieveAnomaly.cs).

### Predict with an Iris ensemble
A very simple example of how to create a prediction with an ensemble (for model is close to be equal) that
you previously created. In this example the ensemble resource is not retrieved.

See code [here](Gists/PredictionWithIrisEnsemble.cs).
