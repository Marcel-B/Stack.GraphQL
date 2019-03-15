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

        public MeasureContext(
            ILogger<MeasureContext> logger,
            DbContextOptions<MeasureContext> context) : base(context) {
            _logger = logger;
        }

        public async Task<object> GetTimeTypeByTimeAsync(
            TimeSpan getArgument,
            Guid id)
        {
            var now = DateTimeOffset.Now - getArgument;
            var values = await MeasureValues
                    .Where(x => x.Point == id)
                    .Where(x => x.Timestamp >= now)
                    .OrderBy(x => x.Timestamp).ToListAsync();
            foreach (var value in values)
            {
                _logger.LogInformation($"The value is '{value.Value}' Time '{value.Timestamp}' Point: '{value.Point}'");
            }
            return values;
        }

        public async Task<object> GetUnitsAsync()
            => await Units.ToListAsync();

        public async Task<object> GetMeasureValuesAsync()
            => await MeasureValues.ToListAsync();

        public async Task<object> GetMeasurePointsAsync()
            => await MeasurePoints.ToListAsync();

        public async Task<object> AddMeasurePointAsync(
            MeasurePoint measurePoint)
        {
            measurePoint.Id = Guid.NewGuid();
            await MeasurePoints.AddAsync(measurePoint);
            await SaveChangesAsync();
            return measurePoint;
        }

        public async Task<object> AddUnitAsync(
                Unit unit)
        {
            unit.Id = Guid.NewGuid();
            await Units.AddAsync(unit);
            await SaveChangesAsync();
            return unit;
        }

        public async Task<object> AddMeasureValueAsync(
            MeasureValue measureValue)
        {
            measureValue.Id = Guid.NewGuid();
            await MeasureValues.AddAsync(measureValue);
            await SaveChangesAsync();
            return measureValue;
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
    }
}
