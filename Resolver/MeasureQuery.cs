using System;
using com.b_velop.stack.GraphQl.Contexts;
using com.b_velop.stack.GraphQl.Types;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Resolver
{
    public class MeasureQuery : ObjectGraphType<object>
    {
        public MeasureQuery(
            MeasureContext measureContext)
        {
            Name = "Query";
            FieldAsync<ListGraphType<TimeTypeInterface>>(
                "timeTypeSpan",
                "Returns Timestamps by Id and TimeSpan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<TimeSpanSecondsGraphType>> { Name = "timeSpan", Description = "The timespan in seconds to look for" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The id of the MeasureValue" }
                ),
                resolve: context => measureContext.GetTimeTypeByTimeAsync(
                    context.GetArgument<TimeSpan>("timeSpan"),
                    context.GetArgument<Guid>("id"))
                );

            FieldAsync<ListGraphType<MeasureValueType>>(
                "measureValuesSpan",
                "Returns MeasureValues by Id and TimeSpan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<TimeSpanSecondsGraphType>> { Name = "timeSpan", Description = "The timespan in seconds to look for" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The id of the MeasureValue" }
                ),
                resolve: context => measureContext.GetTimeTypeByTimeAsync(
                    context.GetArgument<TimeSpan>("timeSpan"),
                    context.GetArgument<Guid>("id"))
                );

            FieldAsync<ListGraphType<UnitType>>(
                "units",
                resolve: context => measureContext.GetUnitsAsync());

            FieldAsync<ListGraphType<MeasureValueType>>(
                "measureValues",
                resolve: context => measureContext.GetMeasureValuesAsync());

            FieldAsync<ListGraphType<MeasurePointType>>(
                "measurePoints",
                resolve: context => measureContext.GetMeasurePointsAsync());
        }
    }
}
