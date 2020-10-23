WhizzML
=======

WhizzML is a domain-specific language for automating Machine Learning
workflows, implementing high-level Machine Learning algorithms,
and easily sharing them with others. WhizzML offers out-of-the-box
scalability, abstracts away the complexity of underlying infrastructure,
and helps analysts, developers, and scientists reduce the burden of repetitive
and time-consuming analytics tasks.

Create a script
---------------

WhizzML’s main elements are scripts that contain sentences to create,
transform and manage BigML’s resources. To know more about its syntax you can
refer to the examples, tutorials and references in
[WhizzML Page](https://bigml.com/whizzml). The following one
is a simple example about how to create a script that allows
to import a source using WhizzML.
The core of the script is its `source_code` and contains the code that will
be executed when running the script.

``` {.csharp}
// setting the parameters to be used in script creation
Script.Arguments scArgs = new Script.Arguments();
scArgs.Add("source_code",
           "(create-source {\"remote\" \"https://static.bigml.com/csv/iris.csv\"})");
scArgs.Add("name", "add a remote file");
Script sc = await client.CreateScript(scArgs);
```

Create an execution
-------------------

This section shows how to create an execution of a script stored in BigML.
You only need to know the Id of the script you want to execute
and provide the input parameters for the actual execution.

``` {.csharp}

// --- Retrieve an existing script whose Id is known ---
string scriptId = "script/50a2eac63c19200bd1000XXX";

Execution.Arguments exArgs = new Execution.Arguments();
exArgs.Add("script", scriptId);
exArgs.Add("name", "my script execution");
Execution exec = await client.CreateExecution(exArgs);
```

That’s all! BigML will create the execution as requested. That generates an
`execution` resource that automatically appears in the BigML Dashboard
executions listings. Once the execution reaches a `finished` status, the
results are returned in the `Results` property of the `Execution` object.
