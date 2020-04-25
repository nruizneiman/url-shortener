namespace US.Application.Connection
{
    public static class ConnectionString
    {
        // ============================= MONGO ============================= //
        public static string MongoConnectionString => "mongodb+srv://usappuser:4twfBelps5u3ke2C@url-shortener-0gfos.gcp.mongodb.net/test?retryWrites=true&w=majority";
        public static string MongoDbName => "Url";
        public static string MongoCollectionName => "ShortUrl";


        // ============================= SQL SERVER ============================= //

        private static string MSSQL_Server => @"(localdb)\mssqllocaldb";
        private static string MSSQL_DataBase => @"SomeProject";
        public static string SqlServerConnectionString => $"Server={MSSQL_Server};Database={MSSQL_DataBase};Trusted_Connection=True;";
    }
}
