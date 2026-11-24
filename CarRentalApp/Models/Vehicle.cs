
namespace CarRentalApp;

public abstract class Vehicle
{
    private static int autoIncrementID = 1;
    public int _id;
    private string _name;
    private VehicleStatus _status;

    public int ID
    {
        get { return _id; }
    }
    public string Name
    {

        get { return _name; }
    }
    public VehicleStatus Status
    {
        get { return _status; }
    }

    protected Vehicle(string name)
    {
        _id = autoIncrementID++;
        _name = name;
        _status = VehicleStatus.Available;
    }

    public void ChangeStatus(VehicleStatus status)
    {
        _status = status;
    }

    public abstract double CalculateRentalCost(int days);

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"ID: {ID}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Status: {Status}");
    }
}
