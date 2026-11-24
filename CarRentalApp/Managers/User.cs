namespace CarRentalApp;

public class User
{
    private int _ssn;
    private string _name;
    private int _phoneNumber;
    private List<Vehicle> _rentedVehicles;

    public User(int ssn, string name, int phoneNumber)
    {
        _ssn = ssn;
        _name = name;
        _phoneNumber = phoneNumber;
        _rentedVehicles = new List<Vehicle>();
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int PhoneNumber
    {
        get { return _phoneNumber; }
        set { _phoneNumber = value; }
    }

    public void RentVehicle(IVehicleService vehicleService)
    {
        Console.Write("Enter the vehicle ID or name: ");
        string search = Console.ReadLine();
        Vehicle vehicle = vehicleService.FindVehicle(search);

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

        if (vehicle.Status == VehicleStatus.Available)
        {
            double cost = vehicle.CalculateRentalCost(days);
            Console.WriteLine($"Vehicle rented successfully! Total cost: ${cost}");
            vehicle.ChangeStatus(VehicleStatus.Rented);
            _rentedVehicles.Add(vehicle);
        }
        else
        {
            Console.WriteLine("Vehicle is not available for rent.");
        }
    }

    public void PriceEnquire(IVehicleService vehicleService)
    {
        Console.Write("Enter the vehicle ID or name: ");
        string search = Console.ReadLine() ;
        Vehicle vehicle = vehicleService.FindVehicle(search);

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

        double cost = vehicle.CalculateRentalCost(days);
        Console.WriteLine($"Estimated rental cost for {days} days: ${cost}");
    }

    public void displayRentedVehicles()
    {
        if (_rentedVehicles.Count == 0)
        {
            Console.WriteLine("No vehicles rented.");
            return;
        }

        Console.WriteLine("Rented Vehicles:");
        foreach (Vehicle vehicle in _rentedVehicles)
        {
            vehicle.DisplayInfo();
        }
    }




}