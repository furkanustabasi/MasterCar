using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityDal<Car>
    {
        List<CarDetailDto> GetCarsDetails(Expression<Func<CarDetailDto, bool>> filter = null);
    }
}
