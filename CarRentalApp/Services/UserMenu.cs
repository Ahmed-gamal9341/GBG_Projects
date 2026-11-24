namespace CarRentalApp;

public class UserMenu
{
    private readonly IVehicleService _vehicleService;

    public UserMenu(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public void Show(User user)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== CUSTOMER MENU =====");
            Console.WriteLine("1. Display Available Vehicles");
            Console.WriteLine("2. Rent a Vehicle");
            Console.WriteLine("3. Enquire About Rental Price");
            Console.WriteLine("4. Display My Rented Vehicles");
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
                    _vehicleService.DisplayVehicles();
                    break;

                case 2:
                    RentVehicle(user);
                    break;

                case 3:
                    EnquirePrice(user);
                    break;

                case 4:
                    user.displayRentedVehicles();
                    break;

                case 5:
                    running = false;
                    Console.WriteLine("Exiting customer menu...");
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    private void RentVehicle(User user)
    {
        Console.Write("Enter the vehicle ID or name: ");
        string search = Console.ReadLine();

        Vehicle? vehicle = _vehicleService.FindVehicle(search);

        if (vehicle == null)
        {
            Console.WriteLine("Vehicle not found!");
            return;
        }

        Console.Write("Enter number of rental days: ");

        if (!int.TryParse(Console.ReadLine(), out int days) || days <= 0)
        {
            Console.WriteLine("Please enter a valid number of days.");
            return;
        }

        user.RentVehicle(vehicle, days);
    }

    private void EnquirePrice(User user)
    {
        Console.Write("Enter the vehicle ID or name: ");
        string search = Console.ReadLine() ?? "";

        Vehicle? vehicle = _vehicleService.FindVehicle(search);

        if (vehicle == null)
        {
            Console.WriteLine("Vehicle not found!");
            return;
        }

        Console.Write("Enter number of rental days: ");

        if (!int.TryParse(Console.ReadLine(), out int days) || days <= 0)
        {
            Console.WriteLine("Please enter a valid number of days.");
            return;
        }

        user.PriceEnquire(vehicle, days);
    }
}