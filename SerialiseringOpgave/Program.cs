using SerialiseringOpgave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SerialiseringOpgave
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart(6);
            while (true)
            {
                Console.WriteLine(
                    "##################################\n" +
                    "###  Fill Cart with Items      ###\n" +
                    "##################################\n");
                cart.AddItem(new ShoppingItem("Soap", 29.95d));
                cart.AddItem(new ShoppingItem("Bread", 9.95d));
                cart.AddItem(new ShoppingItem("Canned Tuna", 19.95d));
                cart.AddItem(new ShoppingItem("6\'3\" Golden Trump Statue", 2e10d));
                cart.AddItem(new ShoppingItem("Pepsi Max", 15.75d));
                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
                Console.Clear();

                Console.WriteLine(
                    "##################################\n" +
                    "###  Print Items & Save Cart   ###\n" +
                    "##################################\n");
                cart.PrintItems();
                ShoppingCart.SaveToFile(cart);
                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
                Console.Clear();

                Console.WriteLine(
                    "##################################\n" +
                    "###  Empty Cart & Print Again  ###\n" +
                    "##################################\n");
                cart.RemoveItems();
                cart.PrintItems();
                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
                Console.Clear();

                Console.WriteLine(
                    "##################################\n" +
                    "###  Load Cart & Print again   ###\n" +
                    "##################################\n");
                cart = ShoppingCart.LoadFromFile();
                cart.PrintItems();
                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();

                cart.GetItems();
                Console.Clear();

            }
        }
    }
}
