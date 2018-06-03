using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Piwerko.Api.Dto;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly PhotoSettings _photoSettings;
        private readonly IHostingEnvironment _host;
        public PhotoService(IHostingEnvironment host, IOptions<PhotoSettings> options)
        {
            _photoSettings = options.Value;
            _host = host;
        }
        public async Task<ResultDto<StringDto>> SavePhotoToDB(string path, IFormFile file)
        {
            var result = new ResultDto<StringDto>
            {
                Errors = new List<string>()
            };


            if (file == null) result.Errors.Add("Brak pliku");
            if (file.Length == 0) result.Errors.Add("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) result.Errors.Add("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) result.Errors.Add("Nieprawidłowy typ pliku");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, path);
            var nazwa = await UploadPhoto(file, uploadsFolderPath);
            result.SuccessResult.value = nazwa;
            return result;
        }

        private async Task<string> UploadPhoto(IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"{fileName}";

        }


    }
}
