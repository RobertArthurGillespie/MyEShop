using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEShop.Core.Models
{
    public class Basket:BaseEntity
    {
        //important to set it to virtual and ICollection so when entity framework loads it from database
        //it will automatically load the basket items as well
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}
