using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<Car> GetById(int id);
        IDataResult<List<Car>> GetAll();
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IDataResult<List<Car>> GetCarsByColorId(int id);
        IDataResult<CarDetailDto> GetCarDetailById(int id);
        IDataResult<List<CarDetailDto>> GetCarsDetail();
        IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int id);
    }
}
