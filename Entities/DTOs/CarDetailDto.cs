using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CarDetailDto : IDTO
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public string ModelName { get; set; }
        public short ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
    }
}
