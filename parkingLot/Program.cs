using ParkingLotSystem.Vehicles;

var parkingManager = new ParkingLotManager();
parkingManager.AddLevel(1, 1, 1, 1);



// Park some vehicles

var motorcycle = VehicleFactory.CreateVehicle(VehicleType.Motorcycle, "MOTO123", new Duration ());
var car = VehicleFactory.CreateVehicle(VehicleType.Car, "CAR456", new Duration ());
var bus = VehicleFactory.CreateVehicle(VehicleType.Bus, "BUS789", new Duration ());

Console.WriteLine(parkingManager.ParkVehicle(motorcycle));
Console.WriteLine(parkingManager.ParkVehicle(car));
Console.WriteLine(parkingManager.ParkVehicle(bus));
// Unpark a vehicle
Console.WriteLine(parkingManager.UnparkVehicle("CAR456"));
// Get parking lot status
Console.WriteLine(parkingManager.GetParkingLotStatus());
// Add more levels and slots
parkingManager.AddLevel(3, 1, 2, 1);
// Remove some slots
parkingManager.RemoveSlot(1, 1, VehicleType.Car);
Console.WriteLine(parkingManager.GetParkingLotStatus());
// Try to park another car
var anotherCar = VehicleFactory.CreateVehicle(VehicleType.Car, "CAR999", new Duration ());
Console.WriteLine(parkingManager.ParkVehicle(anotherCar));
//Exhaust all slots for motorcycles
var anotherMotorcycle = VehicleFactory.CreateVehicle(VehicleType.Motorcycle, "MOTO999", new Duration ());
Console.WriteLine(parkingManager.ParkVehicle(anotherMotorcycle));
var yetAnotherMotorcycle = VehicleFactory.CreateVehicle(VehicleType.Motorcycle, "MOTO888", new Duration ());
Console.WriteLine(parkingManager.ParkVehicle(yetAnotherMotorcycle));

// Unpark a vehicle that doesn't exist
Console.WriteLine(parkingManager.UnparkVehicle("CAR000"));



