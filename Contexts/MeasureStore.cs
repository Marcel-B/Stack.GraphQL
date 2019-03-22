using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using com.b_velop.stack.Classes.Models;
using Microsoft.Extensions.Logging;
using com.b_velop.stack.DataContext.Abstract;

namespace com.b_velop.stack.GraphQl.Contexts
{
    public class MeasureStore
    {
        private MeasureContext _measureContext;
        private ILogger<MeasureStore> _logger;

        public MeasureStore(
            MeasureContext context,
            ILogger<MeasureStore> logger)
        {
            _measureContext = context;
            _logger = logger;
        }
        public async Task<object> GetTimeTypeByTimeAsync(
            int getArgument,
            Guid id)
        {
            _logger.LogInformation(2571, $"Try to get '{id}' '{getArgument}'");

            var now = DateTimeOffset.Now.AddSeconds(-getArgument);
            var values = await _measureContext.MeasureValues
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
                var tmp = await _measureContext.PriorityStates.FirstOrDefaultAsync(x => x.Id == id);

                tmp.State = state.State;
                tmp.Timestamp = state.Timestamp;

                _measureContext.Entry(tmp).State = EntityState.Modified;
                await _measureContext.SaveChangesAsync();
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
                var tmp = await _measureContext.BatteryStates.FirstOrDefaultAsync(x => x.Id == id);

                tmp.State = state.State;
                tmp.Timestamp = state.Timestamp;

                _measureContext.Entry(tmp).State = EntityState.Modified;
                await _measureContext.SaveChangesAsync();
                return tmp.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(2571, ex, $"Error occurred while updating BatteryState '{id}' to '{state?.State}'.", id, state);
                return null;
            }
        }

        public async Task<object> GetUnitsAsync()
            => await _measureContext.Units.ToListAsync();

        public async Task<object> GetMeasureValuesAsync()
            => await _measureContext.MeasureValues.ToListAsync();

        public async Task<object> GetMeasurePointsAsync()
            => await _measureContext.MeasurePoints.ToListAsync();

        public async Task<object> GetPriorityStatesAsync()
            => await _measureContext.PriorityStates.ToListAsync();

        public async Task<object> GetBatteryStatesAsync()
            => await _measureContext.BatteryStates.ToListAsync();

        public async Task<object> GetActiveMeasurePoints()
            => await _measureContext.ActiveMeasurePoints.ToListAsync();

        public async Task<object> AddMeasurePointAsync(
            MeasurePoint measurePoint)
        {
            try
            {
                measurePoint.Id = Guid.NewGuid();
                await _measureContext.MeasurePoints.AddAsync(measurePoint);
                await _measureContext.SaveChangesAsync();
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
                await _measureContext.Units.AddAsync(unit);
                await _measureContext.SaveChangesAsync();
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
                var tmp = await _measureContext.Units.FirstOrDefaultAsync(x => x.Id == id);
                tmp.Name = unit.Name;
                tmp.Display = unit.Display;

                _measureContext.Entry(tmp).State = EntityState.Modified;
                await _measureContext.SaveChangesAsync();
                return tmp;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while updating Unit '{id}'", id, unit);
                return null;
            }
        }

        public async Task<object> UpdateActiveMeasurePointAsync(
            Guid id,
            ActiveMeasurePoint activeMeasurePoint)
        {
            try
            {
                var tmp = await _measureContext.ActiveMeasurePoints.FirstOrDefaultAsync(x => x.Id == id);
                tmp.IsActive = activeMeasurePoint.IsActive;
                tmp.LastValue = activeMeasurePoint.LastValue;
                tmp.Updated = activeMeasurePoint.Updated;

                _measureContext.Entry(tmp).State = EntityState.Modified;
                await _measureContext.SaveChangesAsync();
                return tmp;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while updating ActiveMeasurePoint '{id}.'", id, activeMeasurePoint);
                return null;
            }
        }

        public async Task<object> UpdateMeasurePointAsync(
            Guid id,
            MeasurePoint measurePoint)
        {
            try
            {
                var mp = await _measureContext.MeasurePoints.FirstOrDefaultAsync(x => x.Id == id);
                mp.Display = measurePoint.Display;
                mp.Max = measurePoint.Max;
                mp.Min = measurePoint.Min;
                mp.Unit = measurePoint.Unit;
                _measureContext.Entry(mp).State = EntityState.Modified;
                await _measureContext.SaveChangesAsync();
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
                await _measureContext.MeasureValues.AddAsync(measureValue);
                await _measureContext.SaveChangesAsync();
                return measureValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while persist MeasureValue ID: '{measureValue.Id}', Timestamp: '{measureValue.Timestamp}', Value: '{measureValue.Value}', PointID: '{measureValue.Point}'", measureValue);
                return null;
            }
        }

        public async Task<object> AddActiveMeasurePoint(
            ActiveMeasurePoint activeMeasurePoint)
        {
            try
            {
                await _measureContext.ActiveMeasurePoints.AddAsync(activeMeasurePoint);
                await _measureContext.SaveChangesAsync();
                return activeMeasurePoint;
            }
            catch (Exception ex)
            {
                _logger.LogError(2573, ex, $"Error occurred while persist ActiveMeasurePoint '{activeMeasurePoint.Id}'.", activeMeasurePoint);
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
                await _measureContext.Locations.AddAsync(location);
                await _measureContext.SaveChangesAsync();
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
          => await _measureContext.MeasurePoints.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Unit> GetUnitAsync(
            Guid id)
            => await _measureContext.Units.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<object> GetMeasureValueAsync(
            Guid id)
            => await _measureContext.MeasureValues.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Location> GetLocationAsync(
            Guid id)
            => await _measureContext.Locations.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<object> GetLocationsAsync()
            => await _measureContext.Locations.ToListAsync();
    }
}
