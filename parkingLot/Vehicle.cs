namespace ParkingLotSystem.Vehicles;

public interface IVehicle
{
    public string LicensePlate { get; set; }
    public Duration ParkingDuration { get; set; }
    public VehicleType GetVehicleType();

}
public class Duration
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

public class Car : IVehicle
{
    public required string LicensePlate { get; set; }
    public required Duration ParkingDuration { get; set; }
    public VehicleType GetVehicleType()
    {
        return VehicleType.Car;
    }
}

public class Motorcycle : IVehicle
{
    public required string LicensePlate { get; set; }
    public required Duration ParkingDuration { get; set; }
    public VehicleType GetVehicleType()
    {
        return VehicleType.Motorcycle;
    }
}
public class Bus : IVehicle
{
    public required string LicensePlate { get; set; }
    public required Duration ParkingDuration { get; set; }
    public VehicleType GetVehicleType()
    {
        return VehicleType.Bus;
    }
}

//Factory
public class VehicleFactory
{
    public static IVehicle CreateVehicle(VehicleType type, string licensePlate, Duration duration)
    {
        return type switch
        {
            VehicleType.Car => new Car { LicensePlate = licensePlate, ParkingDuration = duration },
            VehicleType.Motorcycle => new Motorcycle { LicensePlate = licensePlate, ParkingDuration = duration },
            VehicleType.Bus => new Bus { LicensePlate = licensePlate, ParkingDuration = duration },
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
public enum VehicleType
{
    Car,
    Motorcycle,
    Bus
}

public abstract class ParkingFeeCalculator
{
    public abstract int CalculateFee(IVehicle vehicle);
    public int GetTotalHours(IVehicle vehicle)
    {
        var totalHours = (vehicle.ParkingDuration.EndTime - vehicle.ParkingDuration.StartTime).TotalHours;
        return (int)Math.Ceiling(totalHours);
    }
}
public class MotorcycleFeeCalculator : ParkingFeeCalculator
{
    public override int CalculateFee(IVehicle vehicle)
    {
        return 1 * GetTotalHours(vehicle); // Flat fee for motorcycles
    }
}
public class CarFeeCalculator : ParkingFeeCalculator
{
    public override int CalculateFee(IVehicle vehicle)
    {
        return 2 * GetTotalHours(vehicle); // Flat fee for cars
    }
}
public class BusFeeCalculator : ParkingFeeCalculator
{
    public override int CalculateFee(IVehicle vehicle)
    {
        return 5 * GetTotalHours(vehicle); // Flat fee for buses
    }
}