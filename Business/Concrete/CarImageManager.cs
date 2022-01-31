using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        private ICarService _carService;

        List<string> totalImagePathes = new List<string>();

        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }


        public IDataResult<CarImage> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.ListedSuccess);
        }

        public IDataResult<List<CarImage>> GetCarImages(int carId)
        {

            if (!CheckIfCarExist(carId).Success)
            {
                return new ErrorDataResult<List<CarImage>>(Messages.FaultSearchNotFound);
            }

            if (CheckIfCarImageExist(carId).Success)
            {
                return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(x => x.CarId == carId), Messages.ListedSuccess);
            }

            return ReturnDefaultImage(carId);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            BusinessRules.Run(CheckIfCarExist(carImage.CarId), CarImageOutOfLimit(carImage.CarId));

            var uploadedImage = FileHelper.Add(file);

            if (!uploadedImage.Success)
            {
                return uploadedImage;
            }

            CarImage myCarImage = new CarImage
            {
                CarId = carImage.CarId,
                Date = DateTime.Now,
                ImagePath = uploadedImage.Data
            };

            _carImageDal.Add(myCarImage);

            return new SuccessResult(Messages.AddedSuccess);

        }

        public IResult Add(List<IFormFile> files, CarImage carImage)
        {

            BusinessRules.Run(FilesOutOfLimit(files.Count), CheckIfCarExist(carImage.CarId));


            foreach (var file in files)
            {
                if (CarImageOutOfLimit(carImage.CarId).Success)
                {
                    FileHelper.Add(file);
                }

            }

            return new SuccessResult();

        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            var isDeleted = Delete(carImage);

            if (isDeleted.Success)
            {
                if (Add(file, carImage).Success)
                {
                    return new SuccessResult(Messages.UpdateSuccess);
                }
            }

            return new ErrorResult(Messages.UpdateError);
        }

        public IResult Update(List<IFormFile> files, CarImage carImage)
        {
            throw new NotImplementedException();
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage carImage)
        {
            var source = CheckIfCarImageExist(carImage.Id);

            if (!source.Success)
            {
                return new ErrorResult(Messages.NotFound);
            }

            _carImageDal.Delete(carImage);
            var result = FileHelper.Delete(source.Data.ImagePath);

            if (result.Success)
            {
                return new SuccessResult(Messages.DeleteSuccess);
            }

            return new ErrorResult(Messages.DeleteError);
        }

        private IDataResult<Car> CheckIfCarExist(int carId)
        {
            return _carService.GetById(carId);
        }

        private IDataResult<CarImage> CheckIfCarImageExist(int id)
        {
            var data = _carImageDal.Get(x => x.Id == id);

            if (data != null)
            {
                return new SuccessDataResult<CarImage>(data);
            }

            return new ErrorDataResult<CarImage>(Messages.NotFound);
        }

        private IResult CarImageOutOfLimit(int carId)
        {
            var imageCount = _carImageDal.GetAll(x => x.CarId == carId).Count;

            if (imageCount == 5)
            {
                return new ErrorResult("En fazla 5 resim ekleyebilirsiniz.");
            }

            return new SuccessResult();
        }

        private static IResult FilesOutOfLimit(int count)
        {
            if (count > 5)
            {
                return new ErrorResult("5 resimden fazla yükleme yapamazsınız.");
            }

            return new SuccessResult();
        }

        private static IDataResult<List<CarImage>> ReturnDefaultImage(int carId)
        {
            List<CarImage> carImages = new List<CarImage>
            {
                new CarImage
                {
                   ImagePath = "/images/default.jpg",
                   CarId = carId,
                   Date = DateTime.Now
                }
            };

            return new SuccessDataResult<List<CarImage>>(carImages, "Default resim gösteriliyor.");
        }


    }
}
