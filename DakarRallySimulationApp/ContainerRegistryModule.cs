using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using DakarRallySimulation.App.AddVehicleToRally;
using DakarRallySimulation.App.CreateRally;
using DakarRallySimulation.App.FindVehicle;
using DakarRallySimulation.App.GetLeaderboard;
using DakarRallySimulation.App.GetRallyStatus;
using DakarRallySimulation.App.GetVehicleStatistics;
using DakarRallySimulation.App.RemoveVehicleFromRally;
using DakarRallySimulation.App.StartRally;
using DakarRallySimulation.Domain.Vehicle;
using DakarRallySimulation.Domain.VehicleHealthStatus;

namespace DakarRallySimulation.App
{
    public class ContainerRegistryModule : Module
    {
        public int SportCarMaxSpeed { get; set; } = 140;
        public int TerrainCarMaxSpeed { get; set; } = 100;
        public int TruckMaxSpeed { get; set; } = 80;
        public int SportMotorcycleMaxSpeed { get; set; } = 130;
        public int CrossMotorcycleMaxSpeed { get; set; } = 85;
        public TimeSpan CarRepairmentDuration { get; set; } = TimeSpan.FromHours(5);
        public TimeSpan TruckRepairmentDuration { get; set; } = TimeSpan.FromHours(7);
        public TimeSpan MotorcycleRepairmentDuration { get; set; } = TimeSpan.FromHours(7);
        public int SimulationResolutionTimeInSeconds { get; set; } = 5;



        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DakarRallySimulationApp>().As<ISimulateDakarRally>().SingleInstance();
            builder.RegisterType<CreateRallyService>().As<ICreateRally>().SingleInstance();
            builder.RegisterType<AddVehicleService>().As<IAddVehicleToRally>().SingleInstance();
            builder.RegisterType<RemoveVehicleService>().As<IRemoveVehicleFromRally>().SingleInstance();
            builder.RegisterType<StartRallyService>().As<IStartRally>().SingleInstance();
            //builder.RegisterType<DakarRallySimulationApp>().As<IProvideLeaderboard>().SingleInstance();
            builder.RegisterType<VehicleStatisticsService>().As<IProvideVehicleStatistics>().SingleInstance();
            //builder.RegisterType<DakarRallySimulationApp>().As<IFindVehicle>().SingleInstance();
            builder.RegisterType<RallyRepository>().As<IAmRallyRepository>().SingleInstance();
            builder.RegisterType<GetRallyStatusInfoService>().As<IProvideRallyStatusInfo>().SingleInstance();
            builder.RegisterType<RallyStatusInfoFactory>().As<ICreateRallyStatusInfo>().SingleInstance();
            builder.RegisterType<VehicleStatisticsFactory>().As<ICreateVehicleStatistics>().SingleInstance();

            var vehicleFactory = new VehicleFactory(
                SportCarMaxSpeed,
                TerrainCarMaxSpeed,
                TruckMaxSpeed,
                SportMotorcycleMaxSpeed,
                CrossMotorcycleMaxSpeed,
                CarRepairmentDuration,
                TruckRepairmentDuration,
                MotorcycleRepairmentDuration,
                null,
                SimulationResolutionTimeInSeconds
                );

            builder.RegisterInstance(vehicleFactory).As<ICreateVehicle>().SingleInstance();
            builder.RegisterType<HealthStatusProvider>().As<IProvideHealthStatus>().SingleInstance();
            builder.RegisterType<HealthStatusProviderFactory>().As<ICreateHealthStatusProvider>().SingleInstance();
        }
    }
}
