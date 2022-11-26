using CacheServiceAPI.Service.Abstracts;
using CacheServiceAPI.Service.Concretes;
using CacheServiceAPI.Service.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CacheServiceAPI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        private readonly RemoteApiService _remoteApiService;
        // GET: api/<PostController>
        public PostController(IPostService postService, RemoteApiService remoteApiService)
        {
            _postService = postService;
            _remoteApiService = remoteApiService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postService.Get());
        }
        [HttpGet,Route("ApiTetikle")]
        public async Task<IActionResult> GetApi()
        {
            await _remoteApiService.SaveApiDataToDatabase();
            return Ok();
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostController>
        [HttpPost]
        public IActionResult Post([FromBody] PostDTO newPost)
        {
            _postService.Add(newPost);
            return Ok();
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
