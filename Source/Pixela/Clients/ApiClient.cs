namespace Pixela.Clients
{
    public class ApiClient
    {
        protected PixelaClient Client { get; }

        protected ApiClient(PixelaClient client)
        {
            Client = client;
        }
    }
}