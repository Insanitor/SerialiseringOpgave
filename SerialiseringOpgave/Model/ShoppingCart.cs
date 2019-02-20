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

        public ShoppingItem[] CartItem
        {
            get { return cartItems; }
            private set { cartItems = value; }
        }

        [NonSerialized] private double totalPrice;

        private double TotalPrice
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
        /// Saves a Shopping Cart
        /// </summary>
        /// <param name="cart">The Shopping Cart to save</param>
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
                var deserializedData = bf.Deserialize(fs);
                fs.Close();
                return deserializedData as ShoppingCart;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                fs.Close();
                return null;
            }
        }

        /// <summary>
        /// Attempts to adds an item to the Shopping Cart,
        /// </summary>
        /// <param name="item">The item to add to the cart</param>
        public void AddItem(ShoppingItem item)
        {
            for (int i = 0; i <= cartItems.Length; i++)
                if (i != cartItems.Length)
                {
                    if (cartItems[i] == null && i < cartItems.Length)
                    {
                        cartItems[i] = item;
                        Console.WriteLine("Added " + item.Name + " to the Shopping Cart");
                        break;
                    }
                }
                else
                    Console.WriteLine("Cart is full!");
        }

        /// <summary>
        /// Clears the Shopping Cart of Items
        /// </summary>
        public void RemoveItems()
        {
            cartItems = new ShoppingItem[cartItems.Length];
        }

        /// <summary>
        /// Prints all the items in the Shopping Cart
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
                        "Price: " + item.Price + "$\n" +
                        "VAT: " + ((item.Price * ((item.VAT / 100) + 1)) - item.Price) + "$" + "\n");
                }
            }
            Console.WriteLine("Number of Items in Cart: " + counter + "\n");
        }

        /// <summary>
        /// Run when a Shopping Cart is Deserialised
        /// </summary>
        /// <param name="sender">404 Explaination not found</param>
        public void OnDeserialization(object sender)
        {
            foreach (ShoppingItem item in cartItems)
            {
                if (item != null)
                    TotalPrice += (item.Price * ((item.VAT / 100) + 1));
            }
            Console.WriteLine("Total Price of the Loaded Cart: " + TotalPrice + "$\n");
        }
    }
}
