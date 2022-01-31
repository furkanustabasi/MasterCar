using Business.Abstract;
using Business.BusinessAspects.Autofac;
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

        [SecuredOperation("admin, car.ops")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [SecuredOperation("admin, car.ops")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.ListedSuccess);
        }

        public IDataResult<Car> GetById(int id)
        {

            var result = _carDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Car>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<Car>(Messages.NotFound);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int id)
        {

            var result = _carDal.GetCarsDetails(x => x.BrandId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarDetailDto>>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<List<CarDetailDto>>(Messages.NotFound);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int id)
        {

            var result = _carDal.GetCarsDetails(x => x.ColorId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarDetailDto>>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<List<CarDetailDto>>(Messages.NotFound);
        }

        public IDataResult<CarDetailDto> GetCarDetailById(int id)
        {
            var result = _carDal.GetCarsDetails(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                return new SuccessDataResult<CarDetailDto>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<CarDetailDto>(Messages.NotFound);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            var result = _carDal.GetAll(x => x.BrandId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Car>>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<List<Car>>(Messages.NotFound);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {

            var result = _carDal.GetAll(x => x.ColorId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Car>>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<List<Car>>(Messages.NotFound);
        }

        [SecuredOperation("admin, car.ops")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.UpdateSuccess);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails(), Messages.ListedSuccess);
        }
    }
}
