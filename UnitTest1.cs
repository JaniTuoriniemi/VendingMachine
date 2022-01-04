using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Xunit;

namespace VendingMachine
{
    public class UnitTest1
    {// Test of VendingMachine methods. Note that the methods of the subclasses of VendingMachine.Products
     // are tested indirectly as they are called by the methods under test.

        [Fact]
        public void Test_Deposit()//The balance must equal the amount of money deposited.
        {

            int Balance = 0;
            int[] Denominations = new int[9] { 1, 5, 10, 20, 50, 100, 200, 500, 1000 };
           
                int expectedValue = 1886;
            var input = new StringReader(@"1
1
1
1
1
1
1
1
1");
            Console.SetIn(input);
            Balance = Program.VendingMachine.Deposit(Denominations, Balance);


                Assert.Equal(expectedValue, Balance);
            
        }
        [Fact]
        public void Test_Cashreturn()
        {//The money must be returned in the proper set of valeurs.
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            Program.VendingMachine.Balance = 152;
            int[] expectedValue = new int[9] { 2, 0, 0, 0, 1, 1, 0, 0, 0};
            int[] ret = Program.VendingMachine.Cashreturn();
            Assert.Equal(expectedValue, ret);
        }
        [Fact]
        public void Test_Cashreturn2()
        {//The money must be returned in the proper set of valeurs.Second test.
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            Program.VendingMachine.Balance = 1005;
            int[] expectedValue = new int[9] { 0, 1, 0, 0, 0, 0, 0, 0, 1 };
            int[] ret = Program.VendingMachine.Cashreturn();
            Assert.Equal(expectedValue, ret);
        }
        [Fact]
        public void Test_Display()
        {// Tests that information about the products are displayed correctly. 
            //Note! The VendingMachine.Coke_Can.Examine() and analogous are tested here indirectly.
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            var output = new StringWriter();
               Console.SetOut(output);
            Program.VendingMachine.Display();
             var expected = @"Name: Coke_can, Category: Beverage Use: You can drink the beverage! Product No: 1, Price: 10 Kr 
Name: Snickers_bar, Category: Snack, Use: You can eat the snack! Product No: 2, Price: 15 Kr 
Name: Teddy_bear, Category: Toys, Use: You can play with the toy! Product No: 3, Price: 20 Kr 
";

           Assert.Equal(expected, output.ToString());
        }
        [Fact]
        public static void Test_Ansver_Display()
        {// Tests that information about products are displayed correctly upon request by user. 
            //Note! The VendingMachine.Coke_Can.Examine() and analogous are tested here indirectly.
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            int ansver1 = 1;
                int ansver2 = 2;
                int ansver3 = 3;
            var output = new StringWriter();
            Console.SetOut(output);
            Program.VendingMachine.Display(ansver1);
            Program.VendingMachine.Display(ansver2);
            Program.VendingMachine.Display(ansver3);
            var expected = @"Name: Coke_can, Category: Beverage Use: You can drink the beverage! Product No: 1, Price: 10 Kr 
Name: Snickers_bar, Category: Snack, Use: You can eat the snack! Product No: 2, Price: 15 Kr 
Name: Teddy_bear, Category: Toys, Use: You can play with the toy! Product No: 3, Price: 20 Kr 
";
            Assert.Equal(expected, output.ToString());
        }
        [Fact]
        public static void Test_Displaypurchases()
        {// Tests that information about purchased products are displayed correctly. 
            //Note! The VendingMachine.Coke_Can.Examine() and analogous are tested here indirectly.
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            Program.VendingMachine.Purchases.Add(Program.VendingMachine.Inventory[0]);
            Program.VendingMachine.Purchases.Add(Program.VendingMachine.Inventory[1]);
            Program.VendingMachine.Purchases.Add(Program.VendingMachine.Inventory[2]);
            var output = new StringWriter();
            Console.SetOut(output);
            Program.VendingMachine.Displaypurchases();

           var expected = @"Name: Coke_can, Category: Beverage Use: You can drink the beverage! Product No: 1, Price: 10 Kr 
Name: Snickers_bar, Category: Snack, Use: You can eat the snack! Product No: 2, Price: 15 Kr 
Name: Teddy_bear, Category: Toys, Use: You can play with the toy! Product No: 3, Price: 20 Kr 
";

            Assert.Equal(expected, output.ToString());

        }
        [Fact]
        public static void Test_Purchase()
        {// Test for that purchase is executed and balance is reset correctly. 
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            Program.VendingMachine.Balance = 15;
            Program.VendingMachine.Purchase(1);
            var output = new StringWriter();
            Console.SetOut(output);
            Program.VendingMachine.Displaypurchases();
            var expected = @"Name: Coke_can, Category: Beverage Use: You can drink the beverage! Product No: 1, Price: 10 Kr 
";
            int expected_balance = 5;
            Assert.Equal(expected, output.ToString());
            Assert.Equal(expected_balance, Program.VendingMachine.Balance);
        }
        [Fact]
        public static void Test_Purchase2()
        {// Test for that correct message is shown when there is not enough money for a purchase.
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            Program.VendingMachine.Balance = 0;
            var output = new StringWriter();
            Console.SetOut(output);
            Program.VendingMachine.Purchase(1);
            var expected = @"Not enough money!
";
            int expected_balance = 0;
            Assert.Equal(expected, output.ToString());
            Assert.Equal(expected_balance, Program.VendingMachine.Balance);
        }
        [Fact]
        public static void Test_Unpurchase()
        {// Test for that products are removed from the basket and balance is reset correctly.
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            Program.VendingMachine.Purchases.Add(Program.VendingMachine.Inventory[0]);
            Program.VendingMachine.Balance = 0;
            Program.VendingMachine.Unpurchase(1);
            int a = Program.VendingMachine.Purchases.Count;
            int expected = 0;
            int expected_balance = 10;
            Assert.Equal(expected, a);
            Assert.Equal(expected_balance, Program.VendingMachine.Balance);
        }
        [Fact]
        public static void Test_Checkout()
        {// Test for that the correct message is shown upon checkout and balance is rezeroed.
            Program.VendingMachine vendingMachine = new Program.VendingMachine();
            Program.VendingMachine.Balance = 57;
            Program.VendingMachine.Purchases.Add(Program.VendingMachine.Inventory[0]);
            Program.VendingMachine.Purchases.Add(Program.VendingMachine.Inventory[1]);
            Program.VendingMachine.Purchases.Add(Program.VendingMachine.Inventory[2]);
            var output = new StringWriter();
            Console.SetOut(output);
            Program.VendingMachine.Checkout();

            var expected = @" You have purchased the following items.
Coke_can,   You can drink the beverage! 
 You have purchased the following items.
Snickers_bar,   You can eat the snack! 
 You have purchased the following items.
Teddy_bear,   You can play with the toy! 
You are given 2 1 Kr coins, 1 5 Kr coins, 0 10 Kr coins, 0 20 Kr bills, 1 50 Kr bills, 0 100 Kr bills, 0 200 Kr bills, 0 500 Kr bills, 0 1000 Kr bills, In return
";
            int expected_balance = 0;
            Assert.Equal(expected, output.ToString());
            Assert.Equal(expected_balance, Program.VendingMachine.Balance);
        }
       
    }
}
