
using Newtonsoft.Json;
using System.Text;
using TechnicoWebApi.Dtos;


namespace Technico.Services;

public class RepairService(HttpClient httpClient) : IRepairService
{
    public async Task<List<RepairDto>> GetRepairs()
    {
        var url = "http://localhost:5037/api/Repair";
        var response = await httpClient.GetAsync(url);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var repairList = JsonConvert.DeserializeObject<List<RepairDto>>(jsonResponse);

        return repairList;
    }

    public async Task<List<RepairDto>> GetOngoingRepairs()
    {
        var url = "http://localhost:5037/api/Repair/ongoing";
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var ongoingRepairList = JsonConvert.DeserializeObject<List<RepairDto>>(jsonResponse);

            return ongoingRepairList ?? new List<RepairDto>();
        }
        
        return new List<RepairDto>();
    }

    public async Task<List<RepairDto>> SearchRepairsByDateRange(DateTime startDateTime, DateTime endDateTime, int ownerId)
    {
        var url = $"http://localhost:5037/api/Repair/searchrepairs?startDate={startDateTime}&endDate={endDateTime}&id={ownerId}";
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return null;

        }
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var repairList = JsonConvert.DeserializeObject<List<RepairDto>>(jsonResponse);

        return repairList;
    }

    public async Task<RepairDto> GetRepairById (int id)
    {
        var url = $"http://localhost:5037/api/Repair/{id}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var repairDto = JsonConvert.DeserializeObject<RepairDto>(jsonResponse);

        return repairDto;
    }

    public async Task<RepairDto> CreateRepair (RepairDto repairDto , int ownerId)
    {
        var url = $"http://localhost:5037/api/Repair?ownerId={ownerId}";
        var jsonContent = JsonConvert.SerializeObject(repairDto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createdRepair = JsonConvert.DeserializeObject<RepairDto>(jsonResponse);
            return createdRepair;
        }
        else
        {
            return null;
        }
    }

    public async Task<RepairDto> UpdateRepair (RepairDto repairDto , int id)
    {
        var url = $"http://localhost:5037/api/Repair/{id}";
        var jsonContent = JsonConvert.SerializeObject(repairDto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var updatedRepair = JsonConvert.DeserializeObject<RepairDto>(jsonResponse);
            return updatedRepair;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<RepairDto>> DeleteRepair(int id)
    {
        var url = $"http://localhost:5037/api/Repair/{id}";
        var response = await httpClient.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var repairs = await GetRepairs();
            return repairs;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<RepairDto>> OwnerRepairs(int id)
    {
        var url = $"http://localhost:5037/api/Repair/ownerrepairs/{id}";

        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var repairDto = JsonConvert.DeserializeObject <List<RepairDto>>(jsonResponse);

        return repairDto;
    }
}
