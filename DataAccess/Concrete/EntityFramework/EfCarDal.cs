using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityDal<Car, MasterCarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarsDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using MasterCarContext context = new();

            var result = from car in context.Cars
                         join brand in context.Brands
                         on car.BrandId equals brand.Id
                         join color in context.Colors
                         on car.ColorId equals color.Id
                         select new CarDetailDto
                         {
                             Id = car.Id,
                             BrandId = brand.Id,
                             BrandName = brand.Name,
                             ColorId = color.Id,
                             ColorName = color.Name,
                             DailyPrice = car.DailyPrice,
                             ModelName = car.ModelName,
                             ModelYear = car.ModelYear,
                             Description = car.Description
                         };

            return filter == null
                ? result.ToList()
                : result.Where(filter).ToList();

        }
    }
}
