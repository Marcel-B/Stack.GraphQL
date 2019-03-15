using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using com.b_velop.stack.Classes.Models;
using System.Collections.Generic;

namespace com.b_velop.stack.GraphQl.Contexts
{
    public class MeasureContext : DbContext
    {
        public DbSet<MeasurePoint> MeasurePoints { get; set; }
        public DbSet<MeasureValue> MeasureValues { get; set; }
        public DbSet<Unit> Units { get; set; }

        public MeasureContext(
            DbContextOptions<MeasureContext> context) : base(context) { }

        public async Task<object> GetTimeTypeByTimeAsync(
            TimeSpan getArgument,
            Guid id)
        {
            var now = DateTimeOffset.Now - getArgument;
            var values = await 
                Task.Run(() => MeasureValues
                    .Where(x => x.Point == id)
                    .Where(x => x.Timestamp >= now)
                    .OrderBy(x => x.Timestamp));
            return await values.ToListAsync();
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
