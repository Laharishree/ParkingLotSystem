using ParkingLotSystem.Vehicles;
using ParkingLotSystem.ParkingLotSystem;
public interface IParkVehicle
{
    public string ParkVehicle(IVehicle vehicle);
    public string UnparkVehicle(string licensePlate);
    public static ParkingFeeCalculator GetFeeCalculator(IVehicle vehicle)
    {
        return vehicle.GetVehicleType() switch
        {
            VehicleType.Motorcycle => new MotorcycleFeeCalculator(),
            VehicleType.Car => new CarFeeCalculator(),
            VehicleType.Bus => new BusFeeCalculator(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
public class ParkingVehicle : IParkVehicle
{
    private static ParkingLot parkingLot = ParkingLot.CreateParkingLot();
    private ParkingFeeCalculator? feeCalculator;
    private IFindSpotStrategy findSpotStrategy = new FindSpotStrategy();
    public string ParkVehicle(IVehicle vehicle)
    {
        var spot = findSpotStrategy.FindSpot(parkingLot.parkingLevels, vehicle);
        if (spot != null)
        {
            spot.IsOccupied = true;
            spot.ParkedVehicle = vehicle;
            spot.ParkedVehicle.ParkingDuration.StartTime = DateTime.Now;
            return $"Vehicle with license plate {vehicle.LicensePlate} parked at spot {spot.SpotNumber} on level {spot.LevelNumber}.";
        }
        else
        {
            return "No available parking spot for this vehicle.";
        }
    }
    public string UnparkVehicle(string licensePlate)
    {
        foreach (var level in parkingLot.parkingLevels)
        {
            var spot = level.ParkingSpots.FirstOrDefault(s => s.IsOccupied && s.ParkedVehicle?.LicensePlate == licensePlate);
            if (spot != null && spot.ParkedVehicle != null)
            {
                spot.IsOccupied = false;
                spot.ParkedVehicle.ParkingDuration.EndTime = DateTime.Now;
                feeCalculator = IParkVehicle.GetFeeCalculator(spot.ParkedVehicle);
                var fee = feeCalculator.CalculateFee(spot.ParkedVehicle);
                spot.ParkedVehicle = null;

                return $"Vehicle with license plate {licensePlate} has been unparked from spot {spot.SpotNumber} on level {spot.LevelNumber} With parking fee calculated: ${fee}.";
            }
        }
        return $"Vehicle with license plate {licensePlate} not found in the parking lot.";
    }
    private static List<VehicleType> GetAllowedSpotTypes(IVehicle vehicle)
    {
        return vehicle.GetVehicleType() switch
        {
            VehicleType.Motorcycle => [VehicleType.Motorcycle, VehicleType.Car],

            VehicleType.Car => [VehicleType.Car, VehicleType.Bus],

            VehicleType.Bus => [VehicleType.Bus],

            _ => []
        };
    }
}
