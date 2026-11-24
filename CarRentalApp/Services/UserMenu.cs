namespace CarRentalApp;

public class UserMenu
{
    private readonly User _user;
    private readonly IVehicleService _vehicleService;

    public UserMenu(User user, IVehicleService vehicleService)
    {
        _user = user;
        _vehicleService = vehicleService;
    }

    public void Show()
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
                    _user.RentVehicle(_vehicleService);
                    break;

                case 3:
                    _user.PriceEnquire(_vehicleService);
                    break;

                case 4:
                    _user.displayRentedVehicles();
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
}
