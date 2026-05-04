using ParkingLot.Vehicles;

namespace ParkingLot;

public class ParkingLot
{
    private static ParkingLot? _instance;
    private static object lockObj = new object();
    public List<ParkingLevel> parkingLevels;
    private ParkingLot()
    {
        parkingLevels = new List<ParkingLevel>();
    }
    public static ParkingLot CreateParkingLot()
    {
        if (_instance == null)
        {
            lock (lockObj)
            {
                if (_instance == null)
                {
                    _instance = new ParkingLot();
                }
            }
        }

        return _instance;
    }

    public void AddLevel(int LevelNumber, int numberOfMotorCycle, int numberOfCar, int numberOfBus)
    {
        var level = new ParkingLevel
        {
            LevelNumber = LevelNumber,
            ParkingSpots = new List<ParkingSpot>()
        };
        AddSlot(LevelNumber, numberOfMotorCycle, VehicleType.Motorcycle);
        AddSlot(LevelNumber, numberOfCar, VehicleType.Car);
        AddSlot(LevelNumber, numberOfBus, VehicleType.Bus);
        parkingLevels.Add(level);
    }
    public void AddSlot(int LevelNumber, int numberofSlot, VehicleType vehicleType)
    {
        var level = parkingLevels.FirstOrDefault(l => l.LevelNumber == LevelNumber);
        if (level != null)
        {
            for (int i = 0; i < numberofSlot; i++)
            {
                level.ParkingSpots.Add(new ParkingSpot
                {
                    IsOccupied = false,
                    SpotType = vehicleType,
                    SpotNumber = i
                });
            }
        }
    }
    public void RemoveSlot(int LevelNumber, int numberofSlot, VehicleType vehicleType)
    {
        var level = parkingLevels.FirstOrDefault(l => l.LevelNumber == LevelNumber);
        if (level != null)
        {
            var spotsToRemove = level.ParkingSpots.Where(s => s.SpotType == vehicleType && !s.IsOccupied).Take(numberofSlot).ToList();
            foreach (var spot in spotsToRemove)
            {
                level.ParkingSpots.Remove(spot);
            }
        }
    }

}
public class ParkingLevel
{
    public int LevelNumber { get; set; }
    public List<ParkingSpot> ParkingSpots { get; set; }

}
public class ParkingSpot
{
    public bool IsOccupied { get; set; }
    public IVehicle? ParkedVehicle { get; set; }
    public int SpotNumber { get; set; }
    public VehicleType SpotType { get; set; }
}
//Find a parking spot strategy interface
public interface IFindSpotStrategy
{
    ParkingSpot? FindSpot(List<ParkingLevel> parkingLevels, IVehicle vehicle);

}
public interface IParkVehicle
{
    public string ParkVehicle(IVehicle vehicle);
    public List<VehicleType> GetAllowedSpotTypes(IVehicle vehicle);
    public string UnparkVehicle(string licensePlate);
    public IParkingFeeCalculator GetFeeCalculator(IVehicle vehicle);
}
public class FindSpotStrategy : IFindSpotStrategy
{
    public static ParkingLot parkingLot = ParkingLot.CreateParkingLot();

    public ParkingSpot? FindSpot(List<ParkingLevel> levels, IVehicle vehicle)
    {
        var allowedTypes = GetAllowedSpotTypes(vehicle);

        foreach (var type in allowedTypes)
        {
            foreach (var level in levels)
            {
                var spot = level.ParkingSpots
                    .FirstOrDefault(s => !s.IsOccupied && s.SpotType == type);

                if (spot != null)
                    return spot;
            }
        }

        return null;
    }

}
public class ParkVehicle : IParkVehicle
{
    private static ParkingLot parkingLot = ParkingLot.CreateParkingLot();
    private readonly IParkingFeeCalculator? feeCalculator;
    private readonly IFindSpotStrategy? findSpotStrategy;
    public string ParkVehicle(IVehicle vehicle)
    {
        var spot = findSpotStrategy.FindSpot(parkingLot.parkingLevels, vehicle);
        if (spot != null)
        {
            spot.IsOccupied = true;
            spot.ParkedVehicle = vehicle;
            spot.ParkedVehicle.ParkingDuration.StartTime = DateTime.Now;
            return $"Vehicle with license plate {vehicle.LicensePlate} parked at spot {spot.SpotNumber} on level {parkingLot.parkingLevels.First(l => l.ParkingSpots.Contains(spot)).LevelNumber}.";
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
                feeCalculator = GetFeeCalculator(spot.ParkedVehicle);
                var fee = feeCalculator.CalculateFee(spot.ParkedVehicle);
                spot.ParkedVehicle = null;

                return $"Vehicle with license plate {licensePlate} has been unparked from spot {spot.SpotNumber} on level {level.LevelNumber} With parking fee calculated: ${fee}.";
            }
        }
        return $"Vehicle with license plate {licensePlate} not found in the parking lot.";
    }
    public List<VehicleType> GetAllowedSpotTypes(IVehicle vehicle)
    {
        return vehicle.GetVehicleType() switch
        {
            VehicleType.Motorcycle => [VehicleType.Motorcycle, VehicleType.Car],

            VehicleType.Car => [VehicleType.Car, VehicleType.Bus],

            VehicleType.Bus => [VehicleType.Bus],

            _ => []
        };
    }
    public static IParkingFeeCalculator GetFeeCalculator(IVehicle vehicle)
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
//Manager
public class ParkingLotManager
{




}