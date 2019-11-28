using System;
using com.b_velop.stack.GraphQl.Types;
using GraphQL.Types;
using com.b_velop.stack.DataContext.Repository;
using Microsoft.Extensions.Logging;

namespace com.b_velop.stack.GraphQl.Resolver
{
    public class MeasureQuery : ObjectGraphType<object>
    {
        public MeasureQuery(
            IRepositoryWrapper rep,
            ILogger<MeasureQuery> logger)
        {
            Name = "Query";

            #region GetAll
            FieldAsync<ListGraphType<PriorityStateType>>(
                "priorityStates",
                "Get all priority states",
                resolve: async context => await rep.PriorityState.SelectAllAsync());

            FieldAsync<ListGraphType<BatteryStateType>>(
                "batteryStates",
                "Get all battery states",
                resolve: async context => await rep.BatteryState.SelectAllAsync());

            FieldAsync<ListGraphType<ActiveMeasurePointType>>(
                "activeMeasurePoints",
                "Get all ActiveMeasurePoints entities",
                resolve: async context => await rep.ActiveMeasurePoint.SelectAllAsync());

            FieldAsync<ListGraphType<UnitType>>(
                "units",
                "Request all units",
                 resolve: async context => await rep.Unit.SelectAllAsync());

            FieldAsync<ListGraphType<LocationType>>(
                "locations",
                "Request all locations",
                resolve: async context => await rep.Location.SelectAllAsync());

            FieldAsync<ListGraphType<MeasureValueType>>(
                "measureValues",
                "Request all measureValues",
                resolve: async context => await rep.MeasureValue.SelectAllAsync());

            FieldAsync<ListGraphType<MeasurePointType>>(
                "measurePoints",
                "Request all measurePoints",
                resolve: async context => await rep.MeasurePoint.SelectAllAsync());

            FieldAsync<ListGraphType<LinkType>>(
                "links",
                "Request all links",
                resolve: async context => await rep.Link.SelectAllAsync());
            #endregion

            #region Get with filter

            //FieldAsync<ListGraphType<TimeTypeInterface>>(
            //    "timeTypeSpan",
            //    "Returns Timestamps by Id and TimeSpan",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "timeSpan", Description = "The timespan in seconds to look for" }
            //    ),
            //    resolve: async context =>
            //    {
            //        var spanInSeconds = context.GetArgument<int>("timeSpan");
            //        return await rep.MeasureValue.SelectByConditionAsync(_ => _.);
            //    });

            //FieldAsync<ListGraphType<MeasureValueType>>(
            //    "measureValuesSpan",
            //    "Returns MeasureValues by Id and TimeSpan",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<IntGraphType>>
            //        {
            //            Name = "timeSpan",
            //            Description = "The timespan in seconds to look for"
            //        },
            //        new QueryArgument<NonNullGraphType<IdGraphType>>
            //        {
            //            Name = "id",
            //            Description = "The id of the MeasureValue"
            //        }
            //    ),
            //    resolve: async context => await measureValueTimeRepository.FilterPointValuesByTimeAsync(context.GetArgument<Guid>("id"), context.GetArgument<int>("timeSpan"))
            //);

            FieldAsync<ListGraphType<LinkType>>(
                "link",
                "Request a single link by Id",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique identifier of the link"
                    }),
                resolve: async context => await rep.Link.SelectByIdAsync(context.GetArgument<Guid>("id"))
            );

            FieldAsync<MeasurePointType>(
                "measurePoint",
                "Request a MeasurePoint by ID",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "The unique identifier of the unit" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var measurePoint = await rep.MeasurePoint.SelectByIdAsync(id);
                    return measurePoint;
                });

            #endregion
        }
    }
}
