namespace BigML
{
    /// <summary>
    /// BigML client.
    /// </summary>
    public partial class Client
    {
        readonly string _apiKey;
        readonly string _username;
        readonly string _dev;
        readonly string _protocol;
        readonly string _VpcDomain;
        readonly string _projectID;
        readonly string _organizationID;
        readonly bool _useContextInAwaits;
        readonly bool _debug;

        const string DefaultDomain = "bigml.io";
        const string DefaultProtocol = "https";

        public Client(string userName, string apiKey, string projectID="",
                      string organizationID="",
                      string vpcDomain="", bool useContextInAwaits = true,
                      bool devMode = false, bool debug = false)
        {
            _apiKey = apiKey;
            _username = userName;
            _dev = "";
            _protocol = DefaultProtocol;
            _VpcDomain = (vpcDomain.Trim().Length > 0) ? vpcDomain : DefaultDomain;
            _projectID = projectID;
            _organizationID = organizationID;
            _useContextInAwaits = useContextInAwaits;
            _debug = debug;
        }
    }
}