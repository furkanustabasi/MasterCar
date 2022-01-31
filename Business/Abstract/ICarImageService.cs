using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<CarImage> GetById(int id);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetCarImages(int carId);
        IResult Add(IFormFile file, CarImage carImage);
        IResult Add(List<IFormFile> files, CarImage carImage);
        IResult Update(IFormFile file, CarImage carImage);
        IResult Update(List<IFormFile> files, CarImage carImage);
        IResult Delete(CarImage carImage);
    }
}
