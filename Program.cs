using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
namespace VendingMachine
{
    public class Program
    {
        static void Main(string[] args)
        {// Main provides the user interface for the VendingMachine objects.
            VendingMachine vendingMachine = new VendingMachine();
            bool is_on = true;
            while (is_on)
            {
                Console.WriteLine("Welcome to the vending machine.\r\n To initiate a purchase press 1, to quit press 0");
                bool control = double.TryParse(Console.ReadLine(), out double ansver);
                if (control)
                {
                    switch (ansver)
                    {
                        case 0:
                            is_on = false;
                            Console.WriteLine("Closes the program");// The program shuts down by user choice.
                            break;
                        case 1:
                            bool session = true;
                            //Initialises purchase session.
                            while (session)
                            {
                              
                                Console.WriteLine("The following items are available");
                                VendingMachine.Display();
                                Console.WriteLine("Press 1 to insert money, press 0 to leave");
                                bool control2 = double.TryParse(Console.ReadLine(), out double ansver2);
                                if (control2)
                                {
                                    switch (ansver2)
                                    {
                                        case 0:
                                            session = false;
                                            VendingMachine.Checkout();//Exits purchase session.
                                            break;
                                        case 1:

                                            VendingMachine.Balance = VendingMachine.Deposit(VendingMachine.Denominations, VendingMachine.Balance);
                                            bool session2 = true;
                                            while (session2)
                                            {//Next step of purchase session.
                                                Console.WriteLine("Enter product number to examine a product");
                                                Console.WriteLine("Enter 123 for purchase");
                                                Console.WriteLine("Enter 321 to remove a purchased product");
                                                Console.WriteLine("Enter 0 to get money returned and leave");
                                                bool control3 = int.TryParse(Console.ReadLine(), out int ansver3);
                                                if (control3)
                                                {
                                                    switch (ansver3)
                                                    {
                                                        case 0:
                                                            VendingMachine.Checkout();
                                                            //Exits purchase session.
                                                            session2 = false;

                                                            break;
                                                        case 123://Purchase is executed
                                                            Console.WriteLine("Give the product number of the product you vant to buy.");
                                                            bool control4 = int.TryParse(Console.ReadLine(), out int choice);
                                                            if (control4)
                                                            {
                                                                VendingMachine.Purchase(choice);
                                                                Console.WriteLine("Your shopping cart is now contains:");
                                                                VendingMachine.Displaypurchases();
                                                                Console.WriteLine($"Your have {VendingMachine.Balance} Kr left.");
                                                            }
                                                            else { Console.WriteLine("Could not understand input"); }
                                                            break;
                                                        case 321:// Puchased products are removed from the basket.
                                                            Console.WriteLine("Give the product number of the product you vant to remove");
                                                            bool control5 = int.TryParse(Console.ReadLine(), out int choice2);
                                                            if (control5)
                                                            {
                                                                VendingMachine.Unpurchase(choice2);
                                                            }
                                                            else { Console.WriteLine("Could not understand input"); }
                                                            break;
                                                        case 1:
                                                            VendingMachine.Display(ansver3);
                                                            break;
                                                        case 2:
                                                            VendingMachine.Display(ansver3);
                                                            break;
                                                        case 3:
                                                            VendingMachine.Display(ansver3);
                                                            break;
                                                        default:
                                                            Console.WriteLine("Could not understand input");
                                                            break;

                                                    }
                                                }
                                                else { Console.WriteLine("Could not understand input"); }
                                            }
                                            break;
                                        default:
                                            Console.WriteLine("Could not understand input");
                                            break;

                                    }
                                }
                                else { Console.WriteLine("Could not understand input"); }
                            }
                            break;
                        default:
                            Console.WriteLine("Could not understand input");
                            break;

                    }

                }
                else { Console.WriteLine("Could not understand input"); }
            }
        }
        public class VendingMachine
        {//Initialises the VendingMachine classes.
            public static List<Object> Purchases { get; set; }
            public static List<Object> Inventory { get; set; }
            public static int[] Denominations { get; set; }
            public static int Balance { get; set; }
            public VendingMachine()
            {
                int[] _denominations;//Money denominations are stored and set for the program.
                _denominations = new int[9] { 1, 5, 10, 20, 50, 100, 200, 500,1000 };
                Array.AsReadOnly<int>(_denominations);
                 Denominations = _denominations;
                Balance = 0;//Initial balance.
                Coke_Can a = new Coke_Can();//Products are set
                Snickers_bar b = new Snickers_bar();
                Teddy_bear c = new Teddy_bear();
                Purchases = new List<Object>();//Shopping basket is initialised.
                Inventory = new List<Object>();//Product inventory is built up.
                Inventory.Add(a);
                Inventory.Add(b);
                Inventory.Add(c);
            }
            public static int Deposit(int[] Denominations, int Balance)
            {//Accepts money from user and sets the balance accordingly.
                int[] money = Denominations;
                for (int i = 0; i < money.Length; i++)
                {   if (money[i] < 20)
                    { Console.WriteLine($"How many { money[i]} Kr coins do you want to insert?"); }
                else
                    { Console.WriteLine($"How many { money[i]} Kr bills do you want to insert?"); }
                    bool control = int.TryParse(Console.ReadLine(), out int number);
                    Balance = Balance + number * money[i];
                }
                return Balance;
            }
            public static int[] Cashreturn()
            {//A vector matching the VendingMachine.Denominations is created storing the number of bill
                //that must be returned to user.
                int stilleft = Balance;
                int[] money = Denominations;
                int length=money.Length;
                int[] return_money = new int[length];

                for (int i = length-1; i > -1; i--)
                {
                    if (money[i] <= stilleft)
                    {
                        int quot = Math.DivRem(stilleft, money[i], out int remainder);
                        stilleft = remainder;
                        return_money[i] = quot;
                    }
                }
                return return_money;
            }     
            public static void Display()
            {//Shows information about all products in inventory.
                for (int i = 0; i < Inventory.Count; i++)
                { (Inventory[i] as Products).Examine(); }
            }
            public static void Display(int ansver)
            {//Shows information about a product with a given product number.
                (Inventory[ansver-1] as Products).Examine(); 
            }
            public static void Displaypurchases()
            {//Shows information about purchased products.

                for (int i = 0; i < Purchases.Count; i++)
                    { (Purchases[i] as Products).Examine(); }
            }
            public static void Purchase(int choice)
            {// Adds a product to the basket and adjusts the balance according to price.
                int t = (Inventory[choice-1] as Products).Price();
                if (Balance >= t)
                {
                    Balance = Balance - t;
                    Purchases.Add(Inventory[choice - 1]);
                }
                else { Console.WriteLine( "Not enough money!"); }
            }
           public static  void Unpurchase(int choice)
            { // Removes a product from the basket and adjusts the balance accordingly.
                bool contained=Purchases.Remove(Inventory[choice-1]);
                if (contained==false)
                { Console.WriteLine("You never bought this product!"); }
                else {
                    int t = (Inventory[choice - 1] as Products).Price();
                    Balance = Balance + t;
                    Console.WriteLine($"All {(Inventory[choice - 1] as Products).Name()}s have been removed from your basket");
                }
            }
            public static void Checkout()//The money is returned and the purchases are shown upon leaving.
            { int[] return_money = Cashreturn();
                string money_message = "You are given ";
                for (int i = 0; i < return_money.Length; i++)
                {
                    if ( Denominations[i] < 20)
                    { money_message = money_message + $"{return_money[i]} {Denominations[i]} Kr coins, "; }
                    if (Denominations[i] >=20)
                    { money_message = money_message + $"{return_money[i]} {Denominations[i]} Kr bills, "; }
                }
                
                for (int i = 0; i < Purchases.Count; i++)
                {
                 Console.WriteLine(" You have purchased the following items.");
                 Console.WriteLine($"{(Purchases[i] as Products).Name()}  {(Purchases[i] as Products).Description()}"); }
                Console.WriteLine(money_message + "In return");
                Balance = 0;
            }
            public abstract class Products
            {// Forms the base for all products in the Inventory.
                public abstract string Name();
                public abstract string Category();
                public abstract string Description();
                public abstract int Price();
                public abstract int Product_No();
                public abstract void Examine();

            }
            public class Beverage : Products//Class holding all beverages.
            { public override void Examine()//Displays information returned by the methods below.
                { Console.WriteLine("Name: " + Name() + "Category: " + Category() + "Use " + Description()); }
                public override string Name()
                { return "Category Beverage "; }
                public override string Category()
                { return "Beverage "; }
                public override string Description()
                { return "You can drink the beverage! "; }
                public override int Product_No()
                { return 0; }
                public override int Price()
                { return 0; }
            }

