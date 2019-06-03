using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.AddVehicleToRally
{
    public interface IAddVehicleToRally
    {
        Result AddVehicle(string rallId, Vehicle vehicle);
    }
}