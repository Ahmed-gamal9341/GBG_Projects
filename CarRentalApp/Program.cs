
namespace CarRentalApp;

class Program
{
    static void Main(string[] args)
    {
        VehicleManager manager = new VehicleManager();

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

            string input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid input! Please enter a number.");
                continue;
            }
            do
            {
                switch (choice)
                {
                    case 1:
                        {

                            Console.WriteLine("\n--- ADD VEHICLE ---");
                            Console.WriteLine("1. Sedan");
                            Console.WriteLine("2. SUV");

                            Console.Write("Choose vehicle type: ");

                            if (!int.TryParse(Console.ReadLine(), out int type)
                                || (type != 1 && type != 2))
                            {
                                Console.WriteLine("Invalid vehicle type.");
                                continue;
                            }

                            Console.Write("Enter vehicle name: ");
                            string name = Console.ReadLine();

                            if (name.Equals(""))
                            {
                                Console.WriteLine("Vehicle name cannot be empty.");
                                break;
                            }

                            Console.Write("Enter price per day: ");

                            if (!double.TryParse(Console.ReadLine(), out double price)
                                || price <= 0)
                            {
                                Console.WriteLine("Invalid price.");
                                break;
                            }

                            Vehicle vehicle;

                            if (type == 1)
                            {
                                Console.Write("Enter number of doors: ");

                                if (!int.TryParse(Console.ReadLine(), out int doors)
                                    || doors < 2)
                                {
                                    Console.WriteLine("Invalid number of doors.");
                                    break;
                                }

                                vehicle = new Sedan(name, price, doors);
                            }
                            else
                            {
                                Console.Write("Does it have 4-wheel drive? (y/n): ");
                                string answer = Console.ReadLine();

                                if (answer != "y" && answer != "n" || answer == null)
                                {
                                    Console.WriteLine("Invalid answer.");
                                    break;
                                }

                                bool fourWheelDrive = answer == "y";

                                vehicle = new SUV(name, price, fourWheelDrive);
                            }

                            manager.AddVehicle(vehicle);

                            Console.WriteLine($"Vehicle ID: {vehicle.ID}");
                            break;
                        }

                    case 2:
                        {
                            manager.DisplayVehicles();
                            break;
                        }

                    case 3:
                        {
                            Console.Write("\nEnter vehicle name or ID: ");
                            string search = Console.ReadLine();

                            Vehicle vehicle = manager.FindVehicle(search);

                            if (vehicle == null)
                                Console.WriteLine("Vehicle not found.");
                            else
                                vehicle.DisplayInfo();

                            break;
                        }

                    case 4:
                        {
                            Console.Write("\nEnter vehicle ID: ");
                            string id = Console.ReadLine();

                            Console.WriteLine("Available statuses:");

                            foreach (VehicleStatus status in Enum.GetValues(typeof(VehicleStatus)))
                            {
                                Console.WriteLine($"{(int)status}. {status}");
                            }

                            Console.Write("Choose new status: ");

                            if (!int.TryParse(Console.ReadLine(), out int statusChoice)
                                || !Enum.IsDefined(typeof(VehicleStatus), statusChoice))
                            {
                                Console.WriteLine("Invalid status.");
                                break;
                            }

                            VehicleStatus newStatus = (VehicleStatus)statusChoice;

                            manager.UpdateStatus(id, newStatus);
                            break;
                        }

                    case 5:
                        {
                            Console.Write("\nEnter vehicle ID: ");
                            string id = Console.ReadLine() ?? "";

                            manager.RemoveVehicle(id);
                            break;
                        }

                    case 6:
                        {
                            running = false;
                            Console.WriteLine("Thank you for using the system!");
                            break;
                        }

                    default:
                        Console.WriteLine("Invalid option! Choose between 1 and 6.");
                        break;
                }
            } while (choice < 1 || choice > 6);


        }
    }
}
