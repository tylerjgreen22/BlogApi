using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    // Api contoller layer which interacts with the service layer to provide external interaction
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        // readonly field for the post service instance
        private readonly PostsService _postsService;

        // Posts service retrieved through dependency injection (constructor injection) and controller field initialized to posts service
        public PostsController(PostsService postsService) =>
            _postsService = postsService;

        // Due to asynchronous nature of service methods, methods marked async and return non blocking Tasks

        // HttpGet to return all posts
        [HttpGet]
        public async Task<List<PostDTO>> Get() =>
            await _postsService.GetAsync();


        // HttpGet to return a single post
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PostDTO>> Get(string id)
        {
            var post = await _postsService.GetAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            return post;
        }

        // HttpPost to create a post, returns a 201 with a location header that specified the URI of the new post
        [HttpPost]
        public async Task<IActionResult> Post(PostDTO newPost)
        {
            await _postsService.CreateAsync(newPost);

            return CreatedAtAction(nameof(Get), new { id = newPost.Id }, newPost);
        }

        // HttpPut to update a post
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, PostDTO updatedPost)
        {
            var post = await _postsService.GetAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            updatedPost.Id = post.Id;

            await _postsService.UpdateAsync(id, updatedPost);

            return NoContent();
        }

        // HttpDelete to delete a post
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var post = await _postsService.GetAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            await _postsService.RemoveAsync(id);

            return NoContent();
        }
    }
}