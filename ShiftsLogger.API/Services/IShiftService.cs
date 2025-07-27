using ShiftsLogger.API.Models;

namespace ShiftsLogger.API.Services
{
    public interface IShiftService
    {
        Task<IEnumerable<Shift>> GetAllShiftAsync();
        Task<Shift?> GetShiftByIdAsync(int id);
        Task<Shift> AddShiftAsync(Shift shift);
        Task<bool> UpdateShiftAsync(int id, Shift updatedShift); 
        Task<bool> DeleteShiftAsync(int id);
    }

}
