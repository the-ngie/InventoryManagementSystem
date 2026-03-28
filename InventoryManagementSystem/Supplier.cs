using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    // Represents a supplier/vendor
        class Supplier
        {
            public int Id { get; private set; }
            public string Name { get; set; }
            public string ContactNumber { get; set; }

            // Constructor
            public Supplier(int id, string name, string contactNumber)
            {
                Id = id;
                Name = name;
                ContactNumber = contactNumber;
            }

            // Method to display supplier info
            public void Display()
            {
                Console.WriteLine($"  [{Id}] {Name} | Contact: {ContactNumber}");
            }
        }
 }
