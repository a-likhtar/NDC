using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDC.DataAccess;
using NDC.Domain.Entities;
using NDC.Domain.Extensions;
using NDC.Domain.Interfaces;
using NDC.Domain.Models;
using NDC.Domain.QueryParams;
using NDC.Domain.Services;

namespace NDC.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeteoritesController : Controller
{
    private readonly MeteoritesContext _context;
    private readonly IItemSyncService _itemSyncService;
    private readonly IMeteoriteRepository _meteoriteRepository;
    private readonly IMeteoriteClassRepository _meteoriteClassRepository;
    
    public MeteoritesController(MeteoritesContext context, IItemSyncService itemSyncService, IMeteoriteRepository meteoriteRepository, IMeteoriteClassRepository meteoriteClassRepository)
    {
        _context = context;
        _itemSyncService = itemSyncService;
        _meteoriteRepository = meteoriteRepository;
        _meteoriteClassRepository = meteoriteClassRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Meteorite>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var meteorites = await _context.Meteorites.ToListAsync();

        return Ok(meteorites);
    }

    /// <summary>
    /// Get grouped meteorites with filtering and sorting.
    /// </summary>
    /// <param name="queryParams">Request params</param>
    /// <returns>Grouped meteorites by year</returns>
    [HttpGet("grouped")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllGrouped([FromQuery] MeteoriteQueryParams queryParams)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var query = _meteoriteRepository.GetAll().ApplyFiltering(queryParams);

        var filteredMeteorites = await query.ToListAsync();

        var grouped = filteredMeteorites
            .GroupBy(m => m.ObservationYear!.Value.Year)
            .ApplySortingToGroups(queryParams);

        var groupedResult = new GroupedMeteoritesResult
        {
            TotalCount = filteredMeteorites.Count,
            TotalMass = filteredMeteorites.Sum(m => m.Mass),
            Groups = grouped.Select(g => new MeteoriteGroup
            {
                Year = g.Key,
                Mass = g.Sum(m => m.Mass),
                MeteoritesCount = g.Count()
            })
        };

        return Ok(groupedResult);
    }

    [HttpGet("classes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMeteoritesClasses()
    {
        var meteoriteClasses = await _meteoriteClassRepository.GetAll();

        return Ok(meteoriteClasses);
    }

    [HttpGet("sync")]
    public async Task<IActionResult> SyncItems()
    {
        await _itemSyncService.SyncItemsAsync();

        return Ok();
    }
    
    // TODO: sorting, add indexes, cache, invocation inside job
}