using BlogApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BlogApi.Services
{
    // A service layer containing methods for interacting with the model layer 
    public class PostsService
    {
        // readonly field representing the posts collection from mongodb
        private readonly IMongoCollection<PostDTO> _postsCollection;

        /* BlogDB settings retrieved via dependency injection(constructor injection) 
           mongo client instantiated using blog settings connection string, 
           mongoDatabase retrived using mongo client and blog settings db name, 
           _postscollections initialized using collection name retrieved from mongoDatabase
        */
        public PostsService(
            IOptions<BlogDatabaseSettings> blogDatabaseSettings
        )
        {
            var mongoClient = new MongoClient(blogDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(blogDatabaseSettings.Value.DatabaseName);

            _postsCollection = mongoDatabase.GetCollection<PostDTO>(blogDatabaseSettings.Value.PostsCollectionName);
        }

        // Due to asynchonous nature of working with external db, all methods are async and return non blocking Tasks

        // Getting all posts
        public async Task<List<PostDTO>> GetAsync() =>
            await _postsCollection.Find(post => true).ToListAsync();

        // Getting one post by id
        public async Task<PostDTO?> GetAsync(string id) =>
            await _postsCollection.Find(post => post.Id == id).FirstOrDefaultAsync();

        // Creating a post
        public async Task CreateAsync(PostDTO newPost) =>
            await _postsCollection.InsertOneAsync(newPost);

        // Updating a post by id
        public async Task UpdateAsync(string id, PostDTO updatedPost) =>
            await _postsCollection.ReplaceOneAsync(post => post.Id == id, updatedPost);

        // Deleting a post by id
        public async Task RemoveAsync(string id) =>
            await _postsCollection.DeleteOneAsync(post => post.Id == id);
    }
}