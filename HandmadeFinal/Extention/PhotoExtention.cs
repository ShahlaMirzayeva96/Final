using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Extention
{
    public static  class PhotoExtention
    {
        public static bool IsPhoto(this IFormFile formFile)
        {
            return (formFile.ContentType.Contains(@"image/"));
        }

        public static bool PhotoSize(this IFormFile form,int maxSize)
        {
            return (form.Length / 1024 / 1024 < maxSize);
        }

        public async static Task<string> CopyPhoto(this IFormFile formFile,string root,string folder)
        {
            string path = Path.Combine(root,"img");
            string filename = Path.Combine(folder, Guid.NewGuid().ToString() + formFile.FileName);
            string resultPath = Path.Combine(path, filename);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
}
            string replaced_filename = filename.Replace(@"\", "/");
            return replaced_filename;
        }
      
    }
}
