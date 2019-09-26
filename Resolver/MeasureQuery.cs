using System;
using com.b_velop.stack.GraphQl.Types;
using GraphQL.Types;
using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;

namespace com.b_velop.stack.GraphQl.Resolver
{
    public class MeasureQuery : ObjectGraphType<object>
    {
        public MeasureQuery(
            IDataStore<Unit> unitRepository,
            IDataStore<MeasurePoint> measurePointRepository,
            IDataStore<MeasureValue> measureValueRepository,
            BatteryStateRepository batteryStateRepository,
            IDataStore<ActiveMeasurePoint> activeMeasurePointRepository,
            IDataStore<Location> locationRepository,
            IDataStore<PriorityState> priorityStateRepository,
            ITimeDataStore<MeasureValue> measureValueTimeRepository,
            IDataStore<Link> linkRepository)
        {
            Name = "Query";

            #region GetAll
            FieldAsync<ListGraphType<PriorityStateType>>(
                "priorityStates",
                "Get all priority states",
                resolve: async context => await priorityStateRepository.GetAllAsync());

            FieldAsync<ListGraphType<BatteryStateType>>(
                "batteryStates",
                "Get all battery states",
                resolve: async context => await batteryStateRepository.GetAllAsync());

            FieldAsync<ListGraphType<ActiveMeasurePointType>>(
                "activeMeasurePoints",
                "Get all ActiveMeasurePoints entities",
                resolve: async context => await activeMeasurePointRepository.GetAllAsync());

            FieldAsync<ListGraphType<UnitType>>(
                "units",
                "Request all units",
                 resolve: async context => await unitRepository.GetAllAsync());

            FieldAsync<ListGraphType<LocationType>>(
                "locations",
                "Request all locations",
                resolve: async context => await locationRepository.GetAllAsync());

            FieldAsync<ListGraphType<MeasureValueType>>(
                "measureValues",
                "Request all measureValues",
                resolve: async context => await measurePointRepository.GetAllAsync());

            FieldAsync<ListGraphType<MeasurePointType>>(
                "measurePoints",
                "Request all measurePoints",
                resolve: async context => await measurePointRepository.GetAllAsync());

            FieldAsync<ListGraphType<LinkType>>(
                "links",
                "Request all links",
                resolve: async context => await linkRepository.GetAllAsync());
            #endregion

            #region Get with filter

            FieldAsync<ListGraphType<TimeTypeInterface>>(
                "timeTypeSpan",
                "Returns Timestamps by Id and TimeSpan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "timeSpan", Description = "The timespan in seconds to look for" }
                ),
                resolve: async context =>
                {
                    var spanInSeconds = context.GetArgument<int>("timeSpan");
                    return await measureValueTimeRepository.FilterValuesByTimeAsync(spanInSeconds);
                });

            FieldAsync<ListGraphType<MeasureValueType>>(
                "measureValuesSpan",
                "Returns MeasureValues by Id and TimeSpan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                    {
                        Name = "timeSpan",
                        Description = "The timespan in seconds to look for"
                    },
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The id of the MeasureValue"
                    }
                ),
                resolve: async context => await measureValueTimeRepository.FilterPointValuesByTimeAsync(context.GetArgument<Guid>("id"), context.GetArgument<int>("timeSpan"))
            );

            FieldAsync<ListGraphType<LinkType>>(
                "link",
                "Request a single link by Id",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique identifier of the link"
                    }),
                resolve: async context => await linkRepository.GetAsync(context.GetArgument<Guid>("id"))
            );

            FieldAsync<MeasurePointType>(
                "measurePoint",
                "Request a MeasurePoint by ID",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>{ Name = "id", Description = "The unique identifier of the unit" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var measurePoint = await measurePointRepository.GetAsync(id);
                    return measurePoint;
                });

            #endregion
        }
    }
}
