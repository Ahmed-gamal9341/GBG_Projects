using CarRentalApp.Services;
using CarRentalApp.Application;

namespace CarRentalApp;

class Program
{
    static void Main(string[] args)
    {
        // Create dependencies
        IVehicleManager manager = new VehicleManager();
        VehicleFactory factory = new VehicleFactory();

        // Create application
        CarRentalApplication app =
            new CarRentalApplication(manager, factory);

        // Start application
        app.Run();
    }
}