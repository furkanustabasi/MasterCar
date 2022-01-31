using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityDal<Rental, MasterCarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalsDetail(Expression<Func<RentalDetailDto, bool>> filter = null)
        {
            using MasterCarContext context = new();

            var result = from rental in context.Rentals
                         join car in context.Cars
                             on rental.CarId equals car.Id
                         join customer in context.Customers
                             on rental.CustomerId equals customer.Id
                         join user in context.Users
                             on customer.UserId equals user.Id
                         join brand in context.Brands
                             on car.BrandId equals brand.Id
                         select new RentalDetailDto
                         {
                             Id = rental.Id,
                             BrandName = brand.Name,
                             CarId = car.Id,
                             CompanyName = customer.CompanyName,
                             CustomerFullName = user.FirstName + " " + user.LastName,
                             CustomerId = customer.Id,
                             DailyPrice = car.DailyPrice,
                             ModelName = car.ModelName,
                             ModelYear = car.ModelYear,
                             RentDate = rental.RentDate,
                             ReturnDate = rental.ReturnDate

                         };
            return filter == null
                ? result.ToList()
                : result.Where(filter).ToList();

        }
    }
}
