using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SerialiseringOpgave.Model
{
    [Serializable]
    class ShoppingItem
    {
        static int IdCount = 0;

        public string Name { get; private set; }
        public int ItemId { get; private set; }
        public double Price { get; private set; }
        public double VAT { get; private set; }

        /// <summary>
        /// Constructor for a Shopping Item
        /// </summary>
        /// <param name="name">Name of the Item</param>
        /// <param name="price">Price of the Item</param>
        public ShoppingItem(string name, double price)
        {
            Name = name;
            Price = price;
            ItemId = IdCount;
            VAT = 25;
            IdCount++;
        }
    }
}
