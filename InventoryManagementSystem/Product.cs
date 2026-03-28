using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    // Represents a cafe product/item in the inventory
        class Product
        {
            // Properties
            public int Id { get; private set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public int Stock { get; set; }
            public int CategoryId { get; set; }
            public int SupplierId { get; set; }

            // Constructor
            public Product(int id, string name, double price, int stock, int categoryId, int supplierId)
            {
                Id = id;
                Name = name;
                Price = price;
                Stock = stock;
                CategoryId = categoryId;
                SupplierId = supplierId;
            }

            // Method to display product info
            public void Display(List<Category> categories, List<Supplier> suppliers)
            {
                // Find the category name using CategoryId
                string categoryName = "Unknown";
                foreach (Category c in categories)
                {
                    if (c.Id == CategoryId)
                    {
                        categoryName = c.Name;
                        break;
                    }
                }

                // Find the supplier name using SupplierId
                string supplierName = "Unknown";
                foreach (Supplier s in suppliers)
                {
                    if (s.Id == SupplierId)
                    {
                        supplierName = s.Name;
                        break;
                    }
                }

                Console.WriteLine($"  ID: {Id} | {Name} | Price: PHP {Price:F2} | Stock: {Stock} | Category: {categoryName} | Supplier: {supplierName}");
        }
        }
    
}
