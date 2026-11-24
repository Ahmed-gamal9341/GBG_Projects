
namespace CarRentalApp;

public interface IVehicleService
{
    void AddVehicle();
    void DisplayVehicles();
    Vehicle FindVehicle(string search);
    bool UpdateStatus();
    bool RemoveVehicle();
    int countVehicles();
}
