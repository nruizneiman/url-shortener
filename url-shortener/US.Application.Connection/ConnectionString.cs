namespace US.Application.Connection
{
    public static class ConnectionString
    {
        public static string DefaultConnectionString => "mongodb+srv://usappuser:4twfBelps5u3ke2C@url-shortener-0gfos.gcp.mongodb.net/test?retryWrites=true&w=majority";
        public static string DbName => "Url";
        public static string CollectionName => "ShortUrl";
    }
}
