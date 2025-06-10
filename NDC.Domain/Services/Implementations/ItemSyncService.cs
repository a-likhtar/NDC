using System.Text.Json;
using Microsoft.Extensions.Configuration;
using NDC.Domain.Entities;
using NDC.Domain.Interfaces;
using NDC.Domain.JsonConverters;
using NDC.Domain.Models;

namespace NDC.Domain.Services.Implementations;

public class ItemSyncService : IItemSyncService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IMeteoriteClassRepository _meteoriteClassRepository;
    private readonly IMeteoriteRepository _meteoriteRepository;
    
    public ItemSyncService(HttpClient httpClient, IConfiguration configuration, IMeteoriteClassRepository meteoriteClassRepository, IMeteoriteRepository meteoriteRepository)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _meteoriteClassRepository = meteoriteClassRepository;
        _meteoriteRepository = meteoriteRepository;
    }
    
    public async Task SyncItemsAsync()
    {
        // Getting json
        // Check if Json Not Modified with ETags
        var jsonUrl = _configuration["MeteoritesData"];
        var json = await _httpClient.GetStringAsync(jsonUrl);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new MeteoriteConverter() }
        };
        var meteoriteDtos = JsonSerializer.Deserialize<List<MeteoriteDto>>(json, jsonSerializerOptions);

        // 2. Собираем все уникальные itemClass из JSON
        var meteoritesClassNames = meteoriteDtos.Select(d => d.MeteoriteClass)
            .Distinct()
            .ToArray();
        
        // Check existing classes
        var existingMeteoriteClasses = (await _meteoriteClassRepository.GetClassesByClassNames(meteoritesClassNames))
            .ToDictionary(mc => mc.Name, mc => mc);
        
        // Create new classes
        var newMeteoriteClasses = meteoritesClassNames
            .Where(name => !existingMeteoriteClasses.ContainsKey(name))
            .Select(name => new MeteoriteClass { Name = name})
            .ToList();

        if (newMeteoriteClasses.Any())
        {
            await _meteoriteClassRepository.BulkInsertAsync(newMeteoriteClasses);

            foreach (var newClass in newMeteoriteClasses)
            {
                existingMeteoriteClasses[newClass.Name] = newClass;
            }
        }

        var remote = meteoriteDtos.Select(dto => new Meteorite
        {

        });

        var existingMeteorites = await _meteoriteRepository.GetAllAsync();
    }
}