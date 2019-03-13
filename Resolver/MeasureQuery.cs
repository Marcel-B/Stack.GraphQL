using System;
using System.Threading.Tasks;
using com.b_velop.GraphQl.Contexts;
using com.b_velop.GraphQl.Types;
using GraphQL.Types;

namespace com.b_velop.GraphQl.Resolver
{
    public class MeasureQuery : ObjectGraphType<object>
    {
        public MeasureQuery(
            MeasureContext measureContext)
        {
            Name = "Query";

            FieldAsync<TimeTypeInterface>(
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

            Field<ListGraphType<UnitType>>(
                "units",
                resolve: context => measureContext.GetUnitsAsync());

            Field<ListGraphType<MeasureValueType>>(
                "measureValues",
                resolve: context => measureContext.GetMeasureValuesAsync());

            Field<ListGraphType<MeasurePointType>>(
                "measurePoints",
                resolve: context => measureContext.GetMeasurePointsAsync());
        }
    }
}
