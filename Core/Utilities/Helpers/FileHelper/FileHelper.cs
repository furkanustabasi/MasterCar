using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public static class FileHelper
    {
        public static readonly string _rootDirectory = Environment.CurrentDirectory + "wwwroot\\Images\\";
        public static readonly string[] _acceptedDataTypes = new string[] { ".jpg", ".jpeg", ".png" };
        public static readonly double _maxSize = 5242880;

        public static IDataResult<string> Add(IFormFile file)
        {

            if (!CheckFileSize(file.Length))
            {
                return new ErrorDataResult<string>("Yüklemek istediğiniz dosya 5 mb limitini aşmaktadır.");
            }

            if (!IsCorrectType(file.FileName))
            {
                return new ErrorDataResult<string>("Dosya formatı hatalı.");
            }

            CheckAndCreateDirectory(_rootDirectory);

            return new SuccessDataResult<string>(CreateImage(file),"Yükleme başarılı oldu.");
        }

        public static IResult Add(List<IFormFile> files)
        {
            List<string> imagesPathes = new List<string>();


            foreach (var file in files)
            {
                if (!CheckFileSize(file.Length))
                {
                    continue;
                }

                if (!IsCorrectType(file.FileName))
                {
                    continue;
                }

                CheckAndCreateDirectory(_rootDirectory);


                var imagePath = CreateImage(file);

                imagesPathes.Add(imagePath);

            }

            return new SuccessDataResult<List<string>>(imagesPathes);
        }


        public static IResult Update(IFormFile file, string filePath)
        {

            Delete(filePath);

            return Add(file);
        }

        public static IResult Delete(string filePath)
        {
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath);

                return new SuccessResult();
            }

            return new ErrorResult();
        }

        private static bool IsCorrectType(string fileName)
        {
            var extention = Path.GetExtension(fileName);

            if (_acceptedDataTypes.Contains(extention))
            {
                return true;
            }

            return false;
        }

        private static void CheckAndCreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static bool CheckFileSize(double fileSize)
        {

            if (fileSize > 0 && fileSize < _maxSize)
            {
                return true;
            }

            return false;
        }

        private static string CreateImage(IFormFile file)
        {
            var guidName = Guid.NewGuid().ToString();
            var extention = Path.GetExtension(file.FileName);

            var folderName = guidName + extention;
            var imagePath = _rootDirectory + folderName;

            using (FileStream fileStream = File.Create(imagePath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            return imagePath;
        }

    }
}
