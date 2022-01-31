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
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [SecuredOperation("admin, customer.ops")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.AddedSuccess);
        }


        [SecuredOperation("admin, customer.ops")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.ListedSuccess);

        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(x => x.Id == id), Messages.ListedSuccess);
        }

        public IDataResult<CustomerDetailDto> GetCustomerDetail(int id)
        {
            return new SuccessDataResult<CustomerDetailDto>(_customerDal.GetCustomersDetail(x => x.Id == id).FirstOrDefault(), Messages.ListedSuccess);
        }

        public IDataResult<CustomerDetailDto> GetCustomerDetailByUserId(int id)
        {
            return new SuccessDataResult<CustomerDetailDto>(_customerDal.GetCustomersDetail(x => x.UserId == id).FirstOrDefault(), Messages.ListedSuccess);
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomersDetail()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomersDetail(), Messages.ListedSuccess);
        }

        [SecuredOperation("admin, customer.ops")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.UpdateSuccess);
        }
    }
}
