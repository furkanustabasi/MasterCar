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

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.ListedSuccess);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.Id == id), Messages.ListedSuccess);
        }

        public IDataResult<RentalDetailDto> GetRentalDetailById(int id)
        {
            return new SuccessDataResult<RentalDetailDto>(_rentalDal.GetRentalsDetail(x => x.Id == id).FirstOrDefault(), Messages.ListedSuccess);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalsDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalsDetail(), Messages.ListedSuccess);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalsDetailByCarId(int id)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalsDetail(x=>x.CarId == id), Messages.ListedSuccess);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalsDetailByCustomerId(int id)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalsDetail(x => x.CustomerId == id), Messages.ListedSuccess);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.UpdateSuccess);
        }


        private bool CheckIfRentalIsAvaible(Rental rental)
        {
            var check1 = _rentalDal.Get(x => x.Id == rental.Id);
            var check2 = _carService.GetById(check1.CarId);

            if (check1.ReturnDate != null && check2.Data.IsAvaible)
            {
                return true;
            }

            return false;
        }
    }
}
