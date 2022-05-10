using System;
using System.IO;
using System.Collections.Generic;

namespace iotask
{
    public class Item
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public Item(string title, int quantity, int unitPrice)
        {
            Title = title;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
    class Program
    {
        public static List<Item> Items = new List<Item>();
        static void Main(string[] args)
        {
            if (File.Exists($@"{Environment.CurrentDirectory}\ShoppingList.csv"))
            {
                using StreamReader file = new StreamReader($@"{Environment.CurrentDirectory}\ShoppingList.csv");
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    string[] parts = ln.Split(',');
                    Items.Add(new Item(parts[0], Int32.Parse(parts[1]), Int32.Parse(parts[2])));
                }
                file.Close();
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add New Item\n2. List All Items\n3. Show Total Cost\n4. Clear List\n5. Save List\n6. Exit");
                switch (Console.ReadKey().Key.ToString())
                {
                    case "D1":
                        Console.Clear();
                        Add();
                        continue;
                    case "D2":
                        Console.Clear();
                        List();
                        Console.ReadKey();
                        continue;
                    case "D3":
                        Console.Clear();
                        Cost();
                        Console.ReadKey();
                        continue;
                    case "D4":
                        Console.Clear();
                        Clear();
                        continue;
                    case "D5":
                        Console.Clear();
                        Save();
                        Console.WriteLine("Saved");
                        Console.ReadKey();
                        continue;
                    case "D6":
                        Console.Clear();
                        Console.WriteLine("Thanks for using the Shopping List App");
                        break;
                    default:
                        continue;
                }
                break;
            }
        }
        public static void Add()
        {
            Console.WriteLine("You've selected to add a new item");

            Console.WriteLine("Please enter the title of the item: ");
            string title = Console.ReadLine();
            Console.WriteLine("Please enter the quantity of the item: ");
            int quantity = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the unit price of the item: ");
            int unitPrice = Int32.Parse(Console.ReadLine());

            Items.Add(new Item(title, quantity, unitPrice));
            Console.WriteLine("You have successfully added an item");
        }
        public static void List()
        {
            Console.WriteLine("Title | Quantity | UnitPrice ");
            foreach (var item in Items)
            {
                Console.WriteLine($"{item.Title} | {item.Quantity} | {item.UnitPrice}");
            }
        }

        public static void Cost()
        {
            int sum = 0;
            foreach (var item in Items)
            {
                var totalItemCost = item.Quantity * item.UnitPrice;
                sum += totalItemCost;
            }
            Console.WriteLine($"Total cost is {sum}");
        }

        public static void Clear()
        {
            Items = new List<Item>();
        }

        public static void Save()
        {
            StreamWriter writer = new StreamWriter($@"{Environment.CurrentDirectory}\ShoppingList.csv");
            foreach (var item in Items)
            {
                writer.WriteLine($"{item.Title},{item.UnitPrice},{item.Quantity}");
            }
            writer.Close();
        }
    }
}
