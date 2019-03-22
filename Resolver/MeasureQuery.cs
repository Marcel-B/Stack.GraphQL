using System;
using com.b_velop.stack.GraphQl.Contexts;
using com.b_velop.stack.GraphQl.Types;
using GraphQL.Types;
using com.b_velop.stack.Classes.Models;

namespace com.b_velop.stack.GraphQl.Resolver
{
    public class MeasureQuery : ObjectGraphType<object>
    {
        public MeasureQuery(
            MeasureStore measureContext)
        {
            Name = "Query";

            FieldAsync<ListGraphType<PriorityStateType>>(
                "priorityStates",
                "Get all priority states",
                resolve: context => measureContext.GetPriorityStatesAsync());

            FieldAsync<ListGraphType<BatteryStateType>>(
                "batteryStates",
                "Get all battery states",
                resolve: context => measureContext.GetBatteryStatesAsync());

            FieldAsync<ListGraphType<ActiveMeasurePointType>>(
                "activeMeasurePoints",
                "Get all ActiveMeasurePoints entitys",
                resolve: context => measureContext.GetActiveMeasurePoints());

            FieldAsync<ListGraphType<TimeTypeInterface>>(
                "timeTypeSpan",
                "Returns Timestamps by Id and TimeSpan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "timeSpan", Description = "The timespan in seconds to look for" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The id of the MeasureValue" }
                ),
                resolve: context => measureContext.GetTimeTypeByTimeAsync(
                    context.GetArgument<int>("timeSpan"),
                    context.GetArgument<Guid>("id")));

            FieldAsync<ListGraphType<MeasureValueType>>(
                "measureValuesSpan",
                "Returns MeasureValues by Id and TimeSpan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "timeSpan", Description = "The timespan in seconds to look for" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The id of the MeasureValue" }
                ),
                resolve: context => measureContext.GetTimeTypeByTimeAsync(
                    context.GetArgument<int>("timeSpan"),
                    context.GetArgument<Guid>("id")));

            FieldAsync<ListGraphType<UnitType>>(
                "units",
                resolve: context => measureContext.GetUnitsAsync());

            FieldAsync<ListGraphType<LocationType>>(
                "locations",
                resolve: context => measureContext.GetLocationsAsync());

            //FieldAsync<LocationType>(
            //    "location",
            //    "Return a location by their Id",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            //        ),
            //    resolve: contenxt => measureContext.GetLocationAsync(
            //        contenxt.GetArgument<Guid>("id")));

            FieldAsync<ListGraphType<MeasureValueType>>(
                "measureValues",
                resolve: context => measureContext.GetMeasureValuesAsync());

            FieldAsync<ListGraphType<MeasurePointType>>(
                "measurePoints",
                resolve: context => measureContext.GetMeasurePointsAsync());
        }
    }
}
