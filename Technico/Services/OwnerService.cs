

using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public class OwnerService : IOwnerService
{
    HttpClient httpClient = new();

    public async Task<List<OwnerResponseDto>> GetAllOwners()
    {
        var url = "http://localhost:5037/api/Owner";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);

        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to a list of OwnerResponseDto objects
        var ownersList = JsonConvert.DeserializeObject<List<OwnerResponseDto>>(jsonResponse);

        // Pass the ownerList to the View
        return ownersList;
    }

    public async Task<OwnerResponseDto> GetOwnerById(int id)
    {
        var url = $"http://localhost:5037/api/Owner/{id}";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            // Return null for any unsuccessful response (including 404)
            return null;
        }
        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to an OwnerResponseDto object
        var ownerDto = JsonConvert.DeserializeObject<OwnerResponseDto>(jsonResponse);

        // Pass ownerDto to the View
        return ownerDto;
    }

    public async Task<bool> CreateOwner([Bind("Id,VAT,Name,Surname,Address,PhoneNumber,Email,Password")] OwnerRequestDto ownerDto)
    {
        var url = $"http://localhost:5037/api/Owner";

        // Serialize the OwnerRequestDto object to JSON
        var jsonContent = JsonConvert.SerializeObject(ownerDto);

        // Create the HttpContent with the JSON string, setting the correct media type
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Perform the POST request and await the response
        var response = await httpClient.PostAsync(url, content);

        // Check if the request was successful and return true or false
        return response.IsSuccessStatusCode;
    }

    /*
     
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
     
     */

    public async Task<OwnerResponseDto> UpdateOwner(int id, OwnerResponseDto ownerDto)
    {
        var url = $"http://localhost:5037/api/Owner/{id}";
        var jsonContent = JsonConvert.SerializeObject(ownerDto);

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            // If successful, deserialize the response content to a RepairDto
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var updatedOwner = JsonConvert.DeserializeObject<OwnerResponseDto>(jsonResponse);
            return updatedOwner;
        }
        else
        {
            // Return null if creation was unsuccessful
            return null;
        }
    }

    public async Task<bool> DeleteOwner(int id)
    {
        var url = $"http://localhost:5037/api/Owner/{id}";
        var response = await httpClient.DeleteAsync(url);

        // Return true if delete was successful, false otherwise
        return response.IsSuccessStatusCode;
    }

    //public async Task<OwnerResponseDto> SearchOwner(string? vat, string? email) {}
    /*public async Task<OwnerResponseDto> Login(string email, string password) 
    {
        var url = $"http://localhost:5037/api/Owner/{email},{password}";
        var repsonse = await httpClient.PostAsync(url, content);
    }*/
}
