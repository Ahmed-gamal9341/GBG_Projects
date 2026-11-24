namespace CarRentalApp;

class VehicleManager : IVehicleService
{
    private List<Vehicle> vehicles = new List<Vehicle>();

    private List<string> RentedVehicles = new List<string>();


    public void AddVehicle(Vehicle vehicle)
    {
        if (vehicle == null)
        {
            throw new ArgumentNullException(nameof(vehicle));
        }

        if (FindVehicle(vehicle.ID.ToString()) != null)
        {
            throw new InvalidOperationException("Vehicle already exists.");
        }

        vehicles.Add(vehicle);
        Console.WriteLine("Vehicle added successfully!");
    }

    public bool RemoveVehicle(string id)
    {
        Vehicle vehicle = FindVehicle(id);

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

    public bool UpdateStatus(string id, VehicleStatus newStatus)
    {
        Vehicle vehicle = FindVehicle(id);

        if (vehicle == null)
        {
            Console.WriteLine("Vehicle not found.");
            return false;
        }

        vehicle.ChangeStatus(newStatus);

        Console.WriteLine("Vehicle status updated successfully!");
        return true;
    }
}