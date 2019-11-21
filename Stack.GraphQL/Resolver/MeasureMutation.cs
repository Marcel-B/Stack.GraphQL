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
            IRepositoryWrapper rep)
        {
            Name = "Mutation";

            #region Update
            Field<BatteryStateType>(
                "updateBatteryState",
                "Update the state of the battery.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The unique identifier of the battery." },
                    new QueryArgument<NonNullGraphType<BatteryStateInputType>> { Name = "batteryStateType" }
                    ),
                    context =>
                    {
                        var id = context.GetArgument<Guid>("id");
                        var state = context.GetArgument<BatteryState>("batteryStateType");
                        state.Id = id;
                        return rep.BatteryState.Update(state);
                    });

            Field<ListGraphType<BatteryStateType>>(
            "updateBatteryStateBunch",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<ListGraphType<IdGraphType>>> { Name = "ids" },
                new QueryArgument<NonNullGraphType<ListGraphType<DateTimeOffsetGraphType>>> { Name = "timestamps" },
                new QueryArgument<NonNullGraphType<ListGraphType<BooleanGraphType>>> { Name = "states" }),
            resolve: context =>
            {
                var time = DateTimeOffset.Now;
                var ids = context.GetArgument<IList<Guid>>("ids");
                var timestamps = context.GetArgument<IList<DateTimeOffset>>("timestamps");
                var batteryStates = context.GetArgument<IList<bool>>("states");
                var newBatteryStates = new List<BatteryState>();
                foreach (var id in ids)
                {
                    newBatteryStates.Add(new BatteryState
                    {
                        Id = id,
                        State = batteryStates[newBatteryStates.Count],
                        Timestamp = timestamps[newBatteryStates.Count]
                    });
                }
                var updatedStates = rep.BatteryState.UpdateBunch(newBatteryStates);
                return updatedStates;
            });


            Field<PriorityStateType>(
                "updatePriorityState",
                "Update the value of the Priority.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The unique identifier of the battery." },
                    new QueryArgument<NonNullGraphType<PriorityStateInputType>> { Name = "priorityStateType" }
                    ),
                    context =>
                    {
                        var id = context.GetArgument<Guid>("id");
                        var state = context.GetArgument<PriorityState>("priorityStateType");
                        return rep.PriorityState.Update(state);
                    });

            Field<ListGraphType<PriorityStateType>>(
                "updatePriorityStateBunch",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<IdGraphType>>> { Name = "ids" },
                    new QueryArgument<NonNullGraphType<ListGraphType<PriorityStateInputType>>> { Name = "priorityStateTypes" }),
                resolve: context =>
                {
                    var time = DateTimeOffset.Now;
                    var ids = context.GetArgument<IList<Guid>>("ids");
                    var priorityStates = context.GetArgument<IList<PriorityState>>("priorityStateTypes");
                    var updatedStates = new List<PriorityState>();
                    foreach (var id in ids)
                    {
                        updatedStates.Add(new PriorityState
                        {
                            Id = id,
                            State = priorityStates[updatedStates.Count].State,
                            Timestamp = priorityStates[updatedStates.Count].Timestamp
                        });
                    }
                    var result = rep.PriorityState.UpdateBunch(updatedStates);
                    return result;
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
                    var old = await rep.Unit.SelectByIdAsync(id);

                    old.Name = unit.Name;
                    old.Display = unit.Display;

                    return rep.Unit.Update(old);
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

                    var old = await rep.MeasurePoint.SelectByIdAsync(id);

                    old.Display = measurePoint.Display;
                    old.Unit = measurePoint.Unit;
                    old.Location = measurePoint.Location;
                    old.Min = measurePoint.Min;
                    old.Max = measurePoint.Max;
                    old.ExternId = measurePoint.ExternId ?? old.ExternId;

                    return rep.MeasurePoint.Update(old);
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

                    var old = await rep.Link.SelectByIdAsync(id);
                    old.Name = link.Name;
                    old.LinkValue = link.LinkValue;

                    return rep.Link.Update(old);
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

                    return await rep.MeasurePoint.InsertAsync(measurePoint);
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

                    return await rep.MeasureValue.InsertAsync(measureValue);
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
                return await rep.MeasureValue.InsertAsync(measureValue);
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
                    _ = await rep.MeasureValue.InsertBunchAsync(measureValues);
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

                    return await rep.Unit.InsertAsync(unit);
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

                    return await rep.Location.InsertAsync(location);
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

                    return await rep.Link.InsertAsync(link);
                });
            #endregion
        }
    }
}
