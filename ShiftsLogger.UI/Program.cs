using ShiftsLogger.UI.Models;
using ShiftsLogger.UI.Services;

var shiftService = new ShiftServices();
bool isRunning = true;

while (isRunning)
{
    Console.Clear();
    Console.WriteLine("==== SHIFT LOGGER ====");
    Console.WriteLine("1. View All Shifts");
    Console.WriteLine("2. View Shift by ID");
    Console.WriteLine("3. Add New Shift");
    Console.WriteLine("4. Update Existing Shift");
    Console.WriteLine("5. Delete Shift");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            var shifts = await shiftService.GetAllShiftsAsync();
            foreach (var shift in shifts)
            {
                Console.WriteLine($"{shift.Id}: {shift.WorkerName} | {shift.StartTime} - {shift.EndTime}");
            }
            break;

        case "2":
            Console.Write("Enter Shift ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var shift = await shiftService.GetShiftByIdAsync(id);
                if (shift != null)
                {
                    Console.WriteLine($"{shift.Id}: {shift.WorkerName} | {shift.StartTime} - {shift.EndTime}");
                }
                else
                {
                    Console.WriteLine("Shift not found.");
                }
            }
            break;

        case "3":
            var newShift = new Shift();
            Console.Write("Enter Worker Name: ");
            newShift.WorkerName = Console.ReadLine()!;
            Console.Write("Enter Start Time (yyyy-mm-dd hh:mm): ");
            newShift.StartTime = DateTime.Parse(Console.ReadLine()!);
            Console.Write("Enter End Time (yyyy-mm-dd hh:mm): ");
            newShift.EndTime = DateTime.Parse(Console.ReadLine()!);
            var added = await shiftService.AddShiftAsync(newShift);
            Console.WriteLine(added ? "Shift added!" : "Failed to add shift.");
            break;

        case "4":
            var allShiftsToUpdate = await shiftService.GetAllShiftsAsync();
            Console.WriteLine("\n--- All Shifts ---");
            foreach (var shift in allShiftsToUpdate)
            {
                Console.WriteLine($"ID: {shift.Id}, Name: {shift.WorkerName}, Start: {shift.StartTime}, End: {shift.EndTime}");
            }
            Console.WriteLine("------------------\n");

            Console.Write("Enter Shift ID to Update: ");
            if (int.TryParse(Console.ReadLine(), out int updateId))
            {
                var existing = await shiftService.GetShiftByIdAsync(updateId);
                if (existing == null)
                {
                    Console.WriteLine("Shift not found.");
                    break;
                }

                Console.WriteLine("\nPress Enter to keep existing value.");

                Console.Write($"New Worker Name (current: {existing.WorkerName}): ");
                string? newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    existing.WorkerName = newName;

                Console.Write($"New Start Time (yyyy-mm-dd hh:mm) (current: {existing.StartTime}): ");
                string? newStartInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newStartInput) && DateTime.TryParse(newStartInput, out var newStart))
                    existing.StartTime = newStart;

                Console.Write($"New End Time (yyyy-mm-dd hh:mm) (current: {existing.EndTime}): ");
                string? newEndInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEndInput) && DateTime.TryParse(newEndInput, out var newEnd))
                    existing.EndTime = newEnd;

                var updated = await shiftService.UpdateShiftAsync(updateId, existing);
                Console.WriteLine(updated ? "Shift updated!" : "Update failed.");
            }
            break;




        case "5":
            var allShifts1 = await shiftService.GetAllShiftsAsync();
            Console.WriteLine("\n--- All Shifts ---");
            foreach (var shift in allShifts1)
            {
                Console.WriteLine($"ID: {shift.Id}, Name: {shift.WorkerName}, Start: {shift.StartTime}, End: {shift.EndTime}");
            }
            Console.WriteLine("------------------\n");

            Console.Write("Enter Shift ID to Delete: ");
            if (int.TryParse(Console.ReadLine(), out int deleteId))
            {
                var shiftToDelete = await shiftService.GetShiftByIdAsync(deleteId);
                if (shiftToDelete == null)
                {
                    Console.WriteLine("Shift not found.");
                    break;
                }

                Console.WriteLine($"Are you sure you want to delete shift: [ID: {shiftToDelete.Id}, Name: {shiftToDelete.WorkerName}]? (Y/N): ");
                string? confirmation = Console.ReadLine()?.Trim().ToUpper();

                if (confirmation == "Y")
                {
                    var deleted = await shiftService.DeleteShiftAsync(deleteId);
                    Console.WriteLine(deleted ? "Shift deleted!" : "Deletion failed.");

                    if (deleted)
                    {
                        var updatedShifts = await shiftService.GetAllShiftsAsync();
                        Console.WriteLine("\n--- Updated Shift List ---");
                        foreach (var shift in updatedShifts)
                        {
                            Console.WriteLine($"ID: {shift.Id}, Name: {shift.WorkerName}, Start: {shift.StartTime}, End: {shift.EndTime}");
                        }
                        Console.WriteLine("---------------------------\n");
                    }
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            break;


        case "0":
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}
