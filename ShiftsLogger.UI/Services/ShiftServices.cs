using Newtonsoft.Json;
using ShiftsLogger.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLogger.UI.Services
{
    public class ShiftServices
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://localhost:7012/api/shifts";


        public ShiftServices()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Shift>> GetAllShiftsAsync()
        {
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Shift>>(json)!;
        }

        public async Task<Shift> GetShiftByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{baseUrl}/{id}");
           if(!response.IsSuccessStatusCode)
                return null!;
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Shift>(json)!;
        }

        public async Task<bool> AddShiftAsync(Shift shift)
        {
            var json = JsonConvert.SerializeObject(shift);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(baseUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Status Code: {(int)response.StatusCode}");
            Console.WriteLine($"[DEBUG] Response Body: {responseBody}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateShiftAsync(int id, Shift shift)
        {
            var json = JsonConvert.SerializeObject(shift);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{baseUrl}/{shift.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteShiftAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
