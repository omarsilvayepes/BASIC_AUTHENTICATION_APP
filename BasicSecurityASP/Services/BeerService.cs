using BasicSecurityASP.Models;
using System.Text.Json;

namespace BasicSecurityASP.Services
{
    public class BeerService : IBeerService
    {
        string path = @"C:\Users\omary\OneDrive\Escritorio\LearningDEV\BasicSecurityASP\BasicSecurityASP\beers.json";
        public async Task<List<Beer>> Get()
        {
            string content=await File.ReadAllTextAsync(path);
            var beers=JsonSerializer.Deserialize<List<Beer>>(content);
            return beers;
        }
    }
}
