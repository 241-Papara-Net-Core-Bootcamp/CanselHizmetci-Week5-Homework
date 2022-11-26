using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CacheServiceAPI.Data.Abstracts;
using CacheServiceAPI.Domain.Entities;
using CacheServiceAPI.Service.Abstracts;
using CacheServiceAPI.Service.DTOs;

namespace CacheServiceAPI.Service.Concretes
{
    public class PostService:IPostService
    {
        private readonly ICacheService _cacheService;
        private readonly IRepository<Post> _repository;
        private readonly IMapper _mapper;
        public PostService(IRepository<Post> repository, IMapper mapper, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public IList<PostDTO> Get()
        {
            if (_cacheService.TryGet("allPostDto", out IList<PostDTO> cachedList))
                return cachedList;
            var postList = _mapper.Map<List<PostDTO>>(_repository.Get().ToList());
            _cacheService.Set<IList<PostDTO>>("allPostDto", postList);
            return postList;
        }
        //Api den 5 saniyede bir veri çwkiyoruz ve 100 satır var. Her satır sonunda Savechanges yapmasın bütün satırları çektikten sonra SaveChanges yapsın diye saveInstant ekledim.
        public void Add(PostDTO dto, bool saveInstant = true)
        {
            _repository.Add(_mapper.Map<Post>(dto), saveInstant);
        }
        
        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
