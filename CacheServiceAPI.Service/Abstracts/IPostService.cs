using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheServiceAPI.Service.DTOs;

namespace CacheServiceAPI.Service.Abstracts
{
    public interface IPostService
    {
        IList<PostDTO> Get();
        void Add(PostDTO dto, bool saveInstant = true);
        void SaveChanges();
    }
}
