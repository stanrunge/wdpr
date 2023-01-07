using webapi.Data;
using webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ZaalController
{
    private readonly WdprContext _context;
    
    public ZaalController(WdprContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IEnumerable<Zaal> Get()
    {
        return _context.Zalen;
    }
}