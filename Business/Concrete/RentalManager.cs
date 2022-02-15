using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
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
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            if (!CheckIfRentalIsAvaible(rental))
            {
                return new ErrorResult(Messages.RentalCarNotAvailable);
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [SecuredOperation("admin, rental.ops, rental.delete")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Delete(Rental rental)
        {
            if (!GetById(rental.Id).Success)
            {
                return new ErrorResult(Messages.NotFound);
            }

            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        [PerformanceAspect(15)]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.ListedSuccess);
        }

        public IDataResult<Rental> GetById(int id)
        {
            var result = _rentalDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Rental>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<Rental>(Messages.NotFound);
        }

        public IDataResult<RentalDetailDto> GetRentalDetailById(int id)
        {
            var result = _rentalDal.GetRentalsDetail(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                return new SuccessDataResult<RentalDetailDto>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<RentalDetailDto>(Messages.NotFound);
        }

        [PerformanceAspect(15)]
        public IDataResult<List<RentalDetailDto>> GetRentalsDetail()
        {
            var result = _rentalDal.GetRentalsDetail();
            if (result != null)
            {
                return new SuccessDataResult<List<RentalDetailDto>>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<List<RentalDetailDto>>(Messages.NotFound);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalsDetailByCarId(int id)
        {
            var result = _rentalDal.GetRentalsDetail(x => x.CarId == id);
            if (result != null)
            {
                return new SuccessDataResult<List<RentalDetailDto>>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<List<RentalDetailDto>>(Messages.NotFound);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalsDetailByCustomerId(int id)
        {
            var result = _rentalDal.GetRentalsDetail(x => x.CustomerId == id);
            if (result != null)
            {
                return new SuccessDataResult<List<RentalDetailDto>>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<List<RentalDetailDto>>(Messages.NotFound);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            if (!GetById(rental.Id).Success)
            {
                return new ErrorResult(Messages.NotFound);
            }

            _rentalDal.Update(rental);
            return new SuccessResult(Messages.UpdateSuccess);
        }

        private bool CheckIfRentalIsAvaible(Rental rental)
        {
            var check1 = _rentalDal.Get(x => x.Id == rental.Id);
            var check2 = _carService.GetById(check1.CarId);

            if (check1.CertainReturnDate != null && check2.Data.IsAvaible)
            {
                return true;
            }

            return false;
        }
    }
}
