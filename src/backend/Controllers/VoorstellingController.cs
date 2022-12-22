using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

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