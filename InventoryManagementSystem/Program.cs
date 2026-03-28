using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InventoryManagementSystem
{
    class Program
    {
        static List<Category> categories = new List<Category>();
        static List<Supplier> suppliers = new List<Supplier>();
        static List<Product> products = new List<Product>();
        static List<TransactionRecord> transactions = new List<TransactionRecord>();

        static int categoryIdCounter = 1;
        static int supplierIdCounter = 1;
        static int productIdCounter = 1;
        static int transactionIdCounter = 1;

        static User currentUser = new User(1, "Admin");

        static int LOW_STOCK_LIMIT = 5;

        // ENTRY POINT
        static void Main(string[] args)
        {
            Console.WriteLine("============================================");
            Console.WriteLine("   Welcome to the Cafe Inventory System!");
            Console.WriteLine($"   Logged in as: {currentUser.Username}");
            Console.WriteLine("============================================");

            AddSampleData();

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine(); // Wait for user to press Enter THEN clear

            bool running = true;
            while (running)
            {
                Console.Clear(); // ✅ Clear screen here before showing menu
                ShowMainMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddCategory(); break;
                    case "2": AddSupplier(); break;
                    case "3": AddProduct(); break;
                    case "4": ViewAllProducts(); break;
                    case "5": SearchProduct(); break;
                    case "6": UpdateProduct(); break;
                    case "7": DeleteProduct(); break;
                    case "8": RestockProduct(); break;
                    case "9": DeductStock(); break;
                    case "10": ViewTransactionHistory(); break;
                    case "11": ShowLowStockItems(); break;
                    case "12": ComputeTotalInventoryValue(); break;
                    case "0":
                        Console.WriteLine("\nGoodbye! Closing the Cafe Inventory System...");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\n[!] Invalid choice. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress Enter to go back to the menu...");
                    Console.ReadLine();
                }
            }
        }

        // DISPLAY THE MAIN MENU
        static void ShowMainMenu()
        {
            // Console.Clear() was moved to the while loop above
            Console.WriteLine("============================================");
            Console.WriteLine("     CAFE INVENTORY MANAGEMENT SYSTEM");
            Console.WriteLine($"     Staff: {currentUser.Username}");
            Console.WriteLine("============================================");
            Console.WriteLine(" [1]  Add Category");
            Console.WriteLine(" [2]  Add Supplier");
            Console.WriteLine(" [3]  Add Cafe Item");
            Console.WriteLine(" [4]  View All Cafe Items");
            Console.WriteLine(" [5]  Search Cafe Item");
            Console.WriteLine(" [6]  Update Cafe Item");
            Console.WriteLine(" [7]  Delete Cafe Item");
            Console.WriteLine(" [8]  Restock Item");
            Console.WriteLine(" [9]  Deduct Stock");
            Console.WriteLine(" [10] View Transaction History");
            Console.WriteLine(" [11] Show Low Stock Items");
            Console.WriteLine(" [12] Compute Total Inventory Value");
            Console.WriteLine(" [0]  Exit");
            Console.WriteLine("============================================");
            Console.Write("Enter your choice: ");
        }

        // FEATURE 1: ADD CATEGORY
        static void AddCategory()
        {
            Console.WriteLine("\n--- Add New Category ---");

            try
            {
                Console.Write("Category Name (e.g. Coffee, Pastries): ");
                string name = Console.ReadLine();

                // Validate: name must not be empty
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("[!] Category name cannot be empty!");
                    return;
                }

                // Create a new Category object and add it to the list
                Category newCategory = new Category(categoryIdCounter, name.Trim());
                categories.Add(newCategory);
                categoryIdCounter++; // Increment ID for the next category

                Console.WriteLine($"[+] Category '{name}' added successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Error: {e.Message}");
            }
        }

        // FEATURE 2: ADD SUPPLIER
        static void AddSupplier()
        {
            Console.WriteLine("\n--- Add New Supplier ---");

            try
            {
                Console.Write("Supplier Name (e.g. Brew Masters Co.): ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("[!] Supplier name cannot be empty!");
                    return;
                }

                Console.Write("Contact Number: ");
                string contact = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(contact))
                {
                    Console.WriteLine("[!] Contact number cannot be empty!");
                    return;
                }

                // Create a new Supplier object and add it to the list
                Supplier newSupplier = new Supplier(supplierIdCounter, name.Trim(), contact.Trim());
                suppliers.Add(newSupplier);
                supplierIdCounter++;

                Console.WriteLine($"[+] Supplier '{name}' added successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Error: {e.Message}");
            }
        }

        // FEATURE 3: ADD PRODUCT (CAFE ITEM)
        static void AddProduct()
        {
            Console.WriteLine("\n--- Add New Cafe Item ---");

            try
            {
                // Must have at least one category before adding an item
                if (categories.Count == 0)
                {
                    Console.WriteLine("[!] No categories found. Please add a category first.");
                    return;
                }

                Console.WriteLine("Available Categories:");
                foreach (Category c in categories)
                    c.Display();

                // Must have at least one supplier before adding an item
                if (suppliers.Count == 0)
                {
                    Console.WriteLine("[!] No suppliers found. Please add a supplier first.");
                    return;
                }

                Console.WriteLine("Available Suppliers:");
                foreach (Supplier s in suppliers)
                    s.Display();

                Console.Write("Item Name (e.g. Caramel Latte): ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("[!] Item name cannot be empty!");
                    return;
                }

                // Keep asking until a valid price is entered
                double price = 0;
                while (true)
                {
                    Console.Write("Price (in PHP): ");
                    string priceInput = Console.ReadLine();

                    if (!double.TryParse(priceInput, out price))
                    {
                        Console.WriteLine("[!] Invalid price! Please enter a valid number.");
                        continue; // Ask again
                    }

                    if (price < 0)
                    {
                        Console.WriteLine("[!] Price cannot be negative! Please try again.");
                        continue; // Ask again
                    }

                    break; // Price is valid, exit the loop
                }

                // Keep asking until a valid stock quantity is entered
                int stock = 0;
                while (true)
                {
                    Console.Write("Stock Quantity: ");
                    string stockInput = Console.ReadLine();

                    if (!int.TryParse(stockInput, out stock))
                    {
                        Console.WriteLine("[!] Invalid quantity! Please enter a whole number.");
                        continue; // Ask again
                    }

                    if (stock < 0)
                    {
                        Console.WriteLine("[!] Stock cannot be negative! Please try again.");
                        continue; // Ask again
                    }

                    break; // Stock is valid, exit the loop
                }

                // Keep asking until a valid category ID is entered
                int categoryId = 0;
                while (true)
                {
                    Console.Write("Category ID: ");
                    string catInput = Console.ReadLine();

                    if (!int.TryParse(catInput, out categoryId))
                    {
                        Console.WriteLine("[!] Invalid ID! Please enter a valid number.");
                        continue;
                    }

                    // Check if the category ID exists in our list
                    bool categoryExists = false;
                    foreach (Category c in categories)
                    {
                        if (c.Id == categoryId)
                        {
                            categoryExists = true;
                            break;
                        }
                    }

                    if (!categoryExists)
                    {
                        Console.WriteLine("[!] Category ID not found! Please try again.");
                        continue;
                    }

                    break; // Valid category ID, exit the loop
                }

                // Keep asking until a valid supplier ID is entered
                int supplierId = 0;
                while (true)
                {
                    Console.Write("Supplier ID: ");
                    string supInput = Console.ReadLine();

                    if (!int.TryParse(supInput, out supplierId))
                    {
                        Console.WriteLine("[!] Invalid ID! Please enter a valid number.");
                        continue;
                    }

                    // Check if the supplier ID exists in our list
                    bool supplierExists = false;
                    foreach (Supplier s in suppliers)
                    {
                        if (s.Id == supplierId)
                        {
                            supplierExists = true;
                            break;
                        }
                    }

                    if (!supplierExists)
                    {
                        Console.WriteLine("[!] Supplier ID not found! Please try again.");
                        continue;
                    }

                    break; // Valid supplier ID, exit the loop
                }

                // Create and add the new cafe item
                Product newProduct = new Product(productIdCounter, name.Trim(), price, stock, categoryId, supplierId);
                products.Add(newProduct);
                productIdCounter++;

                Console.WriteLine($"[+] Cafe item '{name}' added successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Error: {e.Message}");
            }
        }

        // FEATURE 4: VIEW ALL PRODUCTS (CAFE ITEMS)
        static void ViewAllProducts()
        {
            Console.WriteLine("\n--- All Cafe Items ---");

            if (products.Count == 0)
            {
                Console.WriteLine("  No items found.");
                return;
            }

            // Loop through and display each cafe item
            foreach (Product p in products)
            {
                p.Display(categories, suppliers);
            }

            Console.WriteLine($"\n  Total: {products.Count} item(s)");
        }

        // FEATURE 5: SEARCH PRODUCT (CAFE ITEM)
        static void SearchProduct()
        {
            Console.WriteLine("\n--- Search Cafe Item ---");

            try
            {
                Console.Write("Enter item name to search: ");
                string keyword = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    Console.WriteLine("[!] Search keyword cannot be empty!");
                    return;
                }

                bool found = false;

                // Look for items whose name contains the keyword (case-insensitive)
                foreach (Product p in products)
                {
                    if (p.Name.ToLower().Contains(keyword.ToLower()))
                    {
                        p.Display(categories, suppliers);
                        found = true;
                    }
                }

                if (!found)
                    Console.WriteLine($"  No item found with keyword: '{keyword}'");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Error: {e.Message}");
            }
        }

        // FEATURE 6: UPDATE PRODUCT (CAFE ITEM)
        static void UpdateProduct()
        {
            Console.WriteLine("\n--- Update Cafe Item ---");

            try
            {
                ViewAllProducts();
                if (products.Count == 0) return;

                Console.Write("\nEnter Item ID to update: ");
                int id = int.Parse(Console.ReadLine());

                // Find the item with matching ID
                Product found = null;
                foreach (Product p in products)
                {
                    if (p.Id == id)
                    {
                        found = p;
                        break;
                    }
                }

                if (found == null)
                {
                    Console.WriteLine("[!] Item not found!");
                    return;
                }

                // Press Enter to keep the old value, or type a new one
                Console.Write($"New Name [{found.Name}]: ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    found.Name = newName;

                Console.Write($"New Price [{found.Price}]: ");
                string newPriceStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPriceStr))
                {
                    double newPrice = double.Parse(newPriceStr);
                    if (newPrice < 0)
                        Console.WriteLine("[!] Price not updated. Negative price is not allowed.");
                    else
                        found.Price = newPrice;
                }

                Console.WriteLine("[+] Cafe item updated successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("[!] Invalid input! Please enter a valid number.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Error: {e.Message}");
            }
        }

        // FEATURE 7: DELETE PRODUCT (CAFE ITEM)
        static void DeleteProduct()
        {
            Console.WriteLine("\n--- Delete Cafe Item ---");

            try
            {
                ViewAllProducts();
                if (products.Count == 0) return;

                Console.Write("\nEnter Item ID to delete: ");
                int id = int.Parse(Console.ReadLine());

                // Find the item
                Product toDelete = null;
                foreach (Product p in products)
                {
                    if (p.Id == id)
                    {
                        toDelete = p;
                        break;
                    }
                }

                if (toDelete == null)
                {
                    Console.WriteLine("[!] Item not found!");
                    return;
                }

                // Ask for confirmation before deleting
                Console.Write($"Are you sure you want to delete '{toDelete.Name}'? (yes/no): ");
                string confirm = Console.ReadLine();

                if (confirm.ToLower() == "yes")
                {
                    products.Remove(toDelete);
                    Console.WriteLine("[+] Cafe item deleted successfully!");
                }
                else
                {
                    Console.WriteLine("[-] Delete cancelled.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("[!] Invalid input! Please enter a valid number.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Error: {e.Message}");
            }
        }

        // FEATURE 8: RESTOCK PRODUCT (CAFE ITEM)
        static void RestockProduct()
        {
            Console.WriteLine("\n--- Restock Cafe Item ---");

            try
            {
                ViewAllProducts();
                if (products.Count == 0) return;

                Console.Write("\nEnter Item ID to restock: ");
                int id = int.Parse(Console.ReadLine());

                // Find the item
                Product found = null;
                foreach (Product p in products)
                {
                    if (p.Id == id)
                    {
                        found = p;
                        break;
                    }
                }

                if (found == null)
                {
                    Console.WriteLine("[!] Item not found!");
                    return;
                }

                Console.Write($"How many units to add (current stock: {found.Stock}): ");
                int qty = int.Parse(Console.ReadLine());

                if (qty <= 0)
                {
                    Console.WriteLine("[!] Quantity must be greater than zero!");
                    return;
                }

                // Add the units to the existing stock
                found.Stock += qty;

                // Save this action as a transaction record
                TransactionRecord record = new TransactionRecord(
                    transactionIdCounter, "Restock", found.Id, found.Name, qty, currentUser.Username
                );
                transactions.Add(record);
                transactionIdCounter++;

                Console.WriteLine($"[+] Restocked '{found.Name}'. New stock: {found.Stock}");
            }
            catch (FormatException)
            {
                Console.WriteLine("[!] Invalid input! Please enter a valid number.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Error: {e.Message}");
            }
        }

        // FEATURE 9: DEDUCT STOCK
        static void DeductStock()
        {
            Console.WriteLine("\n--- Deduct Stock ---");

            try
            {
                ViewAllProducts();
                if (products.Count == 0) return;

                Console.Write("\nEnter Item ID to deduct stock from: ");
                int id = int.Parse(Console.ReadLine());

                // Find the item
                Product found = null;
                foreach (Product p in products)
                {
                    if (p.Id == id)
                    {
                        found = p;
                        break;
                    }
                }

                if (found == null)
                {
                    Console.WriteLine("[!] Item not found!");
                    return;
                }

                Console.Write($"How many units to deduct (current stock: {found.Stock}): ");
                int qty = int.Parse(Console.ReadLine());

                if (qty <= 0)
                {
                    Console.WriteLine("[!] Quantity must be greater than zero!");
                    return;
                }

                // Make sure we're not deducting more than what's available
                if (qty > found.Stock)
                {
                    Console.WriteLine($"[!] Not enough stock! Current stock is only {found.Stock}.");
                    return;
                }

                // Subtract the units from the existing stock
                found.Stock -= qty;

                // Save this action as a transaction record
                TransactionRecord record = new TransactionRecord(
                    transactionIdCounter, "Deduct", found.Id, found.Name, qty, currentUser.Username
                );
                transactions.Add(record);
                transactionIdCounter++;

                Console.WriteLine($"[+] Deducted {qty} from '{found.Name}'. New stock: {found.Stock}");
            }
            catch (FormatException)
            {
                Console.WriteLine("[!] Invalid input! Please enter a valid number.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Error: {e.Message}");
            }
        }

        // FEATURE 10: VIEW TRANSACTION HISTORY
        static void ViewTransactionHistory()
        {
            Console.WriteLine("\n--- Transaction History ---");

            if (transactions.Count == 0)
            {
                Console.WriteLine("  No transactions yet.");
                return;
            }

            foreach (TransactionRecord t in transactions)
            {
                t.Display();
            }

            Console.WriteLine($"\n  Total: {transactions.Count} transaction(s)");
        }

        // FEATURE 11: SHOW LOW STOCK ITEMS
        static void ShowLowStockItems()
        {
            Console.WriteLine($"\n--- Low Stock Cafe Items (stock <= {LOW_STOCK_LIMIT}) ---");

            bool anyFound = false;

            foreach (Product p in products)
            {
                // Display items that are at or below the low stock limit
                if (p.Stock <= LOW_STOCK_LIMIT)
                {
                    p.Display(categories, suppliers);
                    anyFound = true;
                }
            }

            if (!anyFound)
                Console.WriteLine("  All cafe items are sufficiently stocked!");
        }

        // FEATURE 12: COMPUTE TOTAL INVENTORY VALUE
        static void ComputeTotalInventoryValue()
        {
            Console.WriteLine("\n--- Total Cafe Inventory Value ---");

            if (products.Count == 0)
            {
                Console.WriteLine("  No items in inventory.");
                return;
            }

            double totalValue = 0;

            // Total value = price x stock for every cafe item
            foreach (Product p in products)
            {
                double itemValue = p.Price * p.Stock;
                Console.WriteLine($"  {p.Name}: PHP {p.Price:F2} x {p.Stock} = PHP {itemValue:F2}");
                totalValue += itemValue;
            }

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"  TOTAL CAFE INVENTORY VALUE: PHP {totalValue:F2}");
        }

        // sample cafe data
        static void AddSampleData()
        {
            // Sample cafe categories
            categories.Add(new Category(categoryIdCounter++, "Coffee & Espresso"));
            categories.Add(new Category(categoryIdCounter++, "Pastries & Bread"));
            categories.Add(new Category(categoryIdCounter++, "Cold Drinks"));

            // Sample cafe suppliers
            suppliers.Add(new Supplier(supplierIdCounter++, "Brew Masters Co.", "0917-111-2222"));
            suppliers.Add(new Supplier(supplierIdCounter++, "Golden Bakery Supply", "0918-333-4444"));
            suppliers.Add(new Supplier(supplierIdCounter++, "CoolDrinks Distributor", "0919-555-6666"));

            // Sample cafe products
            // Caramel Latte and Blueberry Muffin have low stock for testing Feature 11
            products.Add(new Product(productIdCounter++, "Espresso Shot", 60.00, 100, 1, 1));
            products.Add(new Product(productIdCounter++, "Caramel Latte", 150.00, 4, 1, 1));
            products.Add(new Product(productIdCounter++, "Croissant", 85.00, 30, 2, 2));
            products.Add(new Product(productIdCounter++, "Blueberry Muffin", 75.00, 3, 2, 2));
            products.Add(new Product(productIdCounter++, "Iced Matcha", 130.00, 50, 3, 3));
        }
    }
}