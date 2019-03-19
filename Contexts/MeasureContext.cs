using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using com.b_velop.stack.Classes.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace com.b_velop.stack.GraphQl.Contexts
{
    public class MeasureContext : DbContext
    {
        private ILogger<MeasureContext> _logger;

        public DbSet<MeasurePoint> MeasurePoints { get; set; }
        public DbSet<MeasureValue> MeasureValues { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<BatteryState> BatteryStates { get; set; }
        public DbSet<PriorityState> PriorityStates { get; set; }

        public MeasureContext(
            ILogger<MeasureContext> logger,
            DbContextOptions<MeasureContext> context) : base(context)
        {
            _logger = logger;
        }

        public async Task<object> GetTimeTypeByTimeAsync(
            int getArgument,
            Guid id)
        {
            _logger.LogInformation(2571, $"Try to get '{id}' '{getArgument}'");

            var now = DateTimeOffset.Now.AddSeconds(-getArgument);
            var values = await MeasureValues
                    .Where(x => x.Point == id)
                    .Where(x => x.Timestamp >= now)
                    .OrderBy(x => x.Timestamp).ToListAsync();

            return values;
        }

        public async Task<object> UpdatePriorityStateAsync(
            Guid id,
            PriorityState state)
        {
            try
            {
                var tmp = await PriorityStates.FirstOrDefaultAsync(x => x.Id == id);

                tmp.State = state.State;
                tmp.Timestamp = state.Timestamp;

                Entry(tmp).State = EntityState.Modified;
                await SaveChangesAsync();
                return tmp.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(2571, ex, $"Error occurred while updating PriorityState '{id}' to '{state?.State}'.", id, state);
                return null;
            }
        }

        public async Task<object> UpdateBatteryStateAsync(
            Guid id,
            BatteryState state)
        {
            try
            {
                var tmp = await BatteryStates.FirstOrDefaultAsync(x => x.Id == id);

                tmp.State = state.State;
                tmp.Timestamp = state.Timestamp;

                Entry(tmp).State = EntityState.Modified;
                await SaveChangesAsync();
                return tmp.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(2571, ex, $"Error occurred while updating BatteryState '{id}' to '{state?.State}'.", id, state);
                return null;
            }
        }

        public async Task<object> GetUnitsAsync()
            => await Units.ToListAsync();

        public async Task<object> GetMeasureValuesAsync()
            => await MeasureValues.ToListAsync();

        public async Task<object> GetMeasurePointsAsync()
            => await MeasurePoints.ToListAsync();

        public async Task<object> GetPriorityStatesAsync()
            => await PriorityStates.ToListAsync();

        public async Task<object> GetBatteryStatesAsync()
            => await BatteryStates.ToListAsync();

        public async Task<object> AddMeasurePointAsync(
            MeasurePoint measurePoint)
        {
            try
            {
                measurePoint.Id = Guid.NewGuid();
                await MeasurePoints.AddAsync(measurePoint);
                await SaveChangesAsync();
                return measurePoint;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while add measure point", measurePoint);
                return null;
            }
        }

        public async Task<object> AddUnitAsync(
                Unit unit)
        {
            try
            {
                unit.Id = Guid.NewGuid();
                await Units.AddAsync(unit);
                await SaveChangesAsync();
                return unit;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while persist Unit '{unit.Display}'", unit);
                return null;
            }
        }

        public async Task<object> UpdateUnitAsync(
            Guid id,
            Unit unit)
        {
            try
            {
                var tmp = await Units.FirstOrDefaultAsync(x => x.Id == id);
                tmp.Name = unit.Name;
                tmp.Display = unit.Display;

                Entry(tmp).State = EntityState.Modified;
                await SaveChangesAsync();
                return tmp.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while updating Unit '{id}'", id, unit);
                return null;
            }
        }

        public async Task<object> UpdateMeasurePointAsync(
            Guid id,
            MeasurePoint measurePoint)
        {
            try
            {
                var mp = await MeasurePoints.FirstOrDefaultAsync(x => x.Id == id);
                mp.Display = measurePoint.Display;
                mp.Max = measurePoint.Max;
                mp.Min = measurePoint.Min;
                mp.Unit = measurePoint.Unit;
                Entry(mp).State = EntityState.Modified;
                await SaveChangesAsync();
                return mp.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred whie updating MeasurePoint '{id}'.", id, measurePoint);
                return null;
            }
        }

        public async Task<object> AddMeasureValueAsync(
            MeasureValue measureValue)
        {
            try
            {
                measureValue.Id = Guid.NewGuid();
                await MeasureValues.AddAsync(measureValue);
                await SaveChangesAsync();
                return measureValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while persist MeasureValue ID: '{measureValue.Id}', Timestamp: '{measureValue.Timestamp}', Value: '{measureValue.Value}', PointID: '{measureValue.Point}'", measureValue);
                return null;
            }
        }

        public async Task<object> AddLocationAsync(
           Location location)
        {
            try
            {
                location.Id = Guid.NewGuid();
                location.Created = DateTimeOffset.Now;
                await Locations.AddAsync(location);
                await SaveChangesAsync();
                return location;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while persist Location ID: '{location.Id}', Created: '{location.Created}', Display: '{location.Display}', Description: '{location.Description}'", location);
                return null;
            }
        }

        public async Task<MeasurePoint> GetMeasurePointAsync(
          Guid id)
          => await MeasurePoints.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Unit> GetUnitAsync(
            Guid id)
            => await Units.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<object> GetMeasureValueAsync(
            Guid id)
            => await MeasureValues.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Location> GetLocationAsync(
            Guid id)
            => await Locations.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<object> GetLocationsAsync()
            => await Locations.ToListAsync();
    }
}
