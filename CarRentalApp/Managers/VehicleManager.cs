namespace CarRentalApp;

class VehicleManager : IVehicleService
{
    private List<Vehicle> vehicles = new List<Vehicle>();

    private List<string> RentedVehicles = new List<string>();


    public void AddVehicle()
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
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Vehicle name cannot be empty.");
            return;
        }

        Console.Write("Enter price per day: ");

        if (!double.TryParse(Console.ReadLine(), out double price) || price <= 0)
        {
            Console.WriteLine("Please enter a valid positive price.");
            return;
        }

        Vehicle vehicle;

        if (type == 1)
        {
            Console.Write("Enter number of doors: ");

            if (!int.TryParse(Console.ReadLine(), out int doors) || doors <= 0)
            {
                Console.WriteLine("Invalid number of doors.");
                return;
            }

            vehicle = new Sedan(name, price, doors);
        }
        else
        {
            Console.Write("Does it have four-wheel drive? (y/n): ");
            string answer = (Console.ReadLine());

            if (answer != "y" && answer != "n")
            {
                Console.WriteLine("Invalid answer.");
                return;
            }

            vehicle = new SUV(name, price, answer == "y");
        }

        if (vehicles.Any(existingVehicle => existingVehicle.ID == vehicle.ID))
        {
            throw new InvalidOperationException("Vehicle already exists.");
        }

        vehicles.Add(vehicle);
        Console.WriteLine("Vehicle added successfully!");
    }

    public bool RemoveVehicle()
    {
        Console.Write("Enter vehicle ID to remove: ");
        string id = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(id))
        {
            Console.WriteLine("Vehicle not found.");
            return false;
        }

        Vehicle vehicle = vehicles.FirstOrDefault(candidate =>
            candidate.ID.ToString().Equals(id) || candidate.Name.Contains(id));

        if (vehicle == null)
        {
            Console.WriteLine("Vehicle not found.");
            return false;
        }

        vehicles.Remove(vehicle);
        Console.WriteLine("Vehicle removed successfully!");
        return true;
    }

    public Vehicle FindVehicle(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return null;
        }

        foreach (Vehicle vehicle in vehicles)
        {
            if (vehicle.ID.ToString().Equals(search)
                || vehicle.Name.Contains(search))
            {
                return vehicle;
            }
        }

        return null;
    }


    public void DisplayVehicles()
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles available.");
            return;
        }

        foreach (Vehicle vehicle in vehicles)
        {
            if (Enum.GetValues<VehicleStatus>().Contains(VehicleStatus.Rented))
            {
                RentedVehicles.Add(vehicle.ID.ToString());
            }
            vehicle.DisplayInfo();
        }
    }

    public bool UpdateStatus()
    {
        Console.Write("Enter vehicle ID: ");
        string id = Console.ReadLine();
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
            return false;
        }

        VehicleStatus newStatus = statuses[choice - 1];

        if (string.IsNullOrWhiteSpace(id))
        {
            Console.WriteLine("Vehicle not found.");
            return false;
        }

        Vehicle vehicle = vehicles.FirstOrDefault(candidate =>
            candidate.ID.ToString().Equals(id) || candidate.Name.Contains(id));

        if (vehicle == null)
        {
            Console.WriteLine("Vehicle not found.");
            return false;
        }

        vehicle.ChangeStatus(newStatus);

        Console.WriteLine("Vehicle status updated successfully!");
        return true;
    }

    public int countVehicles()
    {
        return vehicles.Count;
    }
}