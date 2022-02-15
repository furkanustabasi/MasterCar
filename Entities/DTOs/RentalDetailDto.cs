using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public short ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string CompanyName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? CertainReturnDate { get; set; }
    }
}
