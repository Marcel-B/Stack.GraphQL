using System;
using System.Collections.Generic;
using System.Linq;
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
            BatteryStateRepository batteryStateRepository,
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

            FieldAsync<ListGraphType<BatteryStateType>>(
            "updateBatteryStateBunch",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<ListGraphType<IdGraphType>>> { Name = "ids" },
                new QueryArgument<NonNullGraphType<ListGraphType<DateTimeOffsetGraphType>>> { Name = "timestamps" },
                new QueryArgument<NonNullGraphType<ListGraphType<BooleanGraphType>>> { Name = "states" }),
            resolve: async context =>
            {
                var time = DateTimeOffset.Now;
                var ids = context.GetArgument<IEnumerable<Guid>>("ids");
                var timestamps = context.GetArgument<IEnumerable<DateTimeOffset>>("timestamps");
                var batteryStates = context.GetArgument<IEnumerable<bool>>("states");
                var updatedStates = await batteryStateRepository.UpdateStatesAsync(ids, timestamps, batteryStates);
                return updatedStates;
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

            FieldAsync<ListGraphType<PriorityStateType>>(
                "updatePriorityStateBunch",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<IdGraphType>>> { Name = "ids" },
                    new QueryArgument<NonNullGraphType<ListGraphType<PriorityStateInputType>>> { Name = "priorityStateTypes" }),
                resolve: async context =>
                {
                    var time = DateTimeOffset.Now;
                    var ids = context.GetArgument<IEnumerable<Guid>>("ids");
                    var priorityStates = context.GetArgument<IEnumerable<PriorityState>>("priorityStateTypes");

                    var updatedStates = new List<PriorityState>();
                    foreach (var priorityState in priorityStates)
                    {
                        var state = await priorityStateRepostiory.UpdateAsync(ids.ElementAt(updatedStates.Count), priorityState);
                        updatedStates.Add(state);
                    }
                    return updatedStates;
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
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
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

            FieldAsync<MeasureValueType>(
            "createEasyMeasureValue",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<FloatGraphType>> { Name = "value" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "pointId" }
                ),
            resolve: async context =>
            {
                var value = context.GetArgument<double>("value");
                var pointId = context.GetArgument<Guid>("pointId");
                var measureValue = new MeasureValue
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTimeOffset.Now,
                    Point = pointId,
                    Value = value
                };
                return await measureValueRepository.SaveAsync(measureValue);
            });

            FieldAsync<ListGraphType<MeasureValueType>>(
                "createMeasureValueBunch",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<FloatGraphType>>> { Name = "values" },
                    new QueryArgument<NonNullGraphType<ListGraphType<IdGraphType>>> { Name = "points" }),
                resolve: async context =>
                {
                    var time = DateTimeOffset.Now;
                    var values = context.GetArgument<IEnumerable<double>>("values");
                    var points = context.GetArgument<IEnumerable<Guid>>("points");
                    var measureValues = new List<MeasureValue>();
                    foreach (var value in values)
                    {
                        var point = points.ElementAt(measureValues.Count);
                        measureValues.Add(new
                            MeasureValue
                        {
                            Id = Guid.NewGuid(),
                            Point = point,
                            Timestamp = time,
                            Value = value
                        });
                    }
                    _ = await measureValueRepository.SaveBulkAsync(measureValues.ToArray());
                    return measureValues;
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
