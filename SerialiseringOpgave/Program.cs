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
            ShoppingCart cart = new ShoppingCart(10);

            cart.AddItem(new ShoppingItem("Sæbe", 29.95d));
            cart.AddItem(new ShoppingItem("Rugbrød", 9.95d));
            cart.AddItem(new ShoppingItem("Skumbananer", 19.95d));
            cart.AddItem(new ShoppingItem("Interkontinentalt Ballistisk Atommissil", 2e10d));
            cart.AddItem(new ShoppingItem("Pepsi Max", 15.75d));

            Console.WriteLine(
                "##################################\n" +
                "###  Print Items & Save Cart   ###\n" +
                "##################################\n");
            cart.PrintItems();
            ShoppingCart.SaveToFile(cart);
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine(
                "##################################\n" +
                "###  Empty Cart & Print Again  ###\n" +
                "##################################\n");
            cart.RemoveItems();
            cart.PrintItems();
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine(
                "##################################\n" +
                "###  Load Cart & Print again   ###\n" +
                "##################################\n");
            cart = ShoppingCart.LoadFromFile();
            cart.PrintItems();
            Console.ReadLine();

            cart.GetItems();
        }
    }
}
