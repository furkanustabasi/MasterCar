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
    public interface IRentalService
    {
        IDataResult<Rental> GetById(int id);
        IDataResult<RentalDetailDto> GetRentalDetailById(int id);
        IDataResult<List<RentalDetailDto>> GetRentalsDetail();
        IDataResult<List<RentalDetailDto>> GetRentalsDetailByCarId(int id);
        IDataResult<List<RentalDetailDto>> GetRentalsDetailByCustomerId(int id);
        IDataResult<List<Rental>> GetAll();
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
    }
}
