using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.FindVehicle
{
    public class FindVehicleService : IFindVehicle
    {
        private readonly IAmRallyRepository _rellyRepo;

        public FindVehicleService(IAmRallyRepository rellyRepo)
        {
            _rellyRepo = rellyRepo;
        }

        public Result<IEnumerable<Vehicle>> FindVehicle(string rallyId, Query query)
        {
            return _rellyRepo.Find(rallyId)
                .OnSuccess(rally =>
                {
                    var result = rally.Vehicles.Values.ToDto().Where(vehicle => Satisfies(vehicle, query)).ToList();
                    result.Sort(new SortingComparer(query));
                    return Result.Ok<IEnumerable<Vehicle>>(result);
                })
                .OnFailureCompensate(failure => Result.Ok(Enumerable.Empty<Vehicle>()));
        }

        private bool Satisfies(Vehicle vehicle, Query query)
        {
            return (query.TeamName?.Equals(vehicle.TeamName) ?? true)
                   && (query.Model?.Equals(vehicle.Model) ?? true)
                   && (query.ManufacturingDateFrom == null || query.ManufacturingDateFrom <= vehicle.ManufacturingDate)
                   && (query.ManufacturingDateTo == null || query.ManufacturingDateTo >= vehicle.ManufacturingDate)
                   && (query.DistanceFrom == null || query.DistanceFrom <= vehicle.DistanceFromStart)
                   && (query.DistanceTo == null || query.DistanceTo >= vehicle.DistanceFromStart)
                   && (query.Status == null || query.Status == vehicle.Status);
        }

        private class SortingComparer : IComparer<Vehicle>
        {
            private readonly Query _query;

            public SortingComparer(Query query)
            {
                this._query = query;
            }

            public int Compare(Vehicle x, Vehicle y)
            {
                switch (_query.SortBy)
                {
                    case SortBy.TeamName:
                    case null:
                        return x.TeamName.CompareTo(y.TeamName);
                    case SortBy.Model:
                        return x.Model.CompareTo(y.Model);
                    case SortBy.ManufacturingDate:
                        return x.ManufacturingDate.CompareTo(y.ManufacturingDate);
                    case SortBy.Distance:
                        return x.DistanceFromStart.CompareTo(y.DistanceFromStart);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
