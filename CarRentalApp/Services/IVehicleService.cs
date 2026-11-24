
namespace CarRentalApp;

public interface IVehicleService
{
    void AddVehicle(Vehicle vehicle);
    void DisplayVehicles();
    Vehicle FindVehicle(string search);
    bool UpdateStatus(string id, VehicleStatus newStatus);
    bool RemoveVehicle(string id);
}
