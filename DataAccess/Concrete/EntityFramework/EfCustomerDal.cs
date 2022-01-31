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
    public class EfCustomerDal : EfEntityDal<Customer, MasterCarContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomersDetail(Expression<Func<CustomerDetailDto, bool>> filter = null)
        {
            using MasterCarContext context = new();

            var result = from customer in context.Customers
                         join user in context.Users
                         on customer.UserId equals user.Id
                         select new CustomerDetailDto
                         {
                             Id = customer.Id,
                             UserId = user.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             CompanyName = customer.CompanyName
                         };

            return filter == null
                ? result.ToList()
                : result.Where(filter).ToList();
        }
    }
}
