using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

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