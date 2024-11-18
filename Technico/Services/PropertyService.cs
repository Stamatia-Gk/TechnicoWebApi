
using System.Text;
using Newtonsoft.Json;
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public class PropertyService(HttpClient httpClient) : IPropertyService
{   
    public async Task<List<PropertyDto>> GetProperties()
    {   
        var url = "http://localhost:5037/api/Property";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var propertyList = JsonConvert.DeserializeObject<List<PropertyDto>>(jsonResponse);

        return propertyList;
    }

    public async Task<List<PropertyDto>> SearchPropertiesByOwnerOrVatAsync(int id, string vat)
    {
        var url = $"http://localhost:5037/api/Property/searchproperties?propertyId={id}&ownerVat={vat}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var propertyList = JsonConvert.DeserializeObject<List<PropertyDto>>(jsonResponse);

        return propertyList; 
    }

    public async Task<bool> CreateProperty(PropertyDto propertyDto, int ownerId)
    {
        var url = $"http://localhost:5037/api/Property?ownerId={ownerId}";
        var jsonContent = JsonConvert.SerializeObject(propertyDto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<PropertyDto> GetPropertyById(int id)
    {
        var url = $"http://localhost:5037/api/Property/{id}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var propertyDto = JsonConvert.DeserializeObject<PropertyDto>(jsonResponse);

        return propertyDto;
    }

    public async Task<List<PropertyDto>> GetPropertiesByOwnerId(int id)
    {
        var url = $"http://localhost:5037/api/Property/{id}/Properties";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var propertyListDto = JsonConvert.DeserializeObject<List<PropertyDto>>(jsonResponse);

        return propertyListDto;
    }
    
    public async Task<PropertyDto> UpdateProperty (PropertyDto repairDto , int id)
    {
        var url = $"http://localhost:5037/api/Property?oldPropertyId={id}";
        var jsonContent = JsonConvert.SerializeObject(repairDto);

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var updatedPropertyDto = JsonConvert.DeserializeObject<PropertyDto>(jsonResponse);
            return updatedPropertyDto;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<PropertyDto>> DeleteProperty(int id)
    {
        var url = $"http://localhost:5037/api/Property/{id}";
        var response = await httpClient.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var repairs = await GetProperties();
            return repairs;
        }
        else
        {
            return null;
        }
    }
}
