using ParkingLotSystem.Vehicles;

namespace ParkingLotSystem.ParkingLotSystem;

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
    public string GetParkingLotStatus()
    {
        var status = new System.Text.StringBuilder();
        foreach (var level in parkingLevels)
        {
            status.AppendLine($"Level {level.LevelNumber}:");
            foreach (var spot in level.ParkingSpots)
            {
                var occupancy = spot.IsOccupied ? $"Occupied by {spot.ParkedVehicle?.LicensePlate}" : "Available";
                status.AppendLine($"  Spot {spot.SpotNumber} ({spot.SpotType}): {occupancy}");
            }
        }
        return status.ToString();
    }
    public void AddLevel(int LevelNumber, int numberOfMotorCycle, int numberOfCar, int numberOfBus)
    {
        var level = new ParkingLevel
        {
            LevelNumber = LevelNumber,
            ParkingSpots = new List<ParkingSpot>()
        };
        parkingLevels.Add(level);
        AddSlot(LevelNumber, numberOfMotorCycle, VehicleType.Motorcycle);
        AddSlot(LevelNumber, numberOfCar, VehicleType.Car);
        AddSlot(LevelNumber, numberOfBus, VehicleType.Bus);
    }
    public void AddSlot(int LevelNumber, int numberofSlot, VehicleType vehicleType)
    {
        var level = parkingLevels.FirstOrDefault(l => l.LevelNumber == LevelNumber);
        int spotCounter = level?.ParkingSpots.Count(s => s.SpotType == vehicleType) ?? 0;
        if (level != null)
        {
            for (int i = 0; i < numberofSlot; i++)
            {
                level.ParkingSpots.Add(new ParkingSpot
                {
                    IsOccupied = false,
                    SpotType = vehicleType,
                    SpotNumber = spotCounter + i + 1,
                    LevelNumber = LevelNumber
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
    public required List<ParkingSpot> ParkingSpots { get; set; }

}
public class ParkingSpot
{
    public required int LevelNumber { get; set; }
    public required bool IsOccupied { get; set; }
    public IVehicle? ParkedVehicle { get; set; }
    public required int SpotNumber { get; set; }
    public required VehicleType SpotType { get; set; }
}
