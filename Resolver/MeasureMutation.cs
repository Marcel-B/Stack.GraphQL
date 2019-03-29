using System;
using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;
using com.b_velop.stack.GraphQl.InputTypes;
using com.b_velop.stack.GraphQl.Types;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Resolver
{
    public class MeasureMutation : ObjectGraphType
    {
        public MeasureMutation(
            IDataStore<BatteryState> batteryStateRepository,
            IDataStore<ActiveMeasurePoint> activeMeasurePointRepository,
            IDataStore<MeasurePoint> measurePointRepository,
            IDataStore<PriorityState> priorityStateRepostiory,
            IDataStore<Location> locationRepository,
            IDataStore<Unit> unitRepository,
            IDataStore<MeasureValue> measureValueRepository,
            IDataStore<Link> linkRepository)
        {
            Name = "Mutation";

            #region Update
            FieldAsync<BatteryStateType>(
                "updateBatteryState",
                "Update the state of the battery.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The unique identifier of the battery." },
                    new QueryArgument<NonNullGraphType<BatteryStateInputType>> { Name = "batteryStateType" }
                    ),
                    async context =>
                    {
                        var id = context.GetArgument<Guid>("id");
                        var state = context.GetArgument<BatteryState>("batteryStateType");
                        return await batteryStateRepository.UpdateAsync(id, state);
                    });

            FieldAsync<PriorityStateType>(
                "updatePriorityState",
                "Update the value of the Priority.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The unique identifier of the battery." },
                    new QueryArgument<NonNullGraphType<PriorityStateInputType>> { Name = "priorityStateType" }
                    ),
                    async context =>
                    {
                        var id = context.GetArgument<Guid>("id");
                        var state = context.GetArgument<PriorityState>("priorityStateType");
                        return await priorityStateRepostiory.UpdateAsync(id, state);
                    });

            FieldAsync<UnitType>(
                "updateUnit",
                "Update Unit by id",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<UnitInputType>> { Name = "unitType" }
                    ),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var unit = context.GetArgument<Unit>("unitType");
                    return await unitRepository.UpdateAsync(id, unit);
                });

            FieldAsync<MeasurePointType>(
                "updateMeasurePoint",
                "Update MeasurePoint by id.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<MeasurePointInputType>> { Name = "measurePointType" }),
                async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var measurePoint = context.GetArgument<MeasurePoint>("measurePointType");
                    return await measurePointRepository.UpdateAsync(id, measurePoint);
                });

            FieldAsync<LinkType>(
                "updateLink",
                "Update an existing Link",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id"},
                    new QueryArgument<NonNullGraphType<LinkInputType>> { Name = "linkType" }
                    ),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var link = context.GetArgument<Link>("linkType");

                    return await linkRepository.UpdateAsync(id, link);
                });
            #endregion

            #region Create
            FieldAsync<MeasurePointType>(
                "createMeasurePoint",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MeasurePointInputType>> { Name = "measurePointType" }
                ),
                resolve: async context =>
                {
                    var measurePoint = context.GetArgument<MeasurePoint>("measurePointType");

                    measurePoint.Id = Guid.NewGuid();
                    measurePoint.Created = DateTimeOffset.Now;

                    return await measurePointRepository.SaveAsync(measurePoint);
                });

            FieldAsync<MeasureValueType>(
                "createMeasureValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MeasureValueInputType>> { Name = "measureValueType" }
                    ),
                resolve: async context =>
                {
                    var measureValue = context.GetArgument<MeasureValue>("measureValueType");

                    measureValue.Id = Guid.NewGuid();
                    measureValue.Timestamp = DateTimeOffset.Now;

                    return await measureValueRepository.SaveAsync(measureValue);
                });

            FieldAsync<UnitType>(
                "createUnit",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UnitInputType>> { Name = "unitType" }
                    ),
                resolve: async context =>
                {
                    var unit = context.GetArgument<Unit>("unitType");

                    unit.Id = Guid.NewGuid();
                    unit.Created = DateTimeOffset.Now;

                    return await unitRepository.SaveAsync(unit);
                });

            FieldAsync<LocationType>(
                "createLocation",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LocationInputType>> { Name = "locationType" }
                    ),
                resolve: async context =>
                {
                    var location = context.GetArgument<Location>("locationType");

                    location.Id = Guid.NewGuid();
                    location.Created = DateTimeOffset.Now;

                    return await locationRepository.SaveAsync(location);
                });

            FieldAsync<LinkType>(
                "createLink",
                "Create a new Link",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LinkInputType>> { Name = "linkType" }
                    ),
                resolve: async context =>
                {
                    var link = context.GetArgument<Link>("linkType");

                    link.Id = Guid.NewGuid();

                    return await linkRepository.SaveAsync(link);
                });
            #endregion
        }
    }
}
