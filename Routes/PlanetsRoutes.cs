using System.Text.Json;
using System.Text.RegularExpressions;

namespace starwarsapi.Routes;

using Microsoft.AspNetCore.Builder;

public class Planet
{
    public string Name { get; set; }
    public string Url { get; set; }
}

public static class PlanetsRoutes
{
    private static List<Planet> _favouritePlanets = new();
    
    private static int ExtractPlanetIdFromUrl(string url)
    {
        var match = Regex.Match(url, @"/(\d+)/$");
        if (match.Success)
        {
            return int.Parse(match.Groups[1].Value);
        }
        return -1;
    }
    
    private static int GetRandomPlanetIdNotInFavourites()
    {
        var random = new Random();
        var favouriteIds = _favouritePlanets.Select(p => ExtractPlanetIdFromUrl(p.Url)).ToList();
        var availableIds = Enumerable.Range(1, 60).Except(favouriteIds).ToList();

        if (!availableIds.Any())
        {
            return -1;  // All planets are favourited
        }

        return availableIds[random.Next(availableIds.Count)];
    }


    public static void MapPlanetRoutes(this WebApplication app)
    {
        app.MapGet("/planets/{page}", async (HttpClient client, int page) =>
            {
                var response = await client.GetAsync($"https://swapi.dev/api/planets/?page={page}");

                if (response.IsSuccessStatusCode)
                {
                    // parse response into JSON
                    var contentString = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<JsonElement>(contentString);
                    return Results.Ok(data);
                }

                return Results.Problem("Failed to fetch data from Star Wars API", statusCode: (int)response.StatusCode);
            })
            .WithName("GetPlanets");

        app.MapGet("/favouriteplanets/", () => _favouritePlanets.Select(p => p.Name).ToList())
            .WithName("/GetFavouritePlanets");

        app.MapPost("/favouriteplanets", (Planet planet) =>
            {
                Console.WriteLine(planet.Name);
                var existingPlanet = _favouritePlanets.FirstOrDefault(p => p.Name == planet.Name);
                if (existingPlanet != null)
                {
                    _favouritePlanets.Remove(existingPlanet);
                    return Results.Ok($"Planet {planet.Name} removed from favourites!");
                }
                _favouritePlanets.Add(planet);
                return Results.Ok($"Planet {planet.Name} added to favourites!");
            })
            .WithName("ModifyFavouritePlanets");

        app.MapGet("/newrandomplanet/", async (HttpClient client) =>
            {
                var randomPlanetId = GetRandomPlanetIdNotInFavourites();
                if (randomPlanetId == -1)
                {
                    return Results.NotFound("All planets are already favourited!");
                }

                var response = await client.GetAsync($"https://swapi.dev/api/planets/{randomPlanetId}/");
                if (!response.IsSuccessStatusCode)
                {
                    return Results.Problem("Failed to fetch data from Star Wars API", statusCode: (int)response.StatusCode);
                }

                var contentString = await response.Content.ReadAsStringAsync();
                var planetData = JsonSerializer.Deserialize<JsonElement>(contentString);
                return Results.Ok(planetData);
            })
            .WithName("GetNewFavouritePlanet");
    }
}