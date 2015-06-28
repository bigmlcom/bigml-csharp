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

        public Client(string userName, string apiKey, bool devMode=false)
        {
            _apiKey = apiKey;
            _username = userName;
            _dev = (devMode == true) ? "dev/" : "";
        }
    }
}