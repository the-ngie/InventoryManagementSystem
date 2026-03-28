using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    // Records every restock or deduct action done on a cafe product
        class TransactionRecord
        {
            // Properties
            public int Id { get; private set; }
            public string Type { get; private set; }        // "Restock" or "Deduct"
            public int ProductId { get; private set; }
            public string ProductName { get; private set; }
            public int Quantity { get; private set; }
            public DateTime Date { get; private set; }
            public string PerformedBy { get; private set; } // The cafe staff who did the action

            // Constructor
            public TransactionRecord(int id, string type, int productId, string productName, int quantity, string performedBy)
            {
                Id = id;
                Type = type;
                ProductId = productId;
                ProductName = productName;
                Quantity = quantity;
                Date = DateTime.Now; // Automatically record the current date and time
                PerformedBy = performedBy;
            }

            // Method to display transaction info
            public void Display()
            {
                Console.WriteLine($"  [{Id}] {Date:yyyy-MM-dd HH:mm} | {Type} | Item: {ProductName} | Qty: {Quantity} | By: {PerformedBy}");
            }
        }
    }
