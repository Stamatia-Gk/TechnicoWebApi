

using Newtonsoft.Json;
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public class PropertyService : IPropertyService
{
    public async Task<List<PropertyDto>> GetProperties()
    {
        HttpClient httpClient = new HttpClient();
        string url = "http://localhost:5037/api/Property";

        // Await the response to complete the asynchronous task
        var response = await httpClient.GetAsync(url);
        
            // Await the content to get the JSON string result
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);

            // Deserialize the JSON string to a list of Owner objects
        var propertyList = JsonConvert.DeserializeObject<List<PropertyDto>>(jsonResponse);

            // Pass the ownerList to the View
        return propertyList;

    }

}
