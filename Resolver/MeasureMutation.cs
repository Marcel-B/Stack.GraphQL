using System;
using com.b_velop.stack.Classes.Models;
using com.b_velop.stack.GraphQl.Contexts;
using com.b_velop.stack.GraphQl.InputTypes;
using com.b_velop.stack.GraphQl.Types;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Resolver
{
    public class MeasureMutation : ObjectGraphType
    {
        public MeasureMutation(
            MeasureContext measureContext)
        {
            Name = "Mutation";

            FieldAsync<BatteryStateType>(
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
                        return measureContext.UpdateBatteryStateAsync(id, state);
                    });

            FieldAsync<PriorityStateType>(
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
                        return measureContext.UpdatePriorityStateAsync(id, state);
                    });

            FieldAsync<MeasurePointType>(
                "createMeasurePoint",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MeasurePointInputType>> { Name = "measurePointType" }
                ),
                resolve: context =>
                {
                    var measurePoint = context.GetArgument<MeasurePoint>("measurePointType");
                    return measureContext.AddMeasurePointAsync(measurePoint);
                });

            FieldAsync<MeasureValueType>(
                "createMeasureValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MeasureValueInputType>> { Name = "measureValueType" }
                    ),
                resolve: context =>
                {
                    var measureValue = context.GetArgument<MeasureValue>("measureValueType");
                    return measureContext.AddMeasureValueAsync(measureValue);
                });

            FieldAsync<UnitType>(
                "createUnit",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UnitInputType>> { Name = "unitType" }
                    ),
                resolve: context =>
                {
                    var unit = context.GetArgument<Unit>("unitType");
                    return measureContext.AddUnitAsync(unit);
                });

            FieldAsync<UnitType>(
                "updateUnit",
                "Update Unit by id",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<UnitInputType> { Name = "unitType" }
                    ),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var unit = context.GetArgument<Unit>("unitType");
                    return measureContext.UpdateUnitAsync(id, unit);
                });

            FieldAsync<MeasurePointType>(
                "updateMeasurePoint",
                "Update MeasurePoint by id.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<MeasurePointType> { Name = "measurePointType" }),
                context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var measurePoint = context.GetArgument<MeasurePoint>("measurePointType");
                    return measureContext.UpdateMeasurePointAsync(id, measurePoint);
                });

            FieldAsync<LocationType>(
                "createLocation",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LocationInputType>> { Name = "locationType" }
                    ),
                resolve: context =>
                {
                    var location = context.GetArgument<Location>("locationType");
                    return measureContext.AddLocationAsync(location);
                });
        }
    }
}
