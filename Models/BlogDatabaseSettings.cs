namespace BlogApi.Models
{
    // Used to store the values from the BlogDatabase property values from the appsetting.json, which is used to connect to the mongodb database
    public class BlogDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PostsCollectionName { get; set; } = null!;
    }
}