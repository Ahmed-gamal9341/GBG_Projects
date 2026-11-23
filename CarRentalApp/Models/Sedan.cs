namespace CarRentalApp;

class Sedan : Vehicle
{
    private double _pricePerDay;
    private int _numberOfDoors;

    public Sedan(string name, double price, int doors)
        : base(name)
    {
        _pricePerDay = price;
        _numberOfDoors = doors;
    }

    public override double CalculateRentalCost(int days)
    {
        return _pricePerDay * days;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Type: Sedan");
        Console.WriteLine($"Price per day: {_pricePerDay}");
        Console.WriteLine($"Number of doors: {_numberOfDoors}");
    }
}
