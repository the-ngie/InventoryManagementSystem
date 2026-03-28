using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    // Represents the cafe staff member using the system

        class User
        {
            // Properties
            public int Id { get; private set; }
            public string Username { get; set; }

            // Constructor
            public User(int id, string username)
            {
                Id = id;
                Username = username;
            }
        }
}
