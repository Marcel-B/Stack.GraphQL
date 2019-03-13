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

            FieldAsync<MeasurePointType>(
                "createMeasurePoint",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MeasurePointInputType>> { Name = "measurePointType"}
                ),
                resolve: context =>
                {
                    var measurePoint = context.GetArgument<MeasurePoint>("measurePointType");
                    return measureContext.AddMeasurePointAsync(measurePoint);
                });

            Field<MeasureValueType>(
                "createMeasureValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MeasureValueInputType>>{Name = "measureValueType"}
                    ),
                resolve: context =>
                {
                    var measureValue = context.GetArgument<MeasureValue>("measureValueType");
                    return measureContext.AddMeasureValueAsync(measureValue);
                });

            Field<UnitType>(
                "createUnit",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UnitInputType>>{Name = "unitType"}
                    ),
                resolve: context =>
                {
                    var unit = context.GetArgument<Unit>("unitType");
                    return measureContext.AddUnitAsync(unit);
                } );
        }
    }
}
