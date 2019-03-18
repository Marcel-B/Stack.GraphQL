﻿using com.b_velop.stack.Classes.Models;
using com.b_velop.stack.GraphQl.Contexts;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class MeasurePointType : ObjectGraphType<MeasurePoint>
    {
        public MeasurePointType(
            MeasureContext measureContext)
        {
            Name = "MeasurePoint";
            Description = "A point that produces measure values.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of the MeasurePoint");
            Field(x => x.Display).Description("The readable name of the MeasurePoint");
            Field(x => x.Max).Description("The maximal possible value of the Unit.");
            Field(x => x.Min).Description("The minimal possible value of the Unit.");

            FieldAsync<UnitType, Unit>(
                nameof(MeasurePoint.Unit),
                "The Unit of the measure point",
                resolve: context => measureContext.GetUnitAsync(context.Source.Unit));

            FieldAsync<LocationType, Location>(
                nameof(MeasurePoint.Location),
                "The location of the measure point",
                resolve: context => measureContext.GetLocationAsync(context.Source.Location));
        }
    }
}
