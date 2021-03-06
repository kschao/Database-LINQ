using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne(); // completed
            //ProblemTwo();
            //ProblemThree(); // completed
            //ProblemFour(); // completed
            //ProblemFive(); // completed
            //ProblemSix(); // completed 
            //ProblemSeven();
            //ProblemEight(); // completed
            //ProblemNine(); // completed
            //ProblemTen(); // completed
            //ProblemEleven();
            //ProblemTwelve(); completed
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var users = _context.Users.ToList().Count;
            Console.WriteLine(users);
                        

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;
            var productsValue = products.Where(p => p.Price > 150);
            foreach (var product in productsValue) 

                {
                    Console.WriteLine(product.Name + " " + product.Price);
                };

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.

            var products = _context.Products;
            var prodcutsWithS = products.Where(p => p.Name.Contains("s"));
            foreach (var product in prodcutsWithS)
            {
                Console.WriteLine(product.Name);
            }

        }

        private void ProblemFive()
        {

            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            var users = _context.Users;
            var date = new DateTime(2016, 1, 1, 0, 0, 0);

            var userPrior2016 = users.Where(user => user.RegistrationDate < date);
            foreach (var userDate in userPrior2016)
            {
                Console.WriteLine(userDate.Email + " " + userDate.RegistrationDate);
            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.

            var users = _context.Users;
            var priorDate = new DateTime(2018, 1, 1, 0, 0, 0);
            var afterDate = new DateTime(2016, 1, 1, 0, 0, 0);

            var userPeriodTime = users.Where(user => priorDate > user.RegistrationDate && user.RegistrationDate > afterDate);
            foreach (var userDate in userPeriodTime)
            {
                Console.WriteLine(userDate.Email + " " + userDate.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            var customerCart = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == "afton@gmail.com");

            foreach (ShoppingCart cart in customerCart)
            {
                Console.WriteLine(cart.Product.Name + " " + cart.Product.Price + " " + cart.Quantity);
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var cartTotal = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine(cartTotal);
        }

        private void ProblemTen()
        {   
            var selectEmployees = _context.UserRoles.Include(ur => ur.Role).Where(ur => ur.Role.RoleName == "Employee").Select(ur => ur.UserId);

            var shopCartContents = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => selectEmployees.Contains(ur.UserId));

            foreach(var content in shopCartContents)
            {
                Console.WriteLine(content.User.Email + " " + content.Product.Name + " " + content.Product.Price + " " + content.Quantity);
            }
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

           
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Wrestle Greased Up Midgets",
                Description = "Slippery Little Buggers",
                Price = 5
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();


        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Wrestle Greased Up Midgets").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();

            ShoppingCart newItem = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newItem);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.

            var product = _context.Products.Where(p => p.Name == "Wrestle Greased Up Midgets").SingleOrDefault();
            product.Price = 120;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.

            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.

            var user = _context.Users.Where(ur => ur.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".

            Console.WriteLine("Please enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();

            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    Console.WriteLine("Signed in!");
                }
                Console.WriteLine("Invalid email or password!");
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.

            var users = _context.Users;
            var cart = _context.ShoppingCarts.Include(p => p.Product).ToList();
            decimal totalSum = 0;
            foreach (var user in users)
            {
                decimal sum = 0;
                foreach (var c in cart)
                {
                    if (user.Id == c.UserId)
                    {
                        sum += c.Product.Price * Convert.ToDecimal(c.Quantity);
                    }
                }
                Console.WriteLine(user.Id + "--Sum: " + sum);
                totalSum += sum;

            }
        }

        // BIG ONE
        private void BonusThree()
        {
            Console.WriteLine("Please enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    Console.WriteLine("Signed in!"); ;
                    bool loop = true;
                    while (loop)
                    {
                        Console.WriteLine(@"Please select an option:
                                        1-Products in Cart
                                        2-View all products
                                        3-Add to cart
                                        4-Remove item from cart
                                        5-Exit");
                        string userInput = Console.ReadLine();
                        switch (userInput)
                        {
                            case "1":
                                var customerCart = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == email);
                                foreach (ShoppingCart car in customerCart)
                                {
                                    Console.WriteLine(customerCart.Product.Name + " " + customerCart.Product.Price + " " + customerCart.Quantity);
                                }
                                break;
                            case "2":
                                var productsAll = _context.Products;
                                foreach (var product in productsAll)
                                {
                                    Console.WriteLine("All Products");
                                    Console.WriteLine(product.Name + " " + product.Description + " " + product.Price);
                                }
                                break;
                            case "3":
                                Console.WriteLine("Please type name of the product of your choice");
                                string chooseProduct = Console.ReadLine();

                                var customersCart = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == email).ToList();
                                var selectProduct = _context.Products.Where(p => p.Name == chooseProduct).Select(password.Id).SingleOrDefault();
                                var shoppCart = _context.ShoppingCarts.Include(p => p.Product);
                                bool breakfor = false;

                                foreach (var cart2 in customersCart)
                                {
                                    if (cart2.Product.Name == chooseProduct)
                                    { 
                                        customersCart.Quantity +=1;
                                        _context.ShoppingCarts.Update(customersCart);
                                        breakfor = true;
                                    }
                                 }
                    if (!breakfor)
                    {
                        ShoppingCart newItem = new ShoppingCart()
                        {
                            UserId = user.Id,
                            ProductId = Convert.ToInt32(selectProduct),
                            Quantity = 1
                        };
                        _context.ShoppingCarts.Add(newItem);
                        _context.SaveChanges();
                        Console.WriteLine("ItemAdded!");
                        break;
                    }
                    break;
                case "4":
                    var productInCartId = _context.ShoppingCarts.Include(p => p.Product).Where (c => c.UserId == user.Id);
                    var currentProductInCart = _context.Shoppingcarts.Include(p => p.Product);
                    foreach (var cartObject in currentProductInCart)
                    {
                        Console.WriteLine(cartObject.Product.Name);
                    }
                    Console.WriteLine("Please enter the item you wish to remove");
                    string removeItem = Console.ReadLine();
                    var removeItemId = _context.Products.Where(p => p.Name == removeItem).Select(p => p.Id).SingleOrDefault();
                    removeItemId = Convert.ToInt32(removeItemId);
                    var itemRemove = _context.ShoppingCarts.Where(i => i.UserId == user.Id && i.ProductId == removeItem).SingleOrDefault();
                    _context.Shoppingcarts.Remove(itemRemove);
                    _context.SaveChanges();
                
                case "5":
                    loop = false;
                    break;
                default:
                    Console.WriteLine("Please select an opton");
                    break;
              }
            }
       }
    }
    Console.WriteLine("Invalid Email or Password"); ;
    BonusThree();
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

        }

    }
}
