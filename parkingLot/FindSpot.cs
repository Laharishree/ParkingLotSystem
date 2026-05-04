using ParkingLotSystem.Vehicles;
using ParkingLotSystem.ParkingLotSystem;
public interface IFindSpotStrategy
{
    ParkingSpot? FindSpot(List<ParkingLevel> parkingLevels, IVehicle vehicle);

}

public class FindSpotStrategy : IFindSpotStrategy
{
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
    private List<VehicleType> GetAllowedSpotTypes(IVehicle vehicle)
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