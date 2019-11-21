using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class MeasurePointType : ObjectGraphType<MeasurePoint>
    {
        public MeasurePointType(
            IRepositoryWrapper rep)
        {
            Name = "MeasurePoint";
            Description = "A point that produces measure values.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of the MeasurePoint");
            Field(x => x.Display).Description("The readable name of the MeasurePoint");
            Field(x => x.Max).Description("The maximal possible value of the Unit.");
            Field(x => x.Min).Description("The minimal possible value of the Unit.");
            Field(x => x.ExternId, nullable: true).Description("The extern ID of the MeasurePoint.");
            Field(x => x.Created, nullable: true).Description("The creation time of the MeasurePoint.");
            Field(x => x.Updated, nullable: true).Description("The description of the MeasurePoint.");

            FieldAsync<UnitType, Unit>(
                nameof(MeasurePoint.Unit),
                "The Unit of the measure point",
                resolve: async context => await rep.Unit.SelectByIdAsync(context.Source.Unit));

            FieldAsync<LocationType, Location>(
                nameof(MeasurePoint.Location),
                "The location of the measure point",
                resolve: async context => await rep.Location.SelectByIdAsync(context.Source.Location));
        }
    }
}
