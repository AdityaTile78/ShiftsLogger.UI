using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Data;
using ShiftsLogger.API.Models;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

namespace ShiftsLogger.API.Services
{
    public class ShiftService : IShiftService
    {

        //readonly = it becomes immutable — you can use it but cannot reassign it.
        //This means the field can only be assigned once at the time of declaration or in the constructor.
        private readonly ShiftsDbContext _context;

        public ShiftService(ShiftsDbContext context)
        {
            //This sets _context to the instance of ShiftsDbContext passed into the class, 
            //usually via Dependency Injection.
            _context = context;
        }
        public async Task<IEnumerable<Shift>> GetAllShiftAsync()
        {
            return await _context.Shifts.ToListAsync();
        }

        public async Task<Shift?> GetShiftByIdAsync(int id)
        {
            return await _context.Shifts.FindAsync(id);
        }

        public async Task<Shift> AddShiftAsync(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
            return shift;
        }

        public async Task<bool> DeleteShiftAsync(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                throw new KeyNotFoundException($"Shift with ID {id} not found.");
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateShiftAsync(int id, Shift updatedShift)
        {
            var existingShift = await _context.Shifts.FindAsync(id);
            if (existingShift == null)
            {
                throw new KeyNotFoundException($"Shift with ID {id} not found.");
            }

            existingShift.WorkerName = updatedShift.WorkerName;
            existingShift.StartTime = updatedShift.StartTime;
            existingShift.EndTime = updatedShift.EndTime;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
