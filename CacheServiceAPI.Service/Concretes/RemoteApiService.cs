using CacheServiceAPI.Service.Abstracts;
using CacheServiceAPI.Service.DTOs;
using Newtonsoft.Json;

namespace CacheServiceAPI.Service.Concretes
{
    public class RemoteApiService
    {
        private readonly IPostService _postService;
        private readonly ICacheService _cacheService;

        public RemoteApiService(IPostService postService, ICacheService cacheService)
        {
            _postService = postService;
            _cacheService = cacheService;
        }
        public async Task<IList<PostDTO>> GetDataFromApi()
        {
            using var client = new HttpClient();
            var responseTask = await client.GetAsync(new Uri("https://jsonplaceholder.typicode.com/posts"));
            if (responseTask.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await responseTask.Content.ReadAsStringAsync();
                List<PostDTO> postDtoList = JsonConvert.DeserializeObject<List<PostDTO>>(responseString);

                return postDtoList;
            }

            throw new InvalidOperationException("Api isteğinin status kodu 200 değil");

        }

        public async Task SaveApiDataToDatabase()
        {
            var list = await GetDataFromApi();
            foreach (var item in list)
            {
                _postService.Add(item,false);
            }
            _postService.SaveChanges();
            _cacheService.Remove("allPostDto");
        }
    }
}
