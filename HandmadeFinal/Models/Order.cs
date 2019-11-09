using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime  OrderTime { get; set; }

        public double Total { get; set; }

        public int UserRegisterId { get; set; }

        public virtual UserRegister UserRegister { get; set; }

        public  virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
