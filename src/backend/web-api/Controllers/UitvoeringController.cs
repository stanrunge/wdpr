using webapi.Data;
using webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UitvoeringController
{
    private readonly WdprContext _context;
    
    public UitvoeringController(WdprContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IEnumerable<Uitvoering?> Get()
    {
        return _context.Uitvoeringen;
    }
    
    [HttpGet("{id}")]
    public async Task<Uitvoering?> Get(int id)
    {
        return await _context.Uitvoeringen.FindAsync(id);
    }
}