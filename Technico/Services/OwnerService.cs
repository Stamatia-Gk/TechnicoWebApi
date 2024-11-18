
using System.Text;
using Newtonsoft.Json;
using Technico.Session;
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public class OwnerService(HttpClient httpClient) : IOwnerService
{
    public async Task<List<OwnerResponseDto>> GetAllOwners()
    {
        var url = "http://localhost:5037/api/Owner";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var ownersList = JsonConvert.DeserializeObject<List<OwnerResponseDto>>(jsonResponse);

        return ownersList;
    }

    public async Task<OwnerResponseDto> GetOwnerById(int id)
    {
        var url = $"http://localhost:5037/api/Owner/{id}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var ownerDto = JsonConvert.DeserializeObject<OwnerResponseDto>(jsonResponse);

        return ownerDto;
    }

    public async Task<OwnerResponseDto> CreateOwner(OwnerRequestDto ownerDto)
    {
        var url = $"http://localhost:5037/api/Owner";
        var jsonContent = JsonConvert.SerializeObject(ownerDto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createdOwner = JsonConvert.DeserializeObject<OwnerResponseDto>(jsonResponse);
            return createdOwner;
        }
        else
        {
            return null;
        }
    }

    public async Task<OwnerResponseDto> UpdateOwner(int id, OwnerResponseDto ownerDto)
    {
        var url = $"http://localhost:5037/api/Owner/{id}";
        var jsonContent = JsonConvert.SerializeObject(ownerDto);

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var updatedOwner = JsonConvert.DeserializeObject<OwnerResponseDto>(jsonResponse);
            return updatedOwner;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<OwnerResponseDto>> DeleteOwner(int id)
    {
        var url = $"http://localhost:5037/api/Owner/{id}";
        var response = await httpClient.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var owners = await GetAllOwners();
            return owners;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<OwnerResponseDto>> SearchOwner(string vat, string email)
    {
        var url = $"http://localhost:5037/api/Owner/searchowner?vat={vat}&email={email}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var ownerResult = JsonConvert.DeserializeObject<OwnerResponseDto>(jsonResponse);
        var newListOwnerResponseDto = new List<OwnerResponseDto> { ownerResult };

        return newListOwnerResponseDto;
    }

    public async Task<OwnerResponseDto> Login(string email, string password) 
    {
        var url = $"http://localhost:5037/api/Owner/Login";
        var credentialDto = new OwnerCredentialsDto { Email = email, Password = password };
        var jsonContent = JsonConvert.SerializeObject(credentialDto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var loggedInOwner = JsonConvert.DeserializeObject<OwnerResponseDto>(jsonResponse);
            return loggedInOwner;
        }
        else
        {
            return null;
        }
    }
}
