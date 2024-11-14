

using System.Text;
using Newtonsoft.Json;
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public class PropertyService : IPropertyService
{   
    HttpClient httpClient = new ();
    
    public async Task<List<PropertyDto>> GetProperties()
    {   
        var url = "http://localhost:5037/api/Property";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);
        
        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to a list of PropertyDto objects
        var propertyList = JsonConvert.DeserializeObject<List<PropertyDto>>(jsonResponse);

        // Pass the propertyList to the View
        return propertyList;
    }

    public async Task<bool> CreateProperty(PropertyDto propertyDto, int ownerId)
    {
        // Add ownerId as a query parameter in the URL
        var url = $"http://localhost:5037/api/Property?ownerId={ownerId}";

        // Serialize the PropertyDto object to JSON
        var jsonContent = JsonConvert.SerializeObject(propertyDto);

        // Create the HttpContent with the JSON string, setting the correct media type
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Perform the POST request and await the response
        var response = await httpClient.PostAsync(url, content);

        // Check if the request was successful and return true or false
        return response.IsSuccessStatusCode;
    }

    public async Task<PropertyDto> GetPropertyById(int id)
    {
        var url = $"http://localhost:5037/api/Property/{id}";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            // Return null for any unsuccessful response (including 404)
            return null;
        }
        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to a list of PropertyDto objects
        var propertyDto = JsonConvert.DeserializeObject<PropertyDto>(jsonResponse);

        // Pass the ownerList to the View
        return propertyDto;
    }

    public async Task<List<PropertyDto>> GetPropertiesByOwnerId(int id)
    {
        var url = $"http://localhost:5037/api/Property/{id}/Properties";
        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            // Return null for any unsuccessful response (including 404)
            return null;
        }
        // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON string to a list of Owner objects
        var propertyListDto = JsonConvert.DeserializeObject<List<PropertyDto>>(jsonResponse);

        // Pass the ownerList to the View
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
            // If successful, deserialize the response content to a PropertyDto
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var updatedPropertyDto = JsonConvert.DeserializeObject<PropertyDto>(jsonResponse);
            return updatedPropertyDto;
        }
        else
        {
            // Return null if creation was unsuccessful
            return null;
        }
    }

    public async Task<bool> DeleteProperty(int id)
    {
        var url = $"http://localhost:5037/api/Property/{id}";
        var response = await httpClient.DeleteAsync(url);
        
        // Return true if delete was successful, false otherwise
        return response.IsSuccessStatusCode;
    }
}
