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

        const string DefaultDomain = "bigml.io";
        const string DefaultProtocol = "https";

        public Client(string userName, string apiKey, bool devMode=false, string vpcDomain="")
        {
            _apiKey = apiKey;
            _username = userName;
            _dev = (devMode) ? "dev/" : "";
            _protocol = DefaultProtocol;
            _VpcDomain = (vpcDomain.Trim().Length > 0) ? vpcDomain : DefaultDomain;
        }
    }
}