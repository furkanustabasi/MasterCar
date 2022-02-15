using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;
        private IUserService _userService;

        public CustomerManager(ICustomerDal customerDal, IUserService userService)
        {
            _customerDal = customerDal;
            _userService = userService;
        }

        [SecuredOperation("admin, customer.ops")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {

            var resultRules = BusinessRules.Run(GetById(customer.Id), _userService.GetById(customer.UserId));

            if (!resultRules.Success)
            {
                return new ErrorResult();
            }

            _customerDal.Add(customer);
            return new SuccessResult(Messages.AddedSuccess);
        }


        [SecuredOperation("admin, customer.ops")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Delete(Customer customer)
        {
            if (!GetById(customer.Id).Success)
            {
                return new ErrorResult(Messages.NotFound);
            }

            _customerDal.Delete(customer);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.ListedSuccess);

        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Customer>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<Customer>(Messages.NotFound);
        }

        public IDataResult<CustomerDetailDto> GetCustomerDetail(int id)
        {
            var result = _customerDal.GetCustomersDetail(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                return new SuccessDataResult<CustomerDetailDto>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<CustomerDetailDto>(Messages.NotFound);
        }

        public IDataResult<CustomerDetailDto> GetCustomerDetailByUserId(int id)
        {
            var result = _customerDal.GetCustomersDetail(x => x.UserId == id).FirstOrDefault();
            if (result != null)
            {
                return new SuccessDataResult<CustomerDetailDto>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<CustomerDetailDto>(Messages.NotFound);
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomersDetail()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomersDetail(), Messages.ListedSuccess);
        }

        [SecuredOperation("admin, customer.ops")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            var resultRules = BusinessRules.Run(GetById(customer.Id),_userService.GetById(customer.UserId));

            if (!resultRules.Success)
            {
                return new ErrorResult();
            }

            _customerDal.Update(customer);
            return new SuccessResult(Messages.UpdateSuccess);
        }


        
    }
}
