namespace CarRentalApp;

class SUV : Vehicle
{
    private double _pricePerDay;
    private bool _fourWheelDrive;

    public SUV(string name, double price, bool fourWheelDrive)
        : base(name)
    {
        _pricePerDay = price;
        _fourWheelDrive = fourWheelDrive;
    }

    public override double CalculateRentalCost(int days)
    {
        return _pricePerDay * days;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Type: SUV");
        Console.WriteLine($"Price per day: {_pricePerDay}");
        Console.WriteLine($"Four-wheel drive: {_fourWheelDrive}");
    }
}
