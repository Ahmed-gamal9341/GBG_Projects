namespace CarRentalApp;

class Program
{
    static void Main(string[] args)
    {
        IVehicleService vehicleService = new VehicleManager();

        UserMenu customerMenu = new UserMenu(vehicleService);
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
                string name = Console.ReadLine() ?? "";

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

                Console.WriteLine($"\nWelcome Customer, {user.Name}!");

                customerMenu.Show(user);
                break;

            case 2:
                Console.WriteLine("\nWelcome Manager!");

                managerMenu.Show();
                break;

            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }
}