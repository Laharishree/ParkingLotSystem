using ParkingLotSystem.ParkingLotSystem;
using ParkingLotSystem.Vehicles;

public class ParkingLotManager
{
    public static ParkingLot parkingLot = ParkingLot.CreateParkingLot();
    private static ParkingVehicle parkingVehicle = new ParkingVehicle();

    public string ParkVehicle(IVehicle vehicle)
    {
        return parkingVehicle.ParkVehicle(vehicle);
    }

    public string UnparkVehicle(string licensePlate)
    {
        return parkingVehicle.UnparkVehicle(licensePlate);
    }
    public void AddLevel(int LevelNumber, int numberOfMotorCycle, int numberOfCar, int numberOfBus)
    {
        parkingLot.AddLevel(LevelNumber, numberOfMotorCycle, numberOfCar, numberOfBus);
    }
    public void AddSlot(int LevelNumber, int numberofSlot, VehicleType vehicleType)
    {
        parkingLot.AddSlot(LevelNumber, numberofSlot, vehicleType);
    }
    public void RemoveSlot(int LevelNumber, int numberofSlot, VehicleType vehicleType)
    {
        parkingLot.RemoveSlot(LevelNumber, numberofSlot, vehicleType);
    }

    public string GetParkingLotStatus()
    {
        return parkingLot.GetParkingLotStatus();
    }
}