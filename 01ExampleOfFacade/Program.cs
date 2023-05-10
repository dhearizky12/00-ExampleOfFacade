using System;

namespace OrderFoodApp
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderFoodFacade facade = new OrderFoodFacade();

            // Menampilkan daftar menu
            facade.ShowMenu();

            // Menambahkan beberapa item ke pesanan
            facade.AddItemToOrder("Nasi Goreng", 2);
            facade.AddItemToOrder("Ayam Goreng", 3);
            facade.AddItemToOrder("Es Teh", 4);

            // Menghitung total harga pesanan
            int total = facade.CalculateTotal();
            Console.WriteLine($"Total harga pesanan: {total}");
        }
    }
    // Subsystem 1
    public class Menu
    {
        public void ShowMenu()
        {
            Console.WriteLine("Daftar Menu:");
            Console.WriteLine("- Nasi Goreng");
            Console.WriteLine("- Mie Goreng");
            Console.WriteLine("- Ayam Goreng");
            Console.WriteLine("- Es Teh");
            Console.WriteLine("- Es Jeruk");
            Console.WriteLine("- Es Campur");
        }
    }

    // Subsystem 2
    public class Order
    {
        public void AddItem(string item, int qty)
        {
            Console.WriteLine($"Menambahkan {qty} {item} ke pesanan.");
        }

        public int CalculateTotal()
        {
            Console.WriteLine("Menghitung total harga pesanan.");
            return 50000;
        }
    }

    // Facade
    public class OrderFoodFacade
    {
        private readonly Menu _menu;
        private readonly Order _order;

        public OrderFoodFacade()
        {
            _menu = new Menu();
            _order = new Order();
        }

        public void ShowMenu()
        {
            _menu.ShowMenu();
        }

        public void AddItemToOrder(string item, int qty)
        {
            _order.AddItem(item, qty);
        }

        public int CalculateTotal()
        {
            return _order.CalculateTotal();
        }
    }

}