            public class Snack : Products
            {
                public override void Examine()
                { Console.WriteLine("Name: " + Name() + "Category: " + Category() + "Use " + Description()); }
                public override string Name()
                { return "Snack, "; }
                public override string Category()
                { return "Snack, "; }
                public override string Description()
                { return "You can eat the snack! "; }
                public override int Product_No()
                { return 0; }
                public override int Price()
                { return 0; }
            }
            public class Toys : Products
            {
                public override void Examine()
                { Console.WriteLine("Name: " + Name() + "Category: " +Category() + "Use: "+ Description() ); }
                public override string Name()
                { return "Toys, "; }
                public override string Category()
                { return "Toys, "; }
                public override string Description()
                { return "You can play with the toy! "; }
                public override int Product_No()
                { return 0; }
                public override int Price()
                { return 0; }
            }
            public class Snickers_bar : Snack
            {// The class containing the products themselves.
                public override void Examine()// Display information about the product given by methods below. Note that testing is indirect by calling methods that in turn call .Examine().
                { Console.WriteLine("Name: " + Name() + "Category: "+Category() + "Use: " +Description() + "Product No: "+Convert.ToString(Product_No()) + ", Price: "+Convert.ToString(Price()) + " Kr "); }
                public override string Name()
                { return "Snickers_bar, "; }
                public override int Product_No()
                { return 2; }
                public override int Price()
                { return 15; }
            }
            public class Coke_Can : Beverage
            {
                public override void Examine()
                { Console.WriteLine("Name: " + Name() + "Category: " + Category() + "Use: " +Description() + "Product No: " + Convert.ToString(Product_No()) + ", Price: " + Convert.ToString(Price()) + " Kr "); }
                public override string Name()
                { return "Coke_can, "; }
                public override int Product_No()
                { return 1; }
                public override int Price()
                { return 10; }
            }
            public class Teddy_bear : Toys
            {
                public override void Examine()
                { Console.WriteLine("Name: " + Name() + "Category: " + Category() + "Use: " + Description() + "Product No: " + Convert.ToString(Product_No()) + ", Price: " + Convert.ToString(Price()) + " Kr "); }
                public override string Name()
                { return "Teddy_bear, "; }
                public override int Product_No()
                { return 3; }
                public override int Price()
                { return 20; }
            }
        }
    }
    }

                
            

        

