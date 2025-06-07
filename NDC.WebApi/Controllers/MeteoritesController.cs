using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDC.DataAccess;
using NDC.DataAccess.Entities;

namespace NDC.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MeteoritesController : Controller
{
    private readonly MeteoritesContext _context;
    
    public MeteoritesController(MeteoritesContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Meteorite>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var meteorites = await _context.Meteorites.ToListAsync();

        return Ok(meteorites);
    }
}