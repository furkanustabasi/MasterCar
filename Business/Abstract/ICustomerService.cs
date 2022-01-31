using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<Customer> GetById(int id);
        IDataResult<List<Customer>> GetAll();
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
        IDataResult<List<CustomerDetailDto>> GetCustomersDetail();
        IDataResult<CustomerDetailDto> GetCustomerDetail(int id);
        IDataResult<CustomerDetailDto> GetCustomerDetailByUserId(int id);


    }
}
