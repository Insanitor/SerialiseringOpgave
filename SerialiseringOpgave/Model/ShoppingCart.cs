using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerialiseringOpgave.Model
{
    [Serializable]
    class ShoppingCart : IDeserializationCallback
    {
        ShoppingItem[] cartItems;

        [NonSerialized] private double totalPrice;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        /// <summary>
        /// Constructor for a Shopping Cart
        /// </summary>
        /// <param name="cartSize">Size of the Shopping Cart</param>
        public ShoppingCart(byte cartSize)
        {
            cartItems = new ShoppingItem[cartSize];
        }


        /// <summary>
        /// A very lonely Method with no purpose
        /// </summary>
        public void GetItems() { }


        /// <summary>
        /// Saves a cart
        /// </summary>
        /// <param name="cart"></param>
        public static void SaveToFile(ShoppingCart cart)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream($"ShoppingCart.dat", FileMode.Create);
            try
            {
                bf.Serialize(fs, cart);
                fs.Close();
                Console.WriteLine("Cart Saved Succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                fs.Close();
            }

        }

        /// <summary>
        /// Loads a Cart
        /// </summary>
        /// <returns>Returns the Shopping Cart from ShoppingCart.dat</returns>
        public static ShoppingCart LoadFromFile()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream($"ShoppingCart.dat", FileMode.Open);
            try
            {
                ShoppingCart sc = (ShoppingCart)bf.Deserialize(fs);
                fs.Close();
                return sc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                fs.Close();
                return null;
            }
        }

        /// <summary>
        /// Adds an item to the Shopping Cart
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ShoppingItem item)
        {
            for (int i = 0; i < cartItems.Length - 1; i++)
            {
                if (cartItems[i] == null)
                {
                    cartItems[i] = item;
                    break;
                }
            }
        }

        /// <summary>
        /// Clears the Cart of Items
        /// </summary>
        public void RemoveItems()
        {
            for (int i = 0; i < cartItems.Length - 1; i++)
            {
                cartItems[i] = null;
            }
        }

        /// <summary>
        /// Prints all the items in the Cart
        /// </summary>
        public void PrintItems()
        {
            int counter = 0;
            foreach (ShoppingItem item in cartItems)
            {
                if (item != null)
                {
                    counter++;
                    Console.WriteLine("Product ID: " + item.ItemId + "\n" +
                        "Product: " + item.Name + "\n" +
                        "Price ex. VAT: " + (item.Price * ((item.VAT / 100) + 1)) + "$\n" +
                        "VAT: " + ((item.Price * ((item.VAT / 100) + 1)) - item.Price) + "$" + "\n");
                }
            }
            Console.WriteLine("Number of Items in Cart: " + counter + "\n");
        }

        public void OnDeserialization(object sender)
        {
            foreach (ShoppingItem item in cartItems)
            {
                if (item != null)
                    TotalPrice += item.Price;
            }
            Console.WriteLine("Total Price of the Loaded Cart: " + TotalPrice + "$\n");
        }
    }
}
