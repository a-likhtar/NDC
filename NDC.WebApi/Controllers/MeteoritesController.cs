using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDC.DataAccess;
using NDC.Domain.Entities;
using NDC.Domain.Services;

namespace NDC.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MeteoritesController : Controller
{
    private readonly MeteoritesContext _context;
    private readonly IItemSyncService _itemSyncService;
    
    public MeteoritesController(MeteoritesContext context, IItemSyncService itemSyncService)
    {
        _context = context;
        _itemSyncService = itemSyncService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Meteorite>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var meteorites = await _context.Meteorites.ToListAsync();

        return Ok(meteorites);
    }

    [HttpGet("sync")]
    public async Task<IActionResult> SyncItems()
    {
        await _itemSyncService.SyncItemsAsync();

        return Ok();
    }
}