
namespace CarRentalApp;

public class ManagerMenu
{
    private readonly IVehicleManager _manager;
    private readonly VehicleFactory _factory;

    public ManagerMenu(
        IVehicleManager manager,
        VehicleFactory factory)
    {
        _manager = manager;
        _factory = factory;
    }

    public void Show()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== VEHICLE MANAGEMENT SYSTEM =====");
            Console.WriteLine("1. Add a new vehicle");
            Console.WriteLine("2. View all vehicles");
            Console.WriteLine("3. Search by name or ID");
            Console.WriteLine("4. Update vehicle status");
            Console.WriteLine("5. Remove a vehicle");
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
                    _manager.DisplayVehicles();
                    break;

                case 3:
                    SearchVehicle();
                    break;

                case 4:
                    UpdateVehicleStatus();
                    break;

                case 5:
                    Console.Write("Enter vehicle ID: ");
                    string id = Console.ReadLine() ?? "";
                    _manager.RemoveVehicle(id);
                    break;

                case 6:
                    running = false;
                    Console.WriteLine("Thank you for using the system!");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void AddVehicle()
    {
        Console.WriteLine("\n--- ADD VEHICLE ---");
        Console.WriteLine("1. Sedan");
        Console.WriteLine("2. SUV");

        Console.Write("Choose vehicle type: ");

        if (!int.TryParse(Console.ReadLine(), out int type)
            || (type != 1 && type != 2))
        {
            Console.WriteLine("Invalid vehicle type.");
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

        if (!double.TryParse(Console.ReadLine(), out double price)
            || price <= 0)
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        Vehicle vehicle;

        if (type == 1)
        {
            Console.Write("Enter number of doors: ");

            if (!int.TryParse(Console.ReadLine(), out int doors)
                || doors < 2)
            {
                Console.WriteLine("Invalid number of doors.");
                return;
            }

            vehicle = _factory.CreateSedan(name, price, doors);
        }
        else
        {
            Console.Write("Does it have 4-wheel drive? (y/n): ");

            string answer = Console.ReadLine() ?? "";

            if (answer != "y" && answer != "n")
            {
                Console.WriteLine("Invalid answer.");
                return;
            }

            bool fourWheelDrive = answer == "y";

            vehicle = _factory.CreateSUV(
                name, price, fourWheelDrive);
        }

        _manager.AddVehicle(vehicle);

        Console.WriteLine($"Vehicle ID: {vehicle.ID}");
    }

    private void SearchVehicle()
    {
        Console.Write("Enter vehicle name or ID: ");

        string search = Console.ReadLine() ?? "";

        Vehicle vehicle = _manager.FindVehicle(search);

        if (vehicle == null)
            Console.WriteLine("Vehicle not found.");
        else
            vehicle.DisplayInfo();
    }

    private void UpdateVehicleStatus()
    {
        Console.Write("Enter vehicle ID: ");

        string id = Console.ReadLine() ?? "";

        Console.WriteLine("Available statuses:");

        foreach (VehicleStatus status in Enum.GetValues<VehicleStatus>())
        {
            Console.WriteLine($"{(int)status}. {status}");
        }

        Console.Write("Choose new status: ");

        if (!int.TryParse(Console.ReadLine(), out int statusChoice)
            || !Enum.IsDefined(typeof(VehicleStatus), statusChoice))
        {
            Console.WriteLine("Invalid status.");
            return;
        }

        VehicleStatus newStatus = (VehicleStatus)statusChoice;

        _manager.UpdateStatus(id, newStatus);
    }
}
