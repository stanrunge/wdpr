using webapi.Data;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class KlantController
{
    private readonly WdprContext _context;
    
    public KlantController(WdprContext context)
    {
        _context = context;
    }
}