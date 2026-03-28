using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    // category of cafe products
    // Coffee & Espresso, Pastries, Cold Drinks
        class Category
        {
            public int Id { get; private set; }
            public string Name { get; set; }

            public Category(int id, string name)
            {
                Id = id;
                Name = name;
            }

            // Method that display category info
            public void Display()
            {
                Console.WriteLine($"  [{Id}] {Name}");
            }
        }
  
}

