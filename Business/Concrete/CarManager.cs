using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int id)
        {

            var result = _carDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Car>(result, Messages.CarListed);
            }

            return new ErrorDataResult<Car>(Messages.NotFound);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int id)
        {

            var result = _carDal.GetCarsDetails(x => x.BrandId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarDetailDto>>(result, Messages.CarsListed);
            }

            return new ErrorDataResult<List<CarDetailDto>>(Messages.NotFound);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int id)
        {

            var result = _carDal.GetCarsDetails(x => x.ColorId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarDetailDto>>(result, Messages.CarsListed);
            }

            return new ErrorDataResult<List<CarDetailDto>>(Messages.NotFound);
        }

        public IDataResult<CarDetailDto> GetCarDetailById(int id)
        {
            var result = _carDal.GetCarsDetails(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                return new SuccessDataResult<CarDetailDto>(result, Messages.CarListed);
            }

            return new ErrorDataResult<CarDetailDto>(Messages.NotFound);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            var result = _carDal.GetAll(x => x.BrandId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Car>>(result, Messages.CarsListed);
            }

            return new ErrorDataResult<List<Car>>(Messages.NotFound);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {

            var result = _carDal.GetAll(x => x.ColorId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Car>>(result, Messages.CarsListed);
            }

            return new ErrorDataResult<List<Car>>(Messages.NotFound);
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails(), Messages.CarsListed);
        }
    }
}
