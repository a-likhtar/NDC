using System.Text.Json;
using Microsoft.Extensions.Configuration;
using NDC.Domain.Entities;
using NDC.Domain.Helpers;
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
        // cetting json
        // check if Json Not Modified with ETags
        var jsonUrl = _configuration["MeteoritesData"];
        var json = await _httpClient.GetStringAsync(jsonUrl);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new MeteoriteConverter() }
        };
        var meteoriteDtos = JsonSerializer.Deserialize<List<MeteoriteDto>>(json, jsonSerializerOptions);

        //  get unique item classes from DTOs
        var meteoritesClassNames = meteoriteDtos.Select(d => d.MeteoriteClass)
            .Distinct()
            .ToArray();
        
        // check existing classes
        var existingMeteoriteClasses = (await _meteoriteClassRepository.GetClassesByClassNames(meteoritesClassNames))
            .ToDictionary(mc => mc.Name, mc => mc);
        
        // create new classes
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

        // map from DTO to entity
        var remoteMeteorites = meteoriteDtos.Select(dto => new Meteorite
        {
            Name = dto.Name,
            MeteoriteId = dto.MeteoriteId,
            NameType = dto.NameType,
            MeteoriteClassId = existingMeteoriteClasses[dto.MeteoriteClass].Id,
            Mass = dto.Mass,
            FallType = dto.FallType,
            ObservationYear = dto.ObservationYear,
            Reclat = dto.Reclat,
            Reclong = dto.Reclong,
            GeolocationType = dto.GeoLocation?.Type,
            ComputedRegionCbhk = dto.ComputedRegionCbhk,
            ComputedRegionNnqa = dto.ComputedRegionNnqa,
            Hash = HashHelper.ComputeHash(dto)
        }).ToList();

        // get existing items
        var existingMeteorites = await _meteoriteRepository.GetAllAsync();
        var existingMeteoritesDict = existingMeteorites.ToDictionary(m => m.MeteoriteId);

        var toInsert = new List<Meteorite>();
        var toUpdate = new List<Meteorite>();
        var toDelete = existingMeteorites
            .Where(m => remoteMeteorites.All(r => r.MeteoriteId != m.MeteoriteId))
            .ToList();

        foreach (var remoteMeteorite in remoteMeteorites)
        {
            if (!existingMeteoritesDict.TryGetValue(remoteMeteorite.MeteoriteId, out var existingMeteorite))
            {
                toInsert.Add(remoteMeteorite);
            }
            else if (existingMeteorite.Hash != remoteMeteorite.Hash)
            {
                toUpdate.Add(remoteMeteorite);
            }
        }

        // syncing with bulk operations
        await _meteoriteRepository.SyncMeteoritesAsync(toInsert, toUpdate, toDelete);
    }
}