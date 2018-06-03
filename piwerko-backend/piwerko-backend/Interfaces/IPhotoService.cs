using Microsoft.AspNetCore.Http;
using Piwerko.Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IPhotoService
    {
        Task<ResultDto<StringDto>> SavePhotoToDB(string path, IFormFile file);
    }
}
