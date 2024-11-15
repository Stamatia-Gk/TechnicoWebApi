

using Newtonsoft.Json;
using System.Text;
using TechnicoWebApi.Dtos;


namespace Technico.Services;

public class RepairService(HttpClient httpClient) : IRepairService
{
    public async Task<List<RepairDto>> GetRepairs()
    {
        var url = "http://localhost:5037/api/Repair";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);

        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to a list of repair objects
        var repairList = JsonConvert.DeserializeObject<List<RepairDto>>(jsonResponse);

        // Pass the repairList to the View
        return repairList;
    }

    public async Task<List<RepairDto>> GetOngoingRepairs()
    {
        var url = "http://localhost:5037/api/Repair/ongoing";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            // Await the content to get the JSON string result
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of repair objects
            var ongoingRepairList = JsonConvert.DeserializeObject<List<RepairDto>>(jsonResponse);

            // Pass the repairList to the View
            return ongoingRepairList ?? new List<RepairDto>();
        }
        
        return new List<RepairDto>();
    }

    public async Task<List<RepairDto>> SearchRepairsByDateRange(DateTime startDateTime, DateTime endDateTime, int ownerId)
    {
        var url = $"http://localhost:5037/api/Repair/searchrepairs?startDate={startDateTime}&endDate={endDateTime}&id={ownerId}";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return null;

        }
        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to a list of repair objects
        var repairList = JsonConvert.DeserializeObject<List<RepairDto>>(jsonResponse);

        // Pass the repairList to the View
        return repairList;
    }

    public async Task<RepairDto> GetRepairById (int id)
    {
        var url = $"http://localhost:5037/api/Repair/{id}";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            // Return null for any unsuccessful response (including 404)
            return null;
        }
        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to a Repair object
        var repairDto = JsonConvert.DeserializeObject<RepairDto>(jsonResponse);

        // Pass the repair to the View
        return repairDto;
    }

    public async Task<RepairDto> CreateRepair (RepairDto repairDto , int ownerId)
    {
        // Add ownerId as a query parameter in the URL
        var url = $"http://localhost:5037/api/Repair?ownerId={ownerId}";

        // Serialize the PropertyDto object to JSON
        var jsonContent = JsonConvert.SerializeObject(repairDto);

        // Create the HttpContent with the JSON string, setting the correct media type
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Perform the POST request and await the response
        var response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            // If successful, deserialize the response content to a RepairDto
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createdRepair = JsonConvert.DeserializeObject<RepairDto>(jsonResponse);
            return createdRepair;
        }
        else
        {
            // Return null if creation was unsuccessful
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
            // If successful, deserialize the response content to a RepairDto
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var updatedRepair = JsonConvert.DeserializeObject<RepairDto>(jsonResponse);
            return updatedRepair;
        }
        else
        {
            // Return null if creation was unsuccessful
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
            // Return null for any unsuccessful response (including 404)
            return null;
        }
        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to a Repair object
        var repairDto = JsonConvert.DeserializeObject <List<RepairDto>>(jsonResponse);

        // Pass the repair to the View
        return repairDto;
    }

}
