namespace BigML
{
    /// <summary>
    /// BigML client.
    /// </summary>
    public partial class Client
    {
        readonly string _apiKey;
        readonly string _username;

        public Client(string userName, string apiKey)
        {
            _apiKey = apiKey;
            _username = userName;
        }
    }
}