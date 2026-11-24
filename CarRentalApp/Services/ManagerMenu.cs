namespace CarRentalApp;

public class ManagerMenu
{
    private readonly IVehicleService _vehicleService;

    public ManagerMenu(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public void Show()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== MANAGER MENU =====");
            Console.WriteLine("1. Add Vehicle");
            Console.WriteLine("2. Display All Vehicles");
            Console.WriteLine("3. Search for Vehicle");
            Console.WriteLine("4. Update Vehicle Status");
            Console.WriteLine("5. Remove Vehicle");
            Console.WriteLine("6. Exit");

            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    _vehicleService.AddVehicle();
                    break;

                case 2:
                    _vehicleService.DisplayVehicles();
                    break;

                case 3:
                    Console.Write("Enter vehicle ID or name: ");
                    string search = Console.ReadLine() ;
                    Vehicle? vehicle = _vehicleService.FindVehicle(search);

                    if (vehicle == null)
                    {
                        Console.WriteLine("Vehicle not found!");
                        break;
                    }

                    vehicle.DisplayInfo();
                    break;

                case 4:
                    _vehicleService.UpdateStatus();
                    break;

                case 5:
                    _vehicleService.RemoveVehicle();
                    break;

                case 6:
                    running = false;
                    Console.WriteLine("GoodBye");
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }
}