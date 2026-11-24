
namespace CarRentalApp;

public class UserMenu
{
    private readonly IVehicleManager _manager;

    public UserMenu(IVehicleManager manager)
    {
        _manager = manager;
    }

    public void Show(User user)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== USER MENU =====");
            Console.WriteLine("1. View available vehicles");
            Console.WriteLine("2. Rent a vehicle");
            Console.WriteLine("3. Enquire rental price");
            Console.WriteLine("4. View my rented vehicles");
            Console.WriteLine("5. Exit");

            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    _manager.DisplayVehicles();
                    break;

                case 2:
                case 3:
                    Console.Write("Enter vehicle name or ID: ");
                    string search = Console.ReadLine() ?? "";

                    Vehicle vehicle = _manager.FindVehicle(search);

                    if (vehicle == null)
                    {
                        Console.WriteLine("Vehicle not found.");
                        break;
                    }

                    Console.Write("Enter rental duration in days: ");

                    if (!int.TryParse(Console.ReadLine(), out int days)
                        || days <= 0)
                    {
                        Console.WriteLine("Invalid rental duration.");
                        break;
                    }

                    if (choice == 2)
                        user.RentVehicle(vehicle, days);
                    else
                        user.PriceEnquire(vehicle, days);

                    break;

                case 4:
                    user.displayRentedVehicles();
                    break;

                case 5:
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
