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
                    AddVehicle();
                    break;

                case 2:
                    _vehicleService.DisplayVehicles();
                    break;

                case 3:
                    SearchVehicle();
                    break;

                case 4:
                    UpdateVehicleStatus();
                    break;

                case 5:
                    RemoveVehicle();
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

    private void AddVehicle()
    {
        Console.WriteLine("\n1. Sedan");
        Console.WriteLine("2. SUV");

        Console.Write("Choose vehicle type: ");

        if (!int.TryParse(Console.ReadLine(), out int type) ||
            (type != 1 && type != 2))
        {
            Console.WriteLine("Invalid vehicle type!");
            return;
        }

        Console.Write("Enter vehicle name: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Vehicle name cannot be empty.");
            return;
        }

        Console.Write("Enter price per day: ");

        if (!double.TryParse(Console.ReadLine(), out double price) ||
            price <= 0)
        {
            Console.WriteLine("Please enter a valid positive price.");
            return;
        }

        Vehicle vehicle;

        if (type == 1)
        {
            Console.Write("Enter number of doors: ");

            if (!int.TryParse(Console.ReadLine(), out int doors) ||
                doors <= 0)
            {
                Console.WriteLine("Invalid number of doors.");
                return;
            }

            vehicle = new Sedan(name, price, doors);
        }
        else
        {
            Console.Write("Does it have four-wheel drive? (y/n): ");
            string answer = (Console.ReadLine() ?? "").Trim().ToLower();

            if (answer != "y" && answer != "n")
            {
                Console.WriteLine("Invalid answer.");
                return;
            }

            bool fourWheelDrive = answer == "y";

            vehicle = new SUV(name, price, fourWheelDrive);
        }

        _vehicleService.AddVehicle(vehicle);

        Console.WriteLine("Vehicle added successfully!");
    }

    private void SearchVehicle()
    {
        Console.Write("Enter vehicle ID or name: ");
        string search = Console.ReadLine() ?? "";

        Vehicle? vehicle = _vehicleService.FindVehicle(search);

        if (vehicle == null)
        {
            Console.WriteLine("Vehicle not found!");
            return;
        }

        vehicle.DisplayInfo();
    }

    private void UpdateVehicleStatus()
    {
        Console.Write("Enter vehicle ID: ");
        string id = Console.ReadLine() ?? "";

        VehicleStatus[] statuses = Enum.GetValues<VehicleStatus>();

        Console.WriteLine("\nAvailable statuses:");

        for (int i = 0; i < statuses.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {statuses[i]}");
        }

        Console.Write("Choose new status: ");

        if (!int.TryParse(Console.ReadLine(), out int choice) ||
            choice < 1 || choice > statuses.Length)
        {
            Console.WriteLine("Invalid status choice!");
            return;
        }

        VehicleStatus newStatus = statuses[choice - 1];

        bool updated = _vehicleService.UpdateStatus(id, newStatus);

        Console.WriteLine(updated
            ? "Vehicle status updated successfully!"
            : "Vehicle not found!");
    }

    private void RemoveVehicle()
    {
        Console.Write("Enter vehicle ID to remove: ");
        string id = Console.ReadLine() ?? "";

        bool removed = _vehicleService.RemoveVehicle(id);

        Console.WriteLine(removed
            ? "Vehicle removed successfully!"
            : "Vehicle not found!");
    }
}