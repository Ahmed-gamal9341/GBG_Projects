namespace CarRentalApp;

class Program
{
    static void Main(string[] args)
    {
        IVehicleService vehicleService = new VehicleManager();
        ManagerMenu managerMenu = new ManagerMenu(vehicleService);

        Console.WriteLine("===== CAR RENTAL SYSTEM =====");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Manager");

        Console.Write("Login: ");

        if (!int.TryParse(Console.ReadLine(), out int signin))
        {
            Console.WriteLine("Invalid input!");
            return;
        }

        switch (signin)
        {
            case 1:
                Console.Write("Enter your SSN: ");

                if (!int.TryParse(Console.ReadLine(), out int ssn))
                {
                    Console.WriteLine("Invalid SSN.");
                    return;
                }

                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    return;
                }

                Console.Write("Enter your phone number: ");

                if (!int.TryParse(Console.ReadLine(), out int phoneNumber))
                {
                    Console.WriteLine("Invalid phone number.");
                    return;
                }

                User user = new User(ssn, name, phoneNumber);

                Console.WriteLine($"\nWelcome, {user.Name}!");

                UserMenu customerMenu = new UserMenu(user, vehicleService);
                customerMenu.Show();
                break;

            case 2:
                Console.WriteLine("\nWelcome Manager!");

                managerMenu.Show();
                Console.Write("Enter your SSN: ");

                if (!int.TryParse(Console.ReadLine(), out int ssn1))
                {
                    Console.WriteLine("Invalid SSN.");
                    return;
                }

                Console.Write("Enter your name: ");
                string name1 = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name1))
                {
                    Console.WriteLine("Name cannot be empty.");
                    return;
                }

                Console.Write("Enter your phone number: ");

                if (!int.TryParse(Console.ReadLine(), out int phoneNumber1))
                {
                    Console.WriteLine("Invalid phone number.");
                    return;
                }

                User user1 = new User(ssn1, name1, phoneNumber1);

                Console.WriteLine($"\nWelcome, {user1.Name}!");

                UserMenu customerMenu1 = new UserMenu(user1, vehicleService);
                customerMenu1.Show();
                break;

            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }
}