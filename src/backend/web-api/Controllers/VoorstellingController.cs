using webapi.Data;
using webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class VoorstellingController : ControllerBase
{
    private readonly WdprContext _context;
    
    public VoorstellingController(WdprContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IEnumerable<Voorstelling> Get()
    {
        return _context.Voorstellingen;
    }
}